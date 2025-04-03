using System;

namespace Essentia
{
    public abstract class BaseScript<T> where T : Socket
    {
        private const string AccessingUnestablishedSocketError = "Attempt to access an unestablished socket.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(typeof(T), false, true);

        private T _socket;

        public BaseScript(Func<Type, T> socketExtractor)
        {
            Socket = socketExtractor.Invoke(GetType());
        }

        public BaseScript(T socket = null)
        {
            Socket = socket;
        }

        public T Socket
        {
            get
            {
                if (_socket is null)
                    Console.LogError(AccessingUnestablishedSocketError, s_consoleOutputConfig);

                return _socket;
            }
            private set
            {
                _socket = value;
            }
        }
    }
}
