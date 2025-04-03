using System.Collections.Generic;
using UnityEngine;

namespace Essentia.Infrastructure
{
    public class EntityRegistryData : ScriptableObject
    {
        [HideInInspector] public List<EntityConnectionData> Connections;
    }
}
