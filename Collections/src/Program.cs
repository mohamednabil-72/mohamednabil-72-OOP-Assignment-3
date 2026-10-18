using CollectionsApp;

// (input, method name, expected result)
var cases = new (string? Input, string Method, bool Expected)[]
{
    // Table from the assignment
    ("01012345678",    "IsValidEgyptianPhone",      true),
    ("+201512345678",  "IsValidEgyptianPhone",      true),
    ("01312345678",    "IsValidEgyptianPhone",      false), // 013 is not a valid prefix
    ("0101234567",     "IsValidEgyptianPhone",      false), // only 10 digits
    ("0101234567a",    "IsValidEgyptianPhone",      false), // contains a letter
    ("29901011234567", "IsValidEgyptianNationalId", true),
    ("19901011234567", "IsValidEgyptianNationalId", false), // must start with 2 or 3
    ("2990101123456",  "IsValidEgyptianNationalId", false), // only 13 digits

    // R4: null, empty and spaces must return false and never throw
    (null,             "IsValidEgyptianPhone",      false),
    ("",               "IsValidEgyptianPhone",      false),
    ("   ",            "IsValidEgyptianPhone",      false),
    (null,             "IsValidEgyptianNationalId", false),
    ("",               "IsValidEgyptianNationalId", false),
    ("   ",            "IsValidEgyptianNationalId", false),
};

int failures = 0;

foreach (var (input, method, expected) in cases)
{
    bool actual = method == "IsValidEgyptianPhone"
        ? input.IsValidEgyptianPhone()
        : input.IsValidEgyptianNationalId();

    string shown = input is null ? "null" : $"\"{input}\"";
    string verdict = actual == expected ? "OK  " : "FAIL";
    if (actual != expected) failures++;

    Console.WriteLine($"{verdict} {shown,-18} {method,-27} -> {actual,-5} (expected {expected})");
}

Console.WriteLine();
Console.WriteLine(failures == 0 ? "All results match the expected values." : $"{failures} result(s) did not match!");
