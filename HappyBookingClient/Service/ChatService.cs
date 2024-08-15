using Blazored.LocalStorage;
using HappyBookingClient.Service.IService;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using Microsoft.AspNetCore.Components;

namespace HappyBookingClient.Service;

public class ChatService : BaseApiService, IChatService
{
    public ChatService(HttpClient httpClient, ILocalStorageService localStorage, NavigationManager navigationManager) : base(httpClient, localStorage, navigationManager)
    {
    }

    /// <summary>
    /// AddMemberToGroupAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<AddMemberToGroupResponse?> AddMemberToGroupAsync(AddMemberToGroupRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.AddMemberToGroup}";
            var result = await SendAuthorizedRequestAsync<AddMemberToGroupResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// LeaveChatGroupAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<LeaveChatGroupResponse?> LeaveChatGroupAsync(LeaveChatGroupRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.LeaveChatGroup}";
            var result = await SendAuthorizedRequestAsync<LeaveChatGroupResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// SaveChatGroupAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SaveChatGroupResponse?> SaveChatGroupAsync(SaveChatGroupRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.SaveChatGroup}";
            var result = await SendAuthorizedRequestAsync<SaveChatGroupResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// SendMessageAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<SendMessageResponse?> SendMessageAsync(SendMessageRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.SendMessage}";
            var result = await SendAuthorizedRequestAsync<SendMessageResponse>(HttpMethod.Post, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// UpdateMessageAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<UpdateMessageResponse?> UpdateMessageAsync(UpdateMessageRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.UpdateMessage}";
            var result = await SendAuthorizedRequestAsync<UpdateMessageResponse>(HttpMethod.Put, queryUrl, request);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// GetMessageListAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetMessageListResponse?> GetMessageListAsync(GetMessageListRequest request)
    {
        try
        {
            string queryUrl = $"Chat/{APIName.GetMessageList}?ChatId={request.ChatId}&PageIndex={request.PageIndex}&PageSize={request.PageSize}";
            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                queryUrl += $"&KeyWord={request.KeyWord}";
            }
            var result = await SendAuthorizedRequestAsync<GetMessageListResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// GetMessagesByMessageIdAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetMessagesByMessageIdResponse?> GetMessagesByMessageIdAsync(GetMessagesByMessageIdRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.GetMessagesByMessageId}?MessageId={request.MessageId}";
            var result = await SendAuthorizedRequestAsync<GetMessagesByMessageIdResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// GetChatGroupAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetChatGroupResponse?> GetChatGroupAsync(GetChatGroupRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.GetChatGroup}?ChatId={request.ChatId}&MemberId={request.MemberId}";
            var result = await SendAuthorizedRequestAsync<GetChatGroupResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// GetListChatGroupByMemberAsync
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<GetListChatGroupByMemberResponse?> GetListChatGroupByMemberAsync(GetListChatGroupByMemberRequest request)
    {
        try
        {
            var queryUrl = $"Chat/{APIName.GetListChatGroupByMember}?MemberId={request.MemberId}&IsGroupChat={request.IsGroupChat}&PageIndex={request.PageIndex}&PageSize={request.PageSize}";
            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                queryUrl += $"&KeyWord={request.KeyWord}";
            }
            var result = await SendAuthorizedRequestAsync<GetListChatGroupByMemberResponse>(HttpMethod.Get, queryUrl);
            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    /// <summary>
    /// get token from local storage
    /// </summary>
    /// <returns></returns>
    async Task<string> IBaseApiService.GetTokenFromLocalStorageAsync()
    {
        return await GetTokenFromLocalStorageAsync();
    }
}
