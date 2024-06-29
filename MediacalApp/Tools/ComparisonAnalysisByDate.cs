using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediacalApp.ViewModels;

namespace MediacalApp.Tools
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
}
