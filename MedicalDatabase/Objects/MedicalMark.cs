using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Objects
{
    public class MedicalMark : MedicalNamedElementBase
    {
        private Unit _unit;
        private string _nameSubGroup;
        private string _nameGroup;

        public MedicalMark() : base()
        {
            _unit = new Unit(string.Empty);
            _nameSubGroup = string.Empty;
            _nameGroup = string.Empty;
        }

        public MedicalMark(Int64 id, string name, string unit, string nameSubGroup, string nameGroup) : base(id, name)
        {
            _unit = new Unit(unit);
            _nameSubGroup = nameSubGroup;
            _nameGroup = nameGroup;
        }


        public Unit Unit
        {
            get => _unit;
            set => _unit = value;
        }

        public string NameSubGroup
        {
            get => _nameSubGroup;
            set => _nameSubGroup = value;
        }

        public string NameGroup
        {
            get => _nameGroup;
            set => _nameGroup = value;
        }
    }
}
