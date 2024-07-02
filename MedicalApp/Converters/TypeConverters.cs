using System;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace MedicalApp.Converters
{
    public class TypeConverters
    {
        public static FuncValueConverter<string, Bitmap> IconConverter { get; } =
            new(iconKey =>
            {
                return new Bitmap(AssetLoader.Open(new Uri($"avares://MedicalApp/Assets/{iconKey}")));
            });
    }
}
