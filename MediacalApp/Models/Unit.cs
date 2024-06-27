using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediacalApp.Models
{
    public class Unit
    {
        public Unit(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(object? obj)
        {
            Unit? other = obj as Unit;
            if (other == null)
            {
                return false;
            }

            return (Name == other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Unit? me, Unit? other)
        {
            return Equals(me, other);
        }

        public static bool operator !=(Unit me, Unit other)
        {
            return !Equals(me, other);
        }
    }
}
