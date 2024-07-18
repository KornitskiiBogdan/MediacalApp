using Avalonia;
using Avalonia.Media;
using SkiaSharp;

namespace VisualTools
{
    public class AvaloniaImage : IImage, IDisposable
    {
        private readonly SKBitmap? _source;
        SkBitmapDrawOperation? _drawImageOperation;

        public AvaloniaImage(SKBitmap? source)
        {
            _source = source;
            if (source?.Info.Size is SKSizeI size)
            {
                Size = new(size.Width, size.Height);
            }
        }

        public Size Size { get; }

        public void Dispose() => _source?.Dispose();

        public void Draw(DrawingContext context, Rect sourceRect, Rect destRect)
        {
            if (_drawImageOperation is null)
            {
                _drawImageOperation = new SkBitmapDrawOperation()
                {
                    Bitmap = _source,
                };
            };
            _drawImageOperation.Bounds = sourceRect;
            context.Custom(_drawImageOperation);
        }
    }
}
