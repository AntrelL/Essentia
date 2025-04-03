namespace Essentia
{
    public class Script
    {
        private const string AccessingUnestablishedSocketError = "Attempt to access an unestablished socket.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(typeof(Script), false, true);

        private Socket _socket;

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
