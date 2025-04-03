using UnityEngine;

namespace Essentia.Infrastructure
{
    public class SocketHandle : MonoScript
    {
        [field: SerializeField] public bool IsEntity { get; private set; } = false;

        [field: SerializeField] public ScriptConfig Config { get; private set; }
    }
}
