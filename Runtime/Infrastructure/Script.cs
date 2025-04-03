using System;

namespace Essentia
{
    public abstract class Script
    {
        private const string AccessingUnestablishedSocketError = "Attempt to access an unestablished socket.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(typeof(Script), false, true);

        private Socket _socket;

        public Script(Func<Type, Socket> socketExtractor)
        {
            Socket = socketExtractor.Invoke(GetType());
        }

        public Script(Socket socket = null)
        {
            Socket = socket;
        }

        public Socket Socket 
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
