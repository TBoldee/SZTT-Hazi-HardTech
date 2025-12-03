using System.Globalization;
using VotingPlatform.Model;

namespace VotingPlatform.View;

public class Closed2ColorConverter :IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Colors.Transparent;
        var status = (PollStatus)value;
        if (status == PollStatus.CLOSED)
        {
            return Colors.Red;
        }
        else
        {
            return Colors.DimGray;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}