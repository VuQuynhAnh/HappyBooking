using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class UpdateMessageUseCase : IUpdateMessageUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public UpdateMessageUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<UpdateMessageResponse> UpdateMessage(long userId, UpdateMessageRequest request)
    {
        try
        {
            var result = await _chatRepository.UpdateMessage(request.MessageId, request.Content, request.MessageType, userId);
            return new UpdateMessageResponse(userId, new MessageDto(result), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
