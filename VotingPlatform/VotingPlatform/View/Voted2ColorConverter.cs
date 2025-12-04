using System.Globalization;

namespace VotingPlatform.View;

public class Voted2ColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Colors.Transparent;
        return ((bool)value) ? Colors.BlueViolet : Colors.DimGray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}