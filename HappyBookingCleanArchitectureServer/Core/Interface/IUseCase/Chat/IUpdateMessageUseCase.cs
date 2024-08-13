using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IUpdateMessageUseCase
{
    Task<UpdateMessageResponse> UpdateMessage(long userId, UpdateMessageRequest request);
}
