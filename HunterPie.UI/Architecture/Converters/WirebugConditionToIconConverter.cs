using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;
using HunterPie.UI.Assets.Application;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class WirebugConditionToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WirebugConditions WIrebugCondition)
        {
            string iconName;

            switch (WIrebugCondition)
            {
                case WirebugConditions.None:
                    iconName = "ICON_WIREBUG";
                    break;
                case WirebugConditions.WindMantle:
                    iconName = "ICON_WIREBUG_GREEN";
                    break;
                case WirebugConditions.MarionetteTypeGold:
                    iconName = "ICON_WIREBUG_GOLD";
                    break;
                case WirebugConditions.MarionetteTypeRuby:
                    iconName = "ICON_WIREBUG_RUBY";
                    break;
                case WirebugConditions.IceL:
                    iconName = "ICON_WIREBUG_ICE";
                    break;
                default:
                    return Resources.Icon("ICON_WIREBUG");
            }

            return Resources.Icon(iconName);
        }

        return null;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}