using System;

namespace Essentia
{
    public abstract class Script<T> : BaseScript<Socket<T>> where T : IScriptConfigAccessPoint
    {
        public Script(Func<Type, Socket<T>> socketExtractor) : base (socketExtractor) { }

        public Script(Socket<T> socket = null) : base(socket) { }

        protected T Config => Socket.Config;

        protected GameObject GameObject => Socket.GameObject;

        protected Transform Transform => Socket.GameObject.Transform;
    }
}
