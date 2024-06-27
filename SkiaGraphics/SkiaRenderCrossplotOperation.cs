using System.Diagnostics;
using Avalonia;
using Avalonia.Media;
using Avalonia.Skia;
using SkiaSharp;

namespace SkiaGraphics
{
    public class SkiaRenderCrossplotOperation : SkiaDrawOperationBase
    {
        private readonly SKPoint[] _points; 
        private SkiaRenderAxisOperation _axis;
        private SkiaRenderCrossplotOperation(Rect bounds, int lengthPoints)
        {
            _axis = new SkiaRenderAxisOperation(bounds);
            _points = new SKPoint[lengthPoints];
            Bounds = new Rect(bounds.Size);
        }

        public SkiaRenderCrossplotOperation(Rect bounds, Point[] points) : this(bounds, points.Length)
        {
            for (int i = 0; i < points.Length; i++)
            {
                _points[i] = new SKPoint((float)points[i].X, (float)points[i].Y);
            }
        }

        public SkiaRenderCrossplotOperation(Rect bounds, float[] pointsX, float[] pointsY) : this(bounds, pointsX.Length)
        {
            Debug.Assert(pointsX.Length != pointsY.Length, "pointsX.Length != pointsY.Length");
            for (int i = 0; i < pointsX.Length; i++)
            {
                _points[i] = new SKPoint(pointsX[i], pointsY[i]);
            }
        }

        public override Rect Bounds { get; }
        
        
        protected override void Render(SKCanvas canvas)
        {
            canvas.Clear(new SKColor(255, 255, 255));
            _axis.RenderAxis(canvas);


            using var pointPaint = new SKPaint
            {
                Color = new SKColor(200, 10, 0),
                StrokeWidth = 10,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.StrokeAndFill
            };

            canvas.DrawPoints(SKPointMode.Points, _points, pointPaint);

            using var linePaint = new SKPaint
            {
                Color = Colors.Black.ToSKColor(),
                StrokeWidth = 1,
                IsAntialias = true
            };
            for (int i = 0; i < _points.Length-1; i++)
            {
                canvas.DrawLine(_points[i], _points[i+1], linePaint);
            }
        }
    }
}
