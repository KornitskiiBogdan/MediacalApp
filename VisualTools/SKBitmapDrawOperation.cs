using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace VisualTools;

public class SkBitmapDrawOperation : ICustomDrawOperation
{
    public Rect Bounds { get; set; }

    public SKBitmap? Bitmap { get; init; }

    public void Dispose()
    {
        Bitmap?.Dispose();
    }

    public bool Equals(ICustomDrawOperation? other) => false;

    public bool HitTest(Point p) => Bounds.Contains(p);

    public void Render(ImmediateDrawingContext context)
    {
        if (Bitmap is { } bitmap && context.PlatformImpl.GetFeature<ISkiaSharpApiLeaseFeature>() is { } leaseFeature)
        {
            ISkiaSharpApiLease lease = leaseFeature.Lease();
            using (lease)
            {
                lease.SkCanvas.DrawBitmap(bitmap, SKRect.Create((float)Bounds.X, (float)Bounds.Y, 
                    (float)Bounds.Width, (float)Bounds.Height));
            }
        }
    }
}