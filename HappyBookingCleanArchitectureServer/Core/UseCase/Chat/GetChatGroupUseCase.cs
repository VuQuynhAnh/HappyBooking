using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class GetChatGroupUseCase : IGetChatGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public GetChatGroupUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<GetChatGroupResponse> GetChatGroup(long userId, GetChatGroupRequest request)
    {
        try
        {
            if (!await _chatRepository.CheckExistMemberInGroupChat(request.ChatId, request.MemberId))
            {
                return new GetChatGroupResponse(userId, new ChatDto(), StatusEnum.InvalidParam, _cache);
            }
            var result = await _chatRepository.GetChatGroup(request.ChatId);
            return new GetChatGroupResponse(userId, new ChatDto(result), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
