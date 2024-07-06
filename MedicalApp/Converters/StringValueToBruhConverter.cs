using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using MedicalApp.Tools;
using MedicalApp.ViewModels;

namespace MedicalApp.Converters
{
    public class StringValueToBrushConverter : IMultiValueConverter
    {
        public static readonly StringValueToBrushConverter Instance = new();

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            string? stringValue = null;
            Border border = null;
            foreach (var value in values)
            {
                if (value is string s)
                {
                    stringValue = s;
                }
                else if (value is Border b)
                {
                    border = b;
                }
            }

            if (stringValue == null || border == null)
            {
                return new SolidColorBrush();
            }

            var markViewModel = border.DataContext as MarkViewModel;
            if (border.BorderBrush is LinearGradientBrush linearGradientBrush && markViewModel != null)
            {
                var lowerValue = markViewModel.CurrentReference.LowerValue;
                var upperValue = markViewModel.CurrentReference.UpperValue;
                if(lowerValue != null && upperValue != null && float.TryParse(stringValue, out float fValue)) 
                {
                    var k = (0.5) / (upperValue - lowerValue) ?? 1;
                    var b = (upperValue * 0.25 - lowerValue * 0.75) / (upperValue - lowerValue) ?? 0;
                    return new SolidColorBrush(linearGradientBrush.GradientStops.GetColorByOffset(k * fValue + b));
                }
                
            }

            return new SolidColorBrush();
        }
    }
}
