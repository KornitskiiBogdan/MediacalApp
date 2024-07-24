using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.ViewModels.Tabs
{
    public class DocumentsViewModelTab : ViewModelTabBase
    {
        public override string Header { get; set; }

        public override ViewModelBase ViewModel { get; set; }
    }
}
