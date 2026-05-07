using System.Globalization;
using System.Windows.Data;

namespace UniversityTransportSystem.Presentation.Converters;

[ValueConversion(typeof(bool), typeof(string))]
public class GenderConverter : IValueConverter
{
    public static GenderConverter Instance { get; } = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? "Male" : "Female";
        return "Unknown";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s)
            return s == "Male";
        return true;
    }
}
