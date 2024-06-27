using Avalonia;
using Avalonia.Media;
using Avalonia.Skia;
using SkiaSharp;

namespace SkiaGraphics;

internal class SkiaRenderAxisOperation : SkiaDrawOperationBase
{
    private readonly int _thicknessScale = 10;
    private readonly SKColor GrayColor = Colors.Gray.ToSKColor();

    public SkiaRenderAxisOperation(Rect bounds)
    {
        Bounds = new Rect(bounds.Size);
    }

    public override Rect Bounds { get; }


    public void RenderAxis(SKCanvas canvas)
    {
        Render(canvas);
    }

    protected override void Render(SKCanvas canvas)
    {
        #region Закрашиваем белый фон

        int x0 = 0;
        int x1 = _thicknessScale;
        int x2 = (int)Bounds.Size.Width - _thicknessScale;
        int x3 = (int)Bounds.Size.Width;
        int y0 = 0;
        int y1 = _thicknessScale;
        int y2 = (int)Bounds.Size.Height - _thicknessScale;
        int y3 = (int)Bounds.Size.Height;
        var vertices = new SKPoint[]
        {
            new SKPoint(x0, y0),new SKPoint(x1, y1),new SKPoint(x3, y0),new SKPoint(x2, y1),new SKPoint(x3, y3),
            new SKPoint(x2, y2),new SKPoint(x0, y3),new SKPoint(x1, y2),new SKPoint(x0, y0),new SKPoint(x1, y1),
        };
        var verticesColor = new SKColor[10];
        
        for (int i = 0; i < 10; i++)
        {
            verticesColor[i] = Colors.White.ToSKColor();
        }

        using var verticesPaint = new SKPaint();
        canvas.DrawVertices(SKVertexMode.TriangleStrip, vertices, verticesColor, verticesPaint);

        #endregion

        //#region Рисуем зубчики

        //float x01 = (x0 + x1) / 2;
        //float x23 = (x2 + x3) / 2;
        //float y01 = (y0 + y1) / 2;
        //float y23 = (y2 + y3) / 2;

        //float d = 0.5f;

        //using var lineAxisYPaint = new SKPaint();
        //lineAxisYPaint.Color = GrayColor;
        //lineAxisYPaint.StrokeWidth = 1;
        //foreach (var y in _drawingTools.GetTicksHorizontal(true))
        //{
        //    //if (aPlaceAxisY == EOnLeftOnRight.OnLeft)
        //    //{
        //    //    canvas.DrawLine(x01, y + d, x1, y + d, gridY.Width2);
        //    //}
        //    //else if (aPlaceAxisY == EOnLeftOnRight.OnRight)
        //    //{
        //    //    drawing.AddGoodLine(x2, y + d, x23, y + d, gridY.Width2);
        //    //}
        //    //else
        //    //{
            
        //        canvas.DrawLine(x01, y + d, x1, y + d, lineAxisYPaint);
        //        canvas.DrawLine(x2, y + d, x23, y + d, lineAxisYPaint);
        //    //}
        //}

        //foreach (var x in _drawingTools.GetTicksVertical(true))
        //{
        //    //if (aPlaceAxisX == EOnTopOnBottom.OnTop)
        //    //{
        //    //    drawing.AddGoodLine(x + d, y01, x + d, y1, gridX.Width2);
        //    //}
        //    //else if (aPlaceAxisX == EOnTopOnBottom.OnBottom)
        //    //{
        //    //    drawing.AddGoodLine(x + d, y2, x + d, y23, gridX.Width2);
        //    //}
        //    //else
        //    //{
        //    canvas.DrawLine(x + d, y01, x + d, y1, lineAxisYPaint);
        //    canvas.DrawLine(x + d, y2, x + d, y23, lineAxisYPaint);
        //    //}
        //}

        //foreach (var y in _drawingTools.GetTicksHorizontal(false))
        //{
        //    //if (aPlaceAxisY == EOnLeftOnRight.OnLeft)
        //    //{
        //    //    drawing.AddGoodLine(x0, y + d, x1, y + d, gridY.Width1);
        //    //}
        //    //else if (aPlaceAxisY == EOnLeftOnRight.OnRight)
        //    //{
        //    //    drawing.AddGoodLine(x2, y + d, x3, y + d, gridY.Width1);
        //    //}
        //    //else
        //    //{
        //    canvas.DrawLine(x0, y + d, x1, y + d, lineAxisYPaint);
        //    canvas.DrawLine(x2, y + d, x3, y + d, lineAxisYPaint);
        //    //}
        //}
        
        //foreach (var x in _drawingTools.GetTicksVertical(false))
        //{
        //    //if (aPlaceAxisX == EOnTopOnBottom.OnTop)
        //    //{
        //    //    drawing.AddGoodLine(x + d, y0, x + d, y1, gridX.Width1);
        //    //}
        //    //else if (aPlaceAxisX == EOnTopOnBottom.OnBottom)
        //    //{
        //    //    drawing.AddGoodLine(x + d, y2, x + d, y3, gridX.Width1);
        //    //}
        //    //else
        //    //{
        //    canvas.DrawLine(x + d, y0, x + d, y1, lineAxisYPaint);
        //    canvas.DrawLine(x + d, y2, x + d, y3, lineAxisYPaint);
        //    //}
        //}
        

        //#endregion

        #region Рисуем линии осей

        const int hw = 1;
        const int w = 2 * hw;
        using var linePaint = new SKPaint();
        linePaint.Color = GrayColor;
        linePaint.StrokeWidth = 2 * hw;
        canvas.DrawLine(x1 - w, y1 - hw, x2 + w, y1 - hw, linePaint);
        canvas.DrawLine(x1 - w, y2 + hw, x2 + w, y2 + hw, linePaint);
        canvas.DrawLine(x1 - hw, y1 - w, x1 - hw, y2 + w, linePaint);
        canvas.DrawLine(x2 + hw, y1 - w, x2 + hw, y2 + w, linePaint);

        #endregion
    }
}