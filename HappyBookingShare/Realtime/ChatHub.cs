using HappyBookingShare.Common;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingShare.Realtime;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync(RealtimeConstant.ReceiveMessage, user, message);
    }

    public async Task SendPrivateMessage(string user, string message, string connectionId)
    {
        await Clients.Client(connectionId).SendAsync(RealtimeConstant.ReceiveMessage, user, message);
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessageToGroup(string groupName, string user, string message)
    {
        await Clients.Group(groupName).SendAsync(RealtimeConstant.ReceiveMessage, user, message);
    }
}
