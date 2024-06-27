namespace HappyBookingShare.Common;

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
            StatusEnum.ExistEmail => ExistEmail,
            StatusEnum.ExistPhoneNumber => ExistPhoneNumber,
            StatusEnum.ExistCitizenIdentificationNumber => ExistCitizenIdentificationNumber,
            StatusEnum.InvalidPassword => InvalidPassword,
            StatusEnum.RefreshTokenIsNotExist => RefreshTokenIsNotExist,
            _ => Failed,
        };
    }

    private static string Successed = "Api successed.";
    private static string Failed = "Api failed.";
    private static string InvalidParam = "Invalid param, please try again!";
    private static string RefreshTokenIsNotExist = "Refresh token is not exist, please try again!";
    private static string ExistEmail = "Email is exist, please try again!";
    private static string ExistPhoneNumber = "Phone number is exist, please try again!";
    private static string ExistCitizenIdentificationNumber = "Citizen Identification Number is exist, please try again!";
    private static string InvalidPassword = "Invalid password.";
}
