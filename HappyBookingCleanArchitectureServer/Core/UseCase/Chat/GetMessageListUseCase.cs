using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class GetMessageListUseCase : IGetMessageListUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public GetMessageListUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<GetMessageListResponse> GetMessageList(long userId, GetMessageListRequest request, IHubContext<ChatHub> hubContext)
    {
        try
        {
            var result = await _chatRepository.GetMessageList(request.ChatId, request.KeyWord, request.PageIndex, request.PageSize);
            return new GetMessageListResponse(userId, result.Select(item => new MessageDto(item)).ToList(), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
