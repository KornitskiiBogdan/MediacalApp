using Avalonia.Data.Converters;
using MedicalApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using SkiaSharp;
using Avalonia.Media.Imaging;
using System.IO;
using VisualTools;

namespace MedicalApp.Converters
{
    public class SkBitmapToImageConverter
    {
        public static FuncValueConverter<SKBitmap, Image> ImageConverter { get; } =
            new(skiaBitmap => new Image {Source = new AvaloniaImage(skiaBitmap)});
    }
}
