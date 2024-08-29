using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Model;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class AddMemberToGroupUseCase : IAddMemberToGroupUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;

    public AddMemberToGroupUseCase(IChatRepository chatRepository, IUserRepository userRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _cache = cache;
    }

    public async Task<AddMemberToGroupResponse> AddMemberToGroup(long userId, AddMemberToGroupRequest request)
    {
        try
        {
            var validateResult = await ValidateInput(request);
            if (validateResult != StatusEnum.ValidateSuccessed)
            {
                return new AddMemberToGroupResponse(userId, false, validateResult, _cache);
            }
            var chatMemberList = request.ChatMemberList.Select(item => new ChatMemberModel(item.MemberId, item.ChatRole, item.IsDeleted)).ToList();
            var addMemberResult = await _chatRepository.SaveGroupMember(request.ChatId, chatMemberList, userId);
            return new AddMemberToGroupResponse(userId, addMemberResult, StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
            await _userRepository.ReleaseResource();
        }
    }

    private async Task<StatusEnum> ValidateInput(AddMemberToGroupRequest request)
    {
        if (request.ChatId == 0 || !await _chatRepository.CheckExistChat(request.ChatId))
        {
            return StatusEnum.InvalidParam;
        }
        if (!request.ChatMemberList.Any() || !await _userRepository.CheckExistUserList(request.ChatMemberList.Select(item => item.MemberId).ToList()))
        {
            return StatusEnum.InvalidParam;
        }
        return StatusEnum.ValidateSuccessed;
    }
}
