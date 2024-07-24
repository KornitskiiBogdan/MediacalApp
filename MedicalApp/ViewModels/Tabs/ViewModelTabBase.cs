using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Tools.Messaging;
using IMessageBus = Tools.Messaging.IMessageBus;

namespace MedicalApp.ViewModels.Tabs
{
    public abstract class ViewModelTabBase : ReactiveObject, IDisposable
    {
        public event Action<ViewModelTabBase>? ChangeCurrentTabEvent;

        public abstract string Header { get; set; }

        public abstract void Dispose();

        protected void ChangeCurrentTabInvoke(ViewModelTabBase tab)
        {
            ChangeCurrentTabEvent?.Invoke(tab);
        }
    }
}
