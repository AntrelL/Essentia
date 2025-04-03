using Essentia.Infrastructure;
using Essentia.ObjectControl;

namespace Essentia
{
    public abstract class Entity : Script
    {
        public Entity(ObjectСreationСonfig сreationСonfig = null) 
            : base(type => new Socket(ObjectBuilder.CreateNew<SocketHandle>(type, сreationСonfig)))
        {
        }
    }
}
