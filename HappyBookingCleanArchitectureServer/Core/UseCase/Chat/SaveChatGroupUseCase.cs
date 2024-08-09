using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class SaveChatGroupUseCase : ISaveChatGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public SaveChatGroupUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<SaveChatGroupResponse> SaveChatGroup(long userId, SaveChatGroupRequest request)
    {
        try
        {
            var result = await _chatRepository.SaveChatGroup(request.ChatId, request.Name, request.AvatarUrl, request.IsGroup, userId);
            return new SaveChatGroupResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
