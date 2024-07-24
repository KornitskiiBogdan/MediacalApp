using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace MedicalApp.ViewModels.Tabs
{
    public abstract class ViewModelTabBase : ReactiveObject
    {
        public abstract string Header { get; set; }

        public abstract ViewModelBase ViewModel { get; set; }
    }
}
