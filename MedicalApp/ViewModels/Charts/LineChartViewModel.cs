using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace MedicalApp.ViewModels.Charts
{
    public sealed class LineChartViewModel : ChartViewModel
    {
        private readonly ObservableCollection<ObservableValue> _observableValues;

        public LineChartViewModel()
        {
            _observableValues = new ObservableCollection<ObservableValue>(){new ObservableValue(1), new ObservableValue(2), new ObservableValue(3), 
                new ObservableValue(4), new ObservableValue(5)};

            Title = new LabelVisual()
            {
                Text = "My chart",
                TextSize = 20,
                Paint = new SolidColorPaint(SKColors.Blue)
            };

            Series = new ObservableCollection<ISeries>()
            {
                new LineSeries<ObservableValue>()
                {
                    DataLabelsSize = 10,
                    DataLabelsPaint = new SolidColorPaint(SKColors.Red),
                    DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                    DataLabelsFormatter = (point) => point.Coordinate.PrimaryValue.ToString("C2"),
                    Values = _observableValues,
                    LineSmoothness = 1,
                    Stroke = new SolidColorPaint(SKColors.Green),
                    Fill = null
                }
            };

            XAxes = new Axis[] { new Axis() };

        }

        public override LabelVisual Title { get; set; } 

        public override ICollection<ISeries> Series { get; set; }
        
        public override Axis[] XAxes { get; set; }
    }
}
