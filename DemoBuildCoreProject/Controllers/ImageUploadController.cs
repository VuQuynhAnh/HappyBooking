using DemoBuildCoreProject.Business.IService;
using Microsoft.AspNetCore.Mvc;

namespace DemoBuildCoreProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : BaseController
{
    private readonly IUploadImageService _uploadImageService;

    public ImageUploadController(IUploadImageService uploadImageService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _uploadImageService = uploadImageService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        var response = await _uploadImageService.UploadImageAsync(image, UserId);
        return Ok(response);
    }

    [HttpDelete("{deleteHash}")]
    public async Task<IActionResult> DeleteImage(string deleteHash)
    {
        try
        {
            var response = await _uploadImageService.DeleteImageAsync(deleteHash, UserId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete image: {ex.Message}");
        }
    }

}
