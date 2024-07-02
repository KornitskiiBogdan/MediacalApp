using System.Diagnostics;

namespace Tools.Messaging
{
    public static class MessageBusExtensions
    {
        public static Task SendAsync<T>(this IMessageBus messageBus, CancellationToken cancellationToken) where T : new()
        {
            return messageBus.SendAsync(new T(), cancellationToken);
        }

        public static Task SendAsync<T>(this IMessageBus messageBus, T message)
        {
            return messageBus.SendAsync(message, CancellationToken.None);
        }

        public static Task SendAsync<T>(this IMessageBus messageBus) where T : new()
        {
            return messageBus.SendAsync(new T(), CancellationToken.None);
        }

        public static IDisposable Register<T>(this IMessageSource messageSource, Func<T, CancellationToken, Task> action)
        {
            return messageSource.Register<Func<T, CancellationToken, Task>, T>(action, [DebuggerStepThrough] static (r, m, c) => r.Invoke(m, c));
        }

        public static IDisposable Register<T>(this IMessageSource messageSource, Func<T, Task> action)
        {
            return messageSource.Register<Func<T, Task>, T>(action, [DebuggerStepThrough] static (r, m, c) => r.Invoke(m));
        }

        public static IDisposable Register<T>(this IMessageSource messageSource, Action<T, CancellationToken> action)
        {
            return messageSource.Register<Action<T, CancellationToken>, T>(action, [DebuggerStepThrough] static (r, m, c) => { r.Invoke(m, c); return Task.CompletedTask; });
        }

        public static IDisposable Register<T>(this IMessageSource messageSource, Action<T> action)
        {
            return messageSource.Register<Action<T>, T>(action, [DebuggerStepThrough] static (r, m, c) => { r.Invoke(m); return Task.CompletedTask; });
        }

        public static IMessageSource AsMessageSource(this IMessageBus messageBus)
        {
            return new MessageBusAsSource(messageBus);
        }

        private sealed class MessageBusAsSource : IMessageSource
        {
            private readonly IMessageSource _messageSource;

            public MessageBusAsSource(IMessageSource messageSource)
            {
                _messageSource = messageSource;
            }

            public IDisposable Register<TRecipient, TMessage>(TRecipient recipient, Func<TRecipient, TMessage, CancellationToken, Task> func)
            {
                return _messageSource.Register(recipient, func);
            }
        }
    }
}
