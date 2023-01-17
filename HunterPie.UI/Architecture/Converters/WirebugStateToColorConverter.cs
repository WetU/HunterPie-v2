using System;
using System.Globalization;
using System.Windows.Data;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

namespace HunterPie.UI.Architecture.Converters;

public class WirebugStateToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WirebugState WirebugState)
        {
            string color;

            switch (WirebugState)
            {
                case WirebugState.None:
                    color = "#FF00D6F7";
                    break;
                case WirebugState.IceBlight:
                    color = "#FF6A94BE";
                    break;
                case WirebugState.RubyWirebug:
                    color = "#FFF8A700";
                    break;
                case WirebugState.GoldWirebug:
                    color = "#FFFE4A0D";
                    break;
                case WirebugState.WindMantle:
                    color = "#FF00D600";
                    break;
                case WirebugState.Blocked:
                    color = "#FF00D6F7";
                    break;
                default:
                    return "#FF00D6F7";
            }

            return color;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}