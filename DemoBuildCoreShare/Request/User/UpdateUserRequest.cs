namespace HappyBookingShare.Request.User;

public class UpdateUserRequest : RegisterUserRequest
{
    public long UserId { get; set; }
}
