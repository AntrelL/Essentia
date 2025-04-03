using System;
using UnityEngine;

namespace Essentia.Infrastructure
{
    [Serializable]
    public class EntityConnectionData
    {
		public EntityConnectionData(string typeName, string address)
        {
			TypeName = typeName;
			Address = address;
		}

		[field: SerializeField] public string TypeName { get; private set; }

		[field: SerializeField] public string Address { get; private set; }
	}
}
