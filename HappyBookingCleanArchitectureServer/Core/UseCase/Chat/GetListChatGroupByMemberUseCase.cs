using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class GetListChatGroupByMemberUseCase : IGetListChatGroupByMemberUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public GetListChatGroupByMemberUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<GetListChatGroupByMemberResponse> GetListChatGroupByMember(long userId, GetListChatGroupByMemberRequest request)
    {
        try
        {
            var result = await _chatRepository.GetListChatGroupByMember(request.MemberId, request.KeyWord, request.IsGroupChat, request.PageIndex, request.PageSize);
            return new GetListChatGroupByMemberResponse(userId, result.Select(item => new ChatDto(item)).ToList(), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
