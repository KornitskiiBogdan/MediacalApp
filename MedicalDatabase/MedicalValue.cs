using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase
{
    public class MedicalValue : MedicalElementBase
    {
        private double _value;
        private string _date;
        private int _parentId;

        public MedicalValue()
        {
            _date = string.Empty;
        }

        public MedicalValue(int id, int parentId, double value, string date) : base(id)
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

        public int ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }
    }
}
