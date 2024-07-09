using System.Text.RegularExpressions;

namespace HappyBookingShare.Common;

public static class Validator
{
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneNumberPattern = @"^(0|\+84)(3[2-9]|5[2-9]|7[0-9]|8[1-9]|9[0-9])[0-9]{7}$";
        return Regex.IsMatch(phoneNumber, phoneNumberPattern);
    }

    public static bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
}
