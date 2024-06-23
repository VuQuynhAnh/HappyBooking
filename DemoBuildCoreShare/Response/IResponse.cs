using HappyBookingShare.Response.Status;

namespace HappyBookingShare.Response;

public interface IResponse<T>
{
    T Data { get; }

    string Message { get; }

    CommonStatus Status { get; }
}
