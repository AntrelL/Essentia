using Essentia.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Essentia.Infrastructure
{
    public static class EntityRegistryReader
    {
        public const string DataFilePath = Package.PathToDynamicDataFolder + "/" +
            nameof(EntityRegistryData) + Metadata.AssetExtension;

        private const string EntityAddressNotFoundErrorPart = "Entity address not found, entity type:";

        private static List<EntityConnectionData> s_data;

        static EntityRegistryReader()
        {
            s_data = Addressables.LoadAssetAsync<EntityRegistryData>(DataFilePath)
                .WaitForCompletion().Connections;
        }

        public static string GetAddress(Type entityType)
        {
            EntityConnectionData entityConnectionData =	s_data.Find(
                connectionData => connectionData.TypeName == entityType.FullName);

            if (entityConnectionData is null)
            {
                Console.LogError($"{EntityAddressNotFoundErrorPart} {entityType.FullName}.",
                    Package.ModuleName.Infrastructure, nameof(EntityRegistryReader));
            }

            return entityConnectionData?.Address;
        }
    }
}
