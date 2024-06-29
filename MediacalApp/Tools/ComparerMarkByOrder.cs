using System;
using System.Collections.Generic;
using MediacalApp.ViewModels;

namespace MediacalApp.Tools;

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