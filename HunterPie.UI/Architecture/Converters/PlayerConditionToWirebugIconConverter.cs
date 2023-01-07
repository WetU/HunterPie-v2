using HunterPie.UI.Assets.Application;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class PlayerConditionToWirebugIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string iconName;

        switch ((string)value)
        {
            case "None":
                iconName = "ICON_WIREBUG";
                break;
            case "WindMantle":
                iconName = "ICON_WIREBUG_GREEN";
                break;
            case "GoldBug":
                iconName = "ICON_WIREBUG_GOLD";
                break;
            case "RubyBug":
                iconName = "ICON_WIREBUG_RUBY";
                break;
            case "IceBlight":
                iconName = "ICON_WIREBUG_ICE";
                break;
            default:
                return Resources.Icon("ICON_WIREBUG");
        }

        return Resources.Icon(iconName);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}