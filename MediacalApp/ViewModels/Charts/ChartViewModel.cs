using System.Collections.Generic;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace MedicalApp.ViewModels.Charts;

public abstract class ChartViewModel : ViewModelBase
{
    public abstract LabelVisual Title { get; set; }
    public abstract ICollection<ISeries> Series { get; set; }
    public abstract Axis[] XAxes { get; set; }
}