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

public class LeaveChatGroupUseCase : ILeaveChatGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public LeaveChatGroupUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<LeaveChatGroupResponse> LeaveChatGroup(long userId, LeaveChatGroupRequest request, IHubContext<ChatHub> hubContext)
    {
        try
        {
            var leaveResponse = await _chatRepository.LeaveChatGroup(request.ChatId, request.MemberId, userId);
            ChatDto result = new();
            if (leaveResponse)
            {
                var chatResult = await _chatRepository.GetChatGroup(request.ChatId);
                result = new ChatDto(chatResult);
                string jsonString = JsonSerializer.Serialize(result);
                await hubContext.Clients.All.SendAsync(RealtimeConstant.ChatGroupUpdate, jsonString);
            }
            return new LeaveChatGroupResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
