using System.Collections.Generic;
using MedicalApp.ViewModels.Interfaces;

namespace MedicalApp.Tools
{
    public class ComparerByDate : IComparer<IDateTimeObject>
    {
        public int Compare(IDateTimeObject? x, IDateTimeObject? y)
        {
            if (x != null)
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    if (x.CurrentDateTime == null)
                    {
                        return y.CurrentDateTime == null ? 0 : -1;
                    }

                    return y.CurrentDateTime == null ? 1 : x.CurrentDateTime.Value.CompareTo(y.CurrentDateTime.Value);
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
