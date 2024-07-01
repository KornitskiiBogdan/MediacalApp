using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediacalApp.Models
{
    public class ReferenceModel
    {
        public ReferenceModel(string value, string name)
        {
            Name = name;
        }

        public int? LowerValue { get; set; }

        public int? UpperValue { get; set; }

        public string Name { get; set; }

        private void ParseReferenceToInt(string value)
        {
            Regex regex = new Regex(@"\s*<\s*\w*");
            string parrentLeq = @"\s*<\s*\w*";
            if (Regex.IsMatch(value, parrentLeq))
            {

            }
        }

    }
}
