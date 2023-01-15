using System;
using System.Globalization;
using System.Windows.Data;
using HunterPie.Integrations.Datasources.MonsterHunterRise.Entity.Enums;

namespace HunterPie.UI.Architecture.Converters;

public class WirebugConditionToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is WirebugConditions WirebugCondition)
        {
            string color;

            switch (WirebugCondition)
            {
                case WirebugConditions.None:
                    color = "#FF00D6F7";
                    break;
                case WirebugConditions.ABN_WINDMANTLE:
                    color = "#FF00D600";
                    break;
                case WirebugConditions.ABN_RUBY_WIREBUG:
                    color = "#FFF8A700";
                    break;
                case WirebugConditions.ABN_GOLD_WIREBUG:
                    color = "#FFFE4A0D";
                    break;
                case WirebugConditions.ABN_ICE:
                    color = "#FF6A94BE";
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