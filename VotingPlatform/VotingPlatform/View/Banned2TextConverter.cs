using System.Globalization;

namespace VotingPlatform.View;

public class Banned2TextConverter :IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return "";
        return ((bool)value) ? "Banned" : "Not banned";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}