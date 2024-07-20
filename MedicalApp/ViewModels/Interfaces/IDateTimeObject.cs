using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.ViewModels.Interfaces
{
    public interface IDateTimeObject : IReactiveObject
    {
        DateTime? CurrentDateTime { get; }
    }
}
