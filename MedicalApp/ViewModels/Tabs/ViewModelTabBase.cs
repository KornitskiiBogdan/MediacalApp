using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalApp.Messages;
using ReactiveUI;
using Tools.Messaging;
using IMessageBus = Tools.Messaging.IMessageBus;

namespace MedicalApp.ViewModels.Tabs
{
    public abstract class ViewModelTabBase : ReactiveObject, IDisposable
    {
        protected readonly IMessageBus _messageBus;
        protected ViewModelTabBase(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public abstract string Header { get; set; }

        public abstract void Dispose();

        protected void ChangeCurrentTab(ViewModelTabBase tab)
        {
            _messageBus.SendAsync<ChangeTab>(new ChangeTab(tab));
        }
    }
}
