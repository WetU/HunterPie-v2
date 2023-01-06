using HunterPie.UI.Assets.Application;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class PlayerConditionToWirebugIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int num = (int)value;
        string iconName;

        switch (num)
        {
            case 0:
                iconName = "ICON_WIREBUG";
                break;
            case 1:
                iconName = "ICON_WIREBUG_GREEN";
                break;
            case 2:
                iconName = "ICON_WIREBUG_GOLD";
                break;
            case 3:
                iconName = "ICON_WIREBUG_RUBY";
                break;
            case 4:
                iconName = "ICON_WIREBUG_ICE";
                break;
            default:
                iconName = "ICON_WIREBUG";
                return Resources.Icon(iconName);
        }

        return Resources.Icon(iconName);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}