using Avalonia.Media;
using SkiaSharp;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace VisualTools
{
    public static class SkiaExtensions
    {
        public static IImage? ToAvaloniaImage(this SKBitmap? bitmap)
        {
            if (bitmap is not null)
            {
                return new AvaloniaImage(bitmap);
            }
            return default;
        }

        public static SKBitmap ArrayToBitmap(int widht, int height, byte[] pixelArray)
        {
            var bitmap = new SKBitmap();

            var gcHandle = GCHandle.Alloc(pixelArray, GCHandleType.Pinned);

            var info = new SKImageInfo(widht, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul);

            bitmap.InstallPixels(info, gcHandle.AddrOfPinnedObject(), info.RowBytes, delegate { gcHandle.Free(); }, null);

            return bitmap;
        }

    }
}
