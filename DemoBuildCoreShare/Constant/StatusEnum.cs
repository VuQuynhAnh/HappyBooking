namespace HappyBookingShare.Constant;

public enum StatusEnum : byte
{
    Successed = 0,
    Failed = 1,
    InvalidParam = 2,
    InvalidEmail = 3,
    InvalidPhoneNumber = 4,
    InvalidCitizenIdentificationNumber = 5,
    InvalidPassword = 6,
    RefreshTokenIsNotExist = 7,
}
