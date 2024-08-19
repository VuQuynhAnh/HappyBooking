using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Realtime;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace HappyBookingCleanArchitectureServer.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ChatController : BaseController
{
    private readonly IAddMemberToGroupUseCase _addMemberToGroupUseCase;
    private readonly IGetMessageListUseCase _getMessageListUseCase;
    private readonly IGetMessagesByMessageIdUseCase _getMessagesByMessageIdUseCase;
    private readonly ILeaveChatGroupUseCase _leaveChatGroupUseCase;
    private readonly ISaveChatGroupUseCase _saveChatGroupUseCase;
    private readonly IGetChatGroupUseCase _getChatGroupUseCase;
    private readonly IGetListChatGroupByMemberUseCase _getListChatGroupByMemberUseCase;
    private readonly ISendMessageUseCase _sendMessageUseCase;
    private readonly IUpdateMessageUseCase _updateMessageUseCase;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(IAddMemberToGroupUseCase addMemberToGroupUseCase,
        IGetMessageListUseCase getMessageListUseCase,
        IGetMessagesByMessageIdUseCase getMessagesByMessageIdUseCase,
        ILeaveChatGroupUseCase leaveChatGroupUseCase,
        ISaveChatGroupUseCase saveChatGroupUseCase,
        IGetChatGroupUseCase getChatGroupUseCase,
        IGetListChatGroupByMemberUseCase getListChatGroupByMemberUseCase,
        ISendMessageUseCase sendMessageUseCase,
        IUpdateMessageUseCase updateMessageUseCase,
        IHubContext<ChatHub> hubContext,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _addMemberToGroupUseCase = addMemberToGroupUseCase;
        _getMessageListUseCase = getMessageListUseCase;
        _getMessagesByMessageIdUseCase = getMessagesByMessageIdUseCase;
        _leaveChatGroupUseCase = leaveChatGroupUseCase;
        _saveChatGroupUseCase = saveChatGroupUseCase;
        _getChatGroupUseCase = getChatGroupUseCase;
        _getListChatGroupByMemberUseCase = getListChatGroupByMemberUseCase;
        _sendMessageUseCase = sendMessageUseCase;
        _updateMessageUseCase = updateMessageUseCase;
        _hubContext = hubContext;
    }

    [HttpPost(APIName.AddMemberToGroup)]
    public async Task<ActionResult<AddMemberToGroupResponse>> AddMemberToGroup([FromBody] AddMemberToGroupRequest request)
    {
        var response = await _addMemberToGroupUseCase.AddMemberToGroup(UserId, request);
        return Ok(response);
    }

    [HttpPost(APIName.LeaveChatGroup)]
    public async Task<ActionResult<LeaveChatGroupResponse>> LeaveChatGroup([FromBody] LeaveChatGroupRequest request)
    {
        var response = await _leaveChatGroupUseCase.LeaveChatGroup(UserId, request);
        return Ok(response);
    }

    [HttpPost(APIName.SaveChatGroup)]
    public async Task<ActionResult<SaveChatGroupResponse>> SaveChatGroup([FromBody] SaveChatGroupRequest request)
    {
        var response = await _saveChatGroupUseCase.SaveChatGroup(UserId, request);
        return Ok(response);
    }

    [HttpPost(APIName.SendMessage)]
    public async Task<ActionResult<SendMessageResponse>> SendMessage([FromBody] SendMessageRequest request)
    {
        var response = await _sendMessageUseCase.SendMessage(UserId, request);

        // Broadcast the message using SignalR
        string jsonString = JsonSerializer.Serialize(response);
        await _hubContext.Clients.All.SendAsync(RealtimeConstant.ReceiveMessage, jsonString);
        return Ok(response);
    }

    [HttpPut(APIName.UpdateMessage)]
    public async Task<ActionResult<UpdateMessageResponse>> UpdateMessage([FromBody] UpdateMessageRequest request)
    {
        var response = await _updateMessageUseCase.UpdateMessage(UserId, request);
        return Ok(response);
    }

    [HttpGet(APIName.GetMessageList)]
    public async Task<ActionResult<GetMessageListResponse>> GetMessageList([FromQuery] GetMessageListRequest request)
    {
        var response = await _getMessageListUseCase.GetMessageList(UserId, request, _hubContext);
        return Ok(response);
    }

    [HttpGet(APIName.GetMessagesByMessageId)]
    public async Task<ActionResult<GetMessagesByMessageIdResponse>> GetMessagesByMessageId([FromQuery] GetMessagesByMessageIdRequest request)
    {
        var response = await _getMessagesByMessageIdUseCase.GetMessagesByMessageId(UserId, request);
        return Ok(response);
    }

    [HttpGet(APIName.GetChatGroup)]
    public async Task<ActionResult<GetChatGroupResponse>> GetChatGroup([FromQuery] GetChatGroupRequest request)
    {
        var response = await _getChatGroupUseCase.GetChatGroup(UserId, request);
        return Ok(response);
    }

    [HttpGet(APIName.GetListChatGroupByMember)]
    public async Task<ActionResult<GetListChatGroupByMemberResponse>> GetListChatGroupByMember([FromQuery] GetListChatGroupByMemberRequest request)
    {
        var response = await _getListChatGroupByMemberUseCase.GetListChatGroupByMember(UserId, request);
        return Ok(response);
    }
}
