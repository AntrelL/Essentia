using Essentia.Infrastructure;

namespace Essentia
{
    public class Socket
    {
        private SocketHandle _handle;

        public Socket(SocketHandle handle)
        {
            _handle = handle;
            GameObject = new(_handle.gameObject);
        }

        public GameObject GameObject { get; private set; }
    }
}
