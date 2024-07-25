using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Objects
{
    public class MedicalValue : MedicalElementBase
    {
        private float _value;
        private long _date;
        private Int64 _parentId;

        public MedicalValue() : base()
        {
        }

        public MedicalValue(Int64 id, Int64 parentId, float value, long date) : base(id)
        {
            _parentId = parentId;
            _value = value;
            _date = date;
        }

        public MedicalValue(Int64 id, Int64 parentId, float value) : this(id, parentId, value, DateTime.Now.Ticks)
        {
        }

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public long Date
        {
            get => _date;
            set => _date = value;
        }

        public Int64 ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }

        public DateTime GetDateTime()
        {
            return new DateTime(Date);
        }
    }
}
