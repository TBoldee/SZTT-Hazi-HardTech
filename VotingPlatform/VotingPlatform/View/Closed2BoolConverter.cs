using System.Globalization;
using VotingPlatform.Model;

namespace VotingPlatform.View;

public class Closed2BoolConverter :IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Colors.Transparent;
        var status = (PollStatus)value;
        return status == PollStatus.OPEN;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}