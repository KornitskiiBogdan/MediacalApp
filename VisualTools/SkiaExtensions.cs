using Avalonia.Media;
using SkiaSharp;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace VisualTools
{
    public static class SkiaExtensions
    {
        public static SKBitmap ToSKBitmap(this System.IO.Stream stream)
        {
            return SKBitmap.Decode(stream);
        }

        public static IImage? ToAvaloniaImage(this SKBitmap? bitmap)
        {
            if (bitmap is not null)
            {
                return new AvaloniaImage(bitmap);
            }
            return default;
        }

        public static SKBitmap ArrayToBitmap(byte[] pixelArray)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                int count = 0;
                while (count < pixelArray.Length)
                {
                    memStream.WriteByte(pixelArray[count++]);
                }

                return memStream.ToSKBitmap();
            }
        }

    }
}
