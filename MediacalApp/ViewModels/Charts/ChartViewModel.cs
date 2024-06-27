using System.Collections.Generic;

namespace MediacalApp.ViewModels.Charts;
using SkiaSharp;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

public abstract class ChartViewModel : ViewModelBase
{
    public abstract LabelVisual Title { get; set; }
    public abstract ICollection<ISeries> Series { get; set; }
    public abstract Axis[] XAxes { get; set; }
}