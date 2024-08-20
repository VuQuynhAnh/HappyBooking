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

public class SendMessageUseCase : ISendMessageUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public SendMessageUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<SendMessageResponse> SendMessage(long userId, SendMessageRequest request, IHubContext<ChatHub> hubContext)
    {
        try
        {
            var messageModel = await _chatRepository.SendMessage(request.ChatId, request.Content, request.MessageType, userId);
            var result = new MessageDto(messageModel);
            string jsonString = JsonSerializer.Serialize(result);
            await hubContext.Clients.All.SendAsync(RealtimeConstant.ReceiveMessage, jsonString);
            return new SendMessageResponse(userId, result, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
