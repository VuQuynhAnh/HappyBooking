using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface ISendMessageUseCase
{
    Task<SendMessageResponse> SendMessage(long userId, SendMessageRequest request);
}
