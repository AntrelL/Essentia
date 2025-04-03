using Essentia.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

using UnityGameObject = UnityEngine.GameObject;

namespace Essentia.ObjectControl
{
    public static class ObjectBuilder
    {
        private const string FailedToCreateObjectError = "Failed to create object.";
        private const string CloneMarkerText = "(Clone)";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(typeof(ObjectBuilder), true, true);

        public static UnityGameObject CreateNew(Type type, ObjectСreationСonfig creationСonfig = null)
        {
            string prefabAddress = EntityRegistryReader.GetAddress(type);

            if (prefabAddress is null)
            {
                Console.LogError(FailedToCreateObjectError, s_consoleOutputConfig);
                return null;
            }

            UnityGameObject instance = Addressables.InstantiateAsync(prefabAddress).WaitForCompletion();
            SetUpInstance(instance, creationСonfig);

            return instance;
        }

        public static T CreateNew<T>(Type type, ObjectСreationСonfig сreationСonfig = null) where T : Component
        {
            return CreateNew(type, сreationСonfig).GetComponent<T>();
        }

        private static void SetUpInstance(UnityGameObject instance, ObjectСreationСonfig creationСonfig)
        {
            creationСonfig ??= new();
            Transform transform = instance.transform;

            instance.name = creationСonfig.Name ?? instance.name.Remove(CloneMarkerText);

            transform.position = creationСonfig.Position ?? transform.position;
            transform.rotation = creationСonfig.Rotation ?? transform.rotation;
            transform.SetParent(creationСonfig.Parent ?? transform.parent, creationСonfig.InWorldSpace);

            instance.SetActive(creationСonfig.Active);
        }
    }
}
