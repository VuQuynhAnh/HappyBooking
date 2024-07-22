using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IService;
using HappyBookingCleanArchitectureServer.Database;
using HappyBookingShare.Model;
using HappyBookingShare.Response.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HappyBookingCleanArchitectureServer.Core.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly DataContext _context;

    public TokenService(IConfiguration configuration, DataContext dataContext, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _context = dataContext;
    }

    public async Task<TokenResponse> GenerateToken(UserModel user)
    {
        var jwtToken = GenerateJwtToken(user);
        var refreshToken = await _userRepository.GenerateRefreshToken(user);

        return new TokenResponse(jwtToken, refreshToken);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(string jwtToken, string refreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(jwtToken);
        long userId = 0;
        long.TryParse(principal.FindFirst("UserId")?.Value, out userId);
        var user = await _userRepository.GetUserByUserId(userId);
        if (user.UserId != userId)
        {
            return null;
        }

        var storedRefreshToken = await _context.RefreshTokenRepository.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
        if (storedRefreshToken == null || storedRefreshToken.IsRevoked || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
        {
            return null;
        }
        storedRefreshToken.IsRevoked = true;
        await _context.SaveChangesAsync();

        var tokens = await GenerateToken(user);
        return tokens;
    }

    #region private function
    private string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
    #endregion
}
