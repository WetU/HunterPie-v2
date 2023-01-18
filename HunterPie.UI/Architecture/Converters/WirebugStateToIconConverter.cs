using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;
using HunterPie.UI.Assets.Application;
using System;
using System.Globalization;
using System.Windows.Data;

namespace HunterPie.UI.Architecture.Converters;

public class WirebugStateToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WirebugState WIrebugState)
        {
            string iconName;

            switch (WIrebugState)
            {
                case WirebugState.None:
                    iconName = "ICON_WIREBUG";
                    break;
                case WirebugState.IceBlight:
                    iconName = "ICON_WIREBUG_ICE";
                    break;
                case WirebugState.RubyWirebug:
                    iconName = "ICON_WIREBUG_RUBY";
                    break;
                case WirebugState.GoldWirebug:
                    iconName = "ICON_WIREBUG_GOLD";
                    break;
                case WirebugState.WindMantle:
                    iconName = "ICON_WIREBUG_GREEN";
                    break;
                case WirebugState.Blocked:
                    iconName = "ICON_WIREBUG";
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