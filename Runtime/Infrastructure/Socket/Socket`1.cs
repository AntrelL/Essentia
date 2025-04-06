using Essentia.Infrastructure;

namespace Essentia
{
    public class Socket<T> : Socket where T : IScriptConfigAccessPoint
    {
        public const string ConfigTypeMismatchError = "The specified configuration type and " +
            "the configuration type selected on the socket do not match.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig =
            new(typeof(Socket<T>), true, true);

        public Socket(SocketHandle handle) : base(handle)
        {
            if (handle.Config is T)
            {
                handle.Config.Initialize();
                Config = (T)(IScriptConfigAccessPoint)handle.Config;
            }
            else
            {
                Console.LogError(ConfigTypeMismatchError, s_consoleOutputConfig);
            }
        }

        public T Config { get; private set; }
    }
}
