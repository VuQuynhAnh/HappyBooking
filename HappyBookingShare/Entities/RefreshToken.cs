using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyBookingShare.Entities;

[Table("RefreshToken")]
public class RefreshToken
{
    [Key]
    public int Id { get; set; } // Khóa chính

    public string Token { get; set; } = string.Empty; // Token string

    public long UserId { get; set; } // Khóa ngoại liên kết với người dùng

    public DateTime ExpiryDate { get; set; } // Ngày hết hạn của refresh token

    public bool IsRevoked { get; set; } // Đánh dấu nếu token bị thu hồi
}
