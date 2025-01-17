using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record PhoneNumber
{
    private const int DefaultLength = 10;
    private const string Pattern = @"^[0-9]{10}$";


    public PhoneNumber(string value) => Value = value;

    public static PhoneNumber? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLength)
        {
            return null;
        }

        return new PhoneNumber(value);
    }

    public String Value { get; init; }

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();


}
