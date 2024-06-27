namespace HappyBookingShare.Common;

public enum StatusEnum : byte
{
    Successed = 0,
    Failed = 1,
    InvalidParam = 2,
    ExistEmail = 3,
    ExistPhoneNumber = 4,
    ExistCitizenIdentificationNumber = 5,
    InvalidPassword = 6,
    RefreshTokenIsNotExist = 7,
}
