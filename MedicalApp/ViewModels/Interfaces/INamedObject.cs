using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace MedicalApp.ViewModels.Interfaces
{
    public interface INamedObject : IReactiveObject
    {
        string Name { get; set; }
    }
}
