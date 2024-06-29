using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace MediacalApp.Converters
{
    public class TypeConverters
    {
        public static FuncValueConverter<string, Bitmap> IconConverter { get; } =
            new(iconKey =>
            {
                return new Bitmap(AssetLoader.Open(new Uri($"avares://MediacalApp/Assets/{iconKey}")));
            });
    }
}
