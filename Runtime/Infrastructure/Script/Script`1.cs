using System;

namespace Essentia
{
    public abstract class Script<T> : BaseScript<Socket<T>> where T : ScriptConfig
    {
        public Script(Func<Type, Socket<T>> socketExtractor) : base (socketExtractor) { }

        public Script(Socket<T> socket = null) : base(socket) { }
    }
}
