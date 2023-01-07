using HunterPie.UI.Assets.Application;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class PlayerConditionToWirebugColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string color;

        switch ((int)value)
        {
            case 0:
                color = "#FF00D6F7";
                break;
            case 1:
                color = "#FF00D600";
                break;
            case 2:
                color = "#FFF8A700";
                break;
            case 3:
                color = "#FFFE4A0D";
                break;
            case 4:
                color = "#FF6A94BE";
                break;
            default:
                return "#FF00D6F7";
        }

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}