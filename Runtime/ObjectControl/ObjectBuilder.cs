using Essentia.Infrastructure;
using System;
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

        public static T CreateNew<T>(Type type, ObjectСreationСonfig сreationСonfig = null) where T : UnityEngine.Component
        {
            return CreateNew(type, сreationСonfig).GetComponent<T>();
        }

        private static void SetUpInstance(GameObject instance, ObjectСreationСonfig creationСonfig)
        {
            creationСonfig ??= new();
            Transform transform = instance.Transform;

            instance.Name = creationСonfig.Name ?? instance.Name.Remove(CloneMarkerText);

            transform.Position = creationСonfig.Position ?? transform.Position;
            transform.Rotation = creationСonfig.Rotation ?? transform.Rotation;
            transform.SetParent(creationСonfig.Parent ?? transform.Parent, creationСonfig.InWorldSpace);

            instance.SetActive(creationСonfig.Active);
        }
    }
}
