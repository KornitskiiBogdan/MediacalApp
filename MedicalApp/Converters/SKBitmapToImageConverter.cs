using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Controls;
using Avalonia.Media;
using SkiaSharp;
using VisualTools;
using Avalonia.Layout;
using Microsoft.Extensions.Primitives;

namespace MedicalApp.Converters
{
    public class SkBitmapToImageConverter : IMultiValueConverter
    {
        public static FuncValueConverter<SKBitmap, IImage?> ImageConverter { get; } =
            new(skiaBitmap => skiaBitmap.Resize(new SKSizeI(64, 64), SKFilterQuality.High).ToAvaloniaImage());

        public static readonly SkBitmapToImageConverter Instance = new();


        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            SKBitmap? bitmap = null;
            Layoutable? layoutable = null;
            Rect? bounds = null;

            foreach (var value in values)
            {
                if (value is SKBitmap b)
                {
                    bitmap = b;
                }
                else if (value is Layoutable l)
                {
                    layoutable = l;
                }
                else if (value is Rect r)
                {
                    bounds = r;
                }
            }

            if (bitmap == null || layoutable == null)
            {
                return null;
            }
            
            return new Image() { Source = bitmap.Resize(new SKSizeI((int)(bounds?.Width ?? 0), (int)(bounds?.Height ?? 0)), 
                SKFilterQuality.High).ToAvaloniaImage(),
                Width = bounds?.Width ?? 0,
                Height = bounds?.Height ?? 0,
            };
        }
    }
}
