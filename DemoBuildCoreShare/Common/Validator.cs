using System.Text.RegularExpressions;

namespace HappyBookingShare.Common;

public static class Validator
{
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneNumberPattern = @"^\+?[1-9]\d{1,14}$";
        return Regex.IsMatch(phoneNumber, phoneNumberPattern);
    }

    public static bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
}
