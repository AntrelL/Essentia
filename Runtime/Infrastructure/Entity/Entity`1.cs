using Essentia.Infrastructure;
using Essentia.ObjectControl;

namespace Essentia
{
    public abstract class Entity<T> : Script<T> where T : IScriptConfigAccessPoint
    {
        public Entity(ObjectСreationСonfig сreationСonfig = null)
            : base(type => new Socket<T>(ObjectBuilder.CreateNew<SocketHandle>(type, сreationСonfig)))
        {
        }
    }
}
