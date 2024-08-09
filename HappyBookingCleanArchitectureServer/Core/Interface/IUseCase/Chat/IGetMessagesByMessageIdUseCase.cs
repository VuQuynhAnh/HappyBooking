using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;

namespace HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;

public interface IGetMessagesByMessageIdUseCase
{
    Task<GetMessagesByMessageIdResponse> GetMessagesByMessageId(long userId, GetMessagesByMessageIdRequest request);
}
