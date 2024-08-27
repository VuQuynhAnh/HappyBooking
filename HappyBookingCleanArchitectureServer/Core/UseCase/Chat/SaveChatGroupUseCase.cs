using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class SaveChatGroupUseCase : ISaveChatGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public SaveChatGroupUseCase(IChatRepository chatRepository, IMemoryCache cache, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _cache = cache;
        _userRepository = userRepository;
    }

    public async Task<SaveChatGroupResponse> SaveChatGroup(long userId, SaveChatGroupRequest request, IHubContext<ChatHub> hubContext)
    {
        try
        {
            List<ChatMemberModel> chatMemberList = new();
            if (request.ChatMemberList.Any())
            {
                if (!await _userRepository.CheckExistUserList(request.ChatMemberList.Select(item => item.MemberId).ToList()))
                {
                    return new SaveChatGroupResponse(userId, new ChatDto(), StatusEnum.InvalidParam, _cache);
                }
                chatMemberList = request.ChatMemberList.Select(item => new ChatMemberModel(item.MemberId, item.ChatRole)).ToList();
            }
            var chatId = request.ChatId;

            // if chat is 1vs1, get old chatId
            if (chatId == 0 && chatMemberList.Count == 2)
            {
                chatId = await _chatRepository.Get1v1ExistGroupChatId(chatMemberList[0].MemberId, chatMemberList[1].MemberId);
            }
            var chatResponse = await _chatRepository.SaveChatGroup(chatId, request.ChatName, request.AvatarUrl, request.IsGroup, userId);
            if (chatMemberList.Any())
            {
                var addMemberToGroupResponse = await _chatRepository.AddMemberToGroup(chatResponse.ChatId, chatMemberList, userId);
            }

            var chatResult = await _chatRepository.GetChatGroup(chatId, userId);
            string jsonString = JsonSerializer.Serialize(chatResult);
            await hubContext.Clients.All.SendAsync(RealtimeConstant.ChatGroupUpdate, jsonString);
            return new SaveChatGroupResponse(userId, new ChatDto(chatResult), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
