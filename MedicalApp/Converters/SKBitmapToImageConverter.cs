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
            }

            if (bitmap == null || layoutable == null)
            {
                return null;
            }

            layoutable.Measure(new Size(width: double.MaxValue, height: double.MaxValue));
            layoutable.InvalidateMeasure();
            return bitmap.Resize(new SKSizeI((int)layoutable.DesiredSize.Width, (int)layoutable.DesiredSize.Height), SKFilterQuality.High).ToAvaloniaImage();
        }
    }
}
