using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class LeaveChatGroupUseCase : ILeaveChatGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public LeaveChatGroupUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<LeaveChatGroupResponse> LeaveChatGroup(long userId, LeaveChatGroupRequest request)
    {
        try
        {
            var result = await _chatRepository.LeaveChatGroup(request.ChatId, request.MemberId, userId);
            return new LeaveChatGroupResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
