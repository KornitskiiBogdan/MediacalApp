﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
