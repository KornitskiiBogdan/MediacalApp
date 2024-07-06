using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;

namespace MedicalApp.Tools
{
    public class GradientBrushControl
    {
        public static GradientBrushControl Instance { get; } = new GradientBrushControl();

        public LinearGradientBrush RedToGreenToRed { get; } = new LinearGradientBrush()
        {
            StartPoint = new RelativePoint(0, 100, RelativeUnit.Relative),
            EndPoint = new RelativePoint(100, 100, RelativeUnit.Relative),
            GradientStops = new GradientStops
            {
                new GradientStop(Colors.IndianRed, 0),
                new GradientStop(Colors.Yellow, 0.15),
                new GradientStop(Colors.LimeGreen, 0.5),
                new GradientStop(Colors.Yellow, 0.85),
                new GradientStop(Colors.IndianRed, 1)
            }
        };
    }
}
