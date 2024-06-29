using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediacalApp.Messaging
{
    public interface IMessageBus : IMessageSource
    {
        Task SendAsync<T>(T message, CancellationToken cancellationToken);

        bool HaveSubscriptions(Type messageType);
    }

    public interface IMessageSource
    {
        IDisposable Register<TRecipient, TMessage>(TRecipient recipient, Func<TRecipient, TMessage, CancellationToken, Task> func);
    }
}
