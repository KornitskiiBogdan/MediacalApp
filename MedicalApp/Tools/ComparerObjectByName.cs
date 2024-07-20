using System;
using System.Collections.Generic;
using MedicalApp.ViewModels.Interfaces;

namespace MedicalApp.Tools;

public class ComparerObjectByName : IComparer<INamedObject>
{
    public int Compare(INamedObject? x, INamedObject? y)
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