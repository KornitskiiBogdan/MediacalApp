using System.Collections.Concurrent;
using System.Diagnostics;
using Tools.Collection;

namespace Tools.Messaging
{
    public sealed class StrongReferenceMessageBus : IMessageBus
    {
        public static StrongReferenceMessageBus Instance { get; } = new StrongReferenceMessageBus();

        public Task SendAsync<T>(T message, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (_listeners.TryGetValue(typeof(T), out var listeners))
            {
                if (listeners.Count == 0)
                {
                    return Task.CompletedTask;
                }

                var tasks = new Task[listeners.Count];
                int i = 0;
                foreach (var listener in listeners)
                {
                    try
                    {
                        tasks[i] = ((RecipientActionPair<T>)listener).Invoke(message, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        tasks[i] = Task.FromException(e);
                    }
                    i++;
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public IDisposable Register<TRecipient, TMessage>(TRecipient recipient, Func<TRecipient, TMessage, CancellationToken, Task> func)
        {
            return new RecipientActionPair<TRecipient, TMessage>(_listeners, recipient, func);
        }

        public bool HaveSubscriptions(Type messageType)
        {
            return _listeners.TryGetValue(messageType, out var listeners) && listeners.Count > 0;
        }

        private readonly ConcurrentDictionary<Type, ConcurrentHashSet<object>> _listeners = new(ReferenceEqualityComparer.Instance);

        private abstract class RecipientActionPair<TMessage>
        {
            public abstract Task Invoke(TMessage message, CancellationToken cancellationToken);
        }

        [DebuggerStepThrough]
        private sealed class RecipientActionPair<TRecipient, TMessage> : RecipientActionPair<TMessage>, IDisposable
        {
            private readonly ConcurrentDictionary<Type, ConcurrentHashSet<object>> _listeners;
            private readonly Func<TRecipient, TMessage, CancellationToken, Task> _func;
            private readonly TRecipient _recipient;

            public RecipientActionPair(ConcurrentDictionary<Type, ConcurrentHashSet<object>> listeners,
                                       TRecipient recipient,
                                       Func<TRecipient, TMessage, CancellationToken, Task> func)
            {
                _listeners = listeners;
                _recipient = recipient;
                _func = func;

                _ = listeners
                        .GetOrAdd(typeof(TMessage), _ => new ConcurrentHashSet<object>(ReferenceEqualityComparer.Instance))
                        .Add(this);
            }

            public override Task Invoke(TMessage message, CancellationToken cancellationToken)
            {
                return _func.Invoke(_recipient, message, cancellationToken);
            }

            public void Dispose() => _listeners[typeof(TMessage)].Remove(this);
        }
    }
}
