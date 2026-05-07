using System.Globalization;
using System.Windows.Data;

namespace UniversityTransportSystem.Presentation.Converters;

[ValueConversion(typeof(bool), typeof(string))]
public class BoolToStatusConverter : IValueConverter
{
    public static BoolToStatusConverter Instance { get; } = new();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? "Active" : "Inactive";
        return "Unknown";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s)
            return s == "Active";
        return true;
    }
}
