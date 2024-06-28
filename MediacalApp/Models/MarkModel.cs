using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediacalApp.Models
{
    public class MarkModel
    {
        public MarkModel(float? upperValue, float? lowerValue)
        {
            UpperValue = upperValue;
            LowerValue = lowerValue;
        }

        public float? UpperValue { get; }
        public float? LowerValue { get; }

    }
}
