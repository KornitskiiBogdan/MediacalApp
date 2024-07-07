using System.Collections.ObjectModel;
using System.Xml.Linq;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MedicalDatabase.Objects;
using SkiaSharp;

namespace LiveChartsWrapper
{

    public class MedicalValueComparer : IEqualityComparer<MedicalValue>, IComparer<MedicalValue>
    {
        public bool Equals(MedicalValue x, MedicalValue y)
        {
            return x.Date.Equals(y.Date);
        }

        public int GetHashCode(MedicalValue obj)
        {
            return 0;
        }

        public int Compare(MedicalValue x, MedicalValue y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }

    public class MedicalMarksGraphics
    {

        public MedicalMarksGraphics(MedicalReference reference, ObservableCollection<MedicalValue> values)
        {
            //var temp = values.Distinct(new MedicalValueComparer()).ToList();
            //temp.Sort(new MedicalValueComparer());
            Series = new ObservableCollection<ISeries>()
            {
                new LineSeries<MedicalValue>()
                {
                    Values = values,
                    Fill = null,
                    Stroke = new SolidColorPaint(new SKColor(28, 129, 176, 255), 2),
                    GeometrySize = 5,
                    GeometryStroke = new SolidColorPaint(new SKColor(19, 98, 135, 255), 1),
                    GeometryFill = new SolidColorPaint(new SKColor(28, 129, 176, 255), 1),
                    Mapping = (medicalValue, _) => new Coordinate(medicalValue.Date, medicalValue.Value)
                }
            };

            Sections = new RectangularSection[]
            {
                new RectangularSection()
                {
                    Yi = reference.LowerValue,
                    Yj = reference.UpperValue,
                    Fill = new SolidColorPaint(new SKColor(200, 255, 111, 122))
                }
            };

            MarginFrame = new DrawMarginFrame();
        }

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd MMMM yyyy"))
        };

        public ObservableCollection<ISeries> Series { get; set; }

        public DrawMarginFrame MarginFrame { get; set; }

        public RectangularSection[] Sections { get; set; } 
    }
}
