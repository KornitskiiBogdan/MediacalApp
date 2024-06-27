using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace SkiaGraphics
{
    public abstract class SkiaDrawOperationBase : ICustomDrawOperation
    {
        public abstract Rect Bounds { get; }

        public virtual void Dispose()
        {
        }

        public virtual bool Equals(ICustomDrawOperation? other) => Equals(this, other);

        public virtual bool HitTest(Point p) => new Rect(Bounds.Size).Contains(p);

        public void Render(ImmediateDrawingContext context)
        {
            var skiaFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            using var lease = skiaFeature?.Lease();
            if (lease is null)
            {
                return;
            }

            Render(lease.SkCanvas);
        }

        protected abstract void Render(SKCanvas canvas);
    }
}
