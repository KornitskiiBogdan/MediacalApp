using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalDatabase.Objects
{
    public class MedicalReference : MedicalNamedElementBase
    {
        private string? _value;
        private Int64 _parentId;

        public MedicalReference() : base()
        {
        }

        public MedicalReference(Int64 id, Int64 parentId, string name, string? value) : base(id, name)
        {
            _value = value;
            _parentId = parentId;
            ParseReferenceToInt(value);
        }

        public string? Value
        {
            get => _value;
            set => _value = value;
        }

        public Int64 ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }

        public float? LowerValue { get; set; }

        public float? UpperValue { get; set; }

        private void ParseReferenceToInt(string value)
        {
            //TODO Вроде бы есть более простой вариант чем такой
            if (Regex.IsMatch(value, @"\s*<\s*\w*"))
            {
                LowerValue = 0;
                UpperValue = Convert.ToInt32(value.Replace("<", string.Empty).Replace(" ", string.Empty));
            }
            else if (Regex.IsMatch(value, @"\s*\w*\s*-\s*\w*\s*"))
            {
                Debug.Write(value);
                var values = value.Replace(" ", string.Empty).Split("-");
                if (float.TryParse(values.First(), CultureInfo.InvariantCulture, out float res1))
                {
                    LowerValue = res1;
                }
                if (float.TryParse(values.Last(), CultureInfo.InvariantCulture, out float res2))
                {
                    UpperValue = res2;
                }

                if (UpperValue == null)
                {
                    return;
                }

                if (LowerValue > UpperValue)
                {
                    (LowerValue, UpperValue) = (UpperValue, LowerValue);
                }
            }
        }
    }
}
