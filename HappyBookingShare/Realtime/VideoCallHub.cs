using HappyBookingShare.Common;
using Microsoft.AspNetCore.SignalR;

namespace HappyBookingShare.Realtime;

public class VideoCallHub : Hub
{
    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync(RealtimeConstant.UserJoined, Context.ConnectionId);
    }

    public async Task SendOffer(string groupId, string offer, string senderConnectionId)
    {
        await Clients.GroupExcept(groupId, senderConnectionId).SendAsync(RealtimeConstant.ReceiveOffer, offer, senderConnectionId);
    }

    public async Task SendAnswer(string groupId, string answer, string senderConnectionId)
    {
        await Clients.Client(senderConnectionId).SendAsync(RealtimeConstant.ReceiveAnswer, answer);
    }

    public async Task SendCandidate(string groupId, string candidate, string senderConnectionId)
    {
        await Clients.GroupExcept(groupId, senderConnectionId).SendAsync(RealtimeConstant.ReceiveCandidate, candidate);
    }
}
