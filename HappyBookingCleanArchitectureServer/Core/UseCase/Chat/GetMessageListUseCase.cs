using Azure;
using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class GetMessageListUseCase : IGetMessageListUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public GetMessageListUseCase(IChatRepository chatRepository, IMemoryCache cache, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<GetMessageListResponse> GetMessageList(long userId, GetMessageListRequest request, IHubContext<ChatHub> hubContext)
    {
        try
        {
            var result = await _chatRepository.GetMessageList(request.ChatId, request.KeyWord, request.PageIndex, request.PageSize);
            UpdateStatus(hubContext);
            return new GetMessageListResponse(userId, result.Select(item => new MessageDto(item)).ToList(), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }

    private async Task UpdateStatus(IHubContext<ChatHub> hubContext)
    {
        var result = await _userRepository.AutoMarkUserAsOffline(ParamConstant.LastSecond);
        string jsonString = JsonSerializer.Serialize(result);
        await hubContext.Clients.All.SendAsync(RealtimeConstant.UserOffline, jsonString);
    }
}
