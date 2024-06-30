using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Objects
{
    public class MedicalValue : MedicalElementBase
    {
        private double _value;
        private string _date;
        private Int64 _parentId;

        public MedicalValue() : base()
        {
            _date = string.Empty;
        }

        public MedicalValue(Int64 id, Int64 parentId, double value, string date) : base(id)
        {
            _parentId = parentId;
            _value = value;
            _date = date;
        }

        public double Value
        {
            get => _value;
            set => _value = value;
        }

        public string Date
        {
            get => _date;
            set => _date = value;
        }

        public Int64 ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }
    }
}
