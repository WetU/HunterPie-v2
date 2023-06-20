﻿using System;
using System.Globalization;
using System.Windows.Data;
using Converter = System.Convert;

namespace HunterPie.UI.Architecture.Converters;

public class SecondsToTimeString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string timeFormat = "mm\\:ss";
        double val = Converter.ToDouble(value);

        var span = TimeSpan.FromSeconds(val);

        if (parameter is string format)
            timeFormat = format;

        return span.ToString(timeFormat);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
