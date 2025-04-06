using Essentia.Infrastructure;

namespace Essentia
{
    public abstract class ScriptConfig : MonoScript, IScriptConfigAccessPoint
    {
        public virtual void Initialize() { }
    }
}
