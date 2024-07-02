using System.Collections.Generic;
using MedicalApp.ViewModels;

namespace MedicalApp.Tools
{
    public class ComparerMarkByDate : IComparer<MarkViewModel>
    {
        public int Compare(MarkViewModel? x, MarkViewModel? y)
        {
            if (x != null)
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    if (x.CurrentDatetime == null)
                    {
                        return y.CurrentDatetime == null ? 0 : -1;
                    }

                    return y.CurrentDatetime == null ? 1 : x.CurrentDatetime.Value.CompareTo(y.CurrentDatetime.Value);
                }

            }

            if (y == null)
            {
                return 0;
            }

            return -1;
        }
    }
}
