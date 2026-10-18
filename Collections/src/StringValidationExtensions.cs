using System.Text.RegularExpressions;

namespace CollectionsApp;

public static class StringValidationExtensions
{
    // 010 / 011 / 012 / 015 + 8 digits, written with a leading 0 or with +20 instead.
    // [0-9] is used instead of \d because in .NET \d also matches non-English digits.
    // \z (not $) so a trailing newline is not accepted.
    private static readonly Regex PhonePattern =
        new(@"^(?:\+20|0)1[0125][0-9]{8}\z", RegexOptions.Compiled);

    // Exactly 14 digits, first digit 2 or 3.
    private static readonly Regex NationalIdPattern =
        new(@"^[23][0-9]{13}\z", RegexOptions.Compiled);

    public static bool IsValidEgyptianPhone(this string? value)
    {
        // Regex.IsMatch(null) would throw, so null is handled first (R4).
        // Empty and whitespace-only strings simply do not match the pattern.
        if (value is null)
            return false;

        return PhonePattern.IsMatch(value);
    }

    public static bool IsValidEgyptianNationalId(this string? value)
    {
        if (value is null)
            return false;

        return NationalIdPattern.IsMatch(value);
    }
}
