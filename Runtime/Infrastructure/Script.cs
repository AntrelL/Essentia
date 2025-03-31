namespace Essentia
{
    public class Script
    {
        private const string AccessingUnestablishedSocketError = "Attempt to access an unestablished socket.";

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
                    Console.LogError<Script>(AccessingUnestablishedSocketError, isModuleName: false);

                return _socket;
            }
            private set
            {
                _socket = value;
            }
        }
    }
}
