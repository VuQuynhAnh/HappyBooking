namespace HappyBookingShare.Constant;

public static class MessageConstant
{
    /// <summary>
    /// get message from status enum
    /// </summary>
    /// <param name="statusEnum"></param>
    /// <returns></returns>
    public static string GetMessage(StatusEnum statusEnum)
    {
        return statusEnum switch
        {
            StatusEnum.Successed => Successed,
            StatusEnum.InvalidParam => InvalidParam,
            StatusEnum.InvalidEmail => InvalidEmail,
            StatusEnum.InvalidPhoneNumber => InvalidPhoneNumber,
            StatusEnum.InvalidCitizenIdentificationNumber => InvalidCitizenIdentificationNumber,
            StatusEnum.InvalidPassword => InvalidPassword,
            StatusEnum.RefreshTokenIsNotExist => RefreshTokenIsNotExist,
            _ => Failed,
        };
    }

    private static string Successed = "Api successed.";
    private static string Failed = "Api failed.";
    private static string InvalidParam = "Invalid param.";
    private static string RefreshTokenIsNotExist = "Refresh token is not exist.";
    private static string InvalidEmail = "Invalid email.";
    private static string InvalidPhoneNumber = "Invalid phone number.";
    private static string InvalidCitizenIdentificationNumber = "Invalid Citizen Identification Number.";
    private static string InvalidPassword = "Invalid password.";
}
