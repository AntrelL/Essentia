using System;

namespace Essentia
{
    public abstract class Script : BaseScript<Socket>
    {
        public Script(Func<Type, Socket> socketExtractor) : base(socketExtractor) { }

        public Script(Socket socket = null) : base(socket) { }
    }
}
