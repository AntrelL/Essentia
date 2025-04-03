using UnityEngine;

namespace Essentia.Infrastructure
{
    public class SocketHandle : MonoScript
    {
        [field: SerializeField] public bool IsEntity { get; private set; } = false;
    }
}
