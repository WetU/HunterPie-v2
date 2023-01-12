using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class WirebugConditionToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string color;

        switch (value.ToString())
        {
            case "None":
                color = "#FF00D6F7";
                break;
            case "WindMantle":
                color = "#FF00D600";
                break;
            case "MarionetteTypeGold":
                color = "#FFF8A700";
                break;
            case "MarionetteTypeRuby":
                color = "#FFFE4A0D";
                break;
            case "IceL":
                color = "#FF6A94BE";
                break;
            default:
                return "#FF00D6F7";
        }

        return color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}