using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.ViewModels;

namespace MediacalApp.Models
{
    public class ComparerMarkByDate : IComparer<MarkViewModel>
    {
        public int Compare(MarkViewModel? x, MarkViewModel? y)
        {
            if (x != null)
            {
                return y == null ? 1 : x.CurrentDatetime.CompareTo(y.CurrentDatetime);
            }

            if (y == null)
            {
                return 0;
            }

            return -1;
        }
    }

    public class ComparerMarkByOrder : IComparer<MarkViewModel>
    {
        public int Compare(MarkViewModel? x, MarkViewModel? y)
        {
            if (x != null)
            {
                return y == null ? 1 : string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }

            if (y == null)
            {
                return 0;
            }

            return -1;
        }
    }
}
