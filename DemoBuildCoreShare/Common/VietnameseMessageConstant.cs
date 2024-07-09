namespace HappyBookingShare.Common;

public static class VietnameseMessageConstant
{

    /// <summary>
    /// get Vietnamese message from status enum
    /// </summary>
    /// <param name="statusEnum"></param>
    /// <returns></returns>
    public static string GetMessage(StatusEnum statusEnum)
    {
        return statusEnum switch
        {
            StatusEnum.Successed => Successed,
            StatusEnum.InvalidParam => InvalidParam,
            StatusEnum.ExistEmail => ExistEmail,
            StatusEnum.ExistPhoneNumber => ExistPhoneNumber,
            StatusEnum.ExistCitizenIdentificationNumber => ExistCitizenIdentificationNumber,
            StatusEnum.InvalidPassword => InvalidPassword,
            StatusEnum.RefreshTokenIsNotExist => RefreshTokenIsNotExist,
            _ => Failed,
        };
    }

    private static string Successed = "Thành công.";
    private static string Failed = "Thất bại.";
    private static string InvalidParam = "Các tham số không hợp lệ, hãy nhập lại!";
    private static string RefreshTokenIsNotExist = "Refresh token is not exist, please try again!";
    private static string ExistEmail = "Email đã tồn tại, hãy nhập lại!";
    private static string ExistPhoneNumber = "Số điện thoại đã tồn tại, hãy nhập lại!";
    private static string ExistCitizenIdentificationNumber = "Mã căn cước công dân đã tồn tại, hãy nhập lại!";
    private static string InvalidPassword = "Mật khẩu không hợp lệ.";
}
