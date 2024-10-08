﻿using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Image;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.User;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : BaseController
{
    private readonly IClearImageNotUsedUseCase _clearImageNotUsedUseCase;
    private readonly IDeleteImageUseCase _deleteImageUseCase;
    private readonly IUploadImageUseCase _uploadImageUseCase;

    public ImageUploadController(
        IClearImageNotUsedUseCase clearImageNotUsedUseCase,
        IDeleteImageUseCase deleteImageUseCase,
        IUploadImageUseCase uploadImageUseCase,
        IHubContext<ChatHub> hubContext,
        IHttpContextAccessor httpContextAccessor,
        IHeartbeatUserUseCase heartbeatUserUseCase) : base(httpContextAccessor, heartbeatUserUseCase, hubContext)
    {
        _clearImageNotUsedUseCase = clearImageNotUsedUseCase;
        _deleteImageUseCase = deleteImageUseCase;
        _uploadImageUseCase = uploadImageUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        var response = await _uploadImageUseCase.UploadImage(image, UserId);
        return Ok(response);
    }

    [HttpPost(APIName.ImageUploadWithoutAuthorize)]
    [AllowAnonymous]
    public async Task<IActionResult> ImageUploadWithoutAuthorize([FromForm] IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("No image file provided.");
        }

        var response = await _uploadImageUseCase.UploadImage(image, UserId);
        return Ok(response);
    }

    [HttpDelete("{deleteHash}")]
    public async Task<IActionResult> DeleteImage(string deleteHash)
    {
        var response = await _deleteImageUseCase.DeleteImage(deleteHash, UserId);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImages()
    {
        var response = await _clearImageNotUsedUseCase.ClearImageNotUsed(UserId);
        return Ok(response);
    }
}
