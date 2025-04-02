using Essentia.Deployment.Editor;
using Essentia.Disk.Editor;
using Essentia.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

using UnityGameObject = UnityEngine.GameObject;

namespace Essentia.Infrastructure.Editor
{
    public static class EntityRegistry
    {
        private const string NoMatchingEntityTypeErrorPart = "No matching type for entity named";
        private const string MultipleMatchingEntityTypesErrorPart = "Multiple matching types for entity named";

		private const string FilePath = Package.PathToDynamicDataFolder + "/" + 
			nameof(EntityRegistryData) + Metadata.AssetExtension;

		[MenuItem(Package.Name + "/Update Entity Registry")]
		public static void Update()
		{
			Dictionary<string, string> entityData = GetAllEntitiesData();

			if (Installer.IsPackageDeployed == false)
			{
				Console.LogError(Installer.InvalidDeployedPackageError, 
					Package.ModuleName.Infrastructure, nameof(EntityRegistry));

				return;
			}

			SettingsFile<EntityRegistryData>.Load(FilePath, true)
				.MarkAsAddressable(Package.SystemAddressablesGroupName)
				.Edit(entityRegistryData => entityRegistryData.Value = entityData, true);
		}

		private static Dictionary<string, string> GetAllEntitiesData()
		{
			List<AddressableAssetEntry> prefabs = ResourceScanner.FindPrefabs();
			Type[] allTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.ToArray();

			return prefabs
				.Select(prefab => GetEntityData(prefab, allTypes))
				.Where(pair => pair.Key is not null)
				.ToDictionary(pair => pair.Key, pair => pair.Value);
		}

		private static KeyValuePair<string, string> GetEntityData(AddressableAssetEntry entry, Type[] allTypes)
        {
			UnityGameObject prefab = AssetDatabase.LoadAssetAtPath<UnityGameObject>(entry.AssetPath);
            KeyValuePair<string, string> emptyPair = new(null, null);

            if (prefab.TryGetComponent(out SocketHandle socketHandle) == false)
                return emptyPair;

            if (socketHandle.IsEntity == false)
                return emptyPair;

            Type[] suitableTypes = allTypes
                .Where(type => 
                    type.Name == prefab.name &&
					typeof(Entity).IsAssignableFrom(type))
                .ToArray();

            if (CheckNumberOfEntityTypes(suitableTypes, prefab.name) == false)
                return emptyPair;

            return new(suitableTypes[0].FullName, entry.address);    
		}

		private static bool CheckNumberOfEntityTypes(Type[] types, string prefabName)
        {
			if (types.Length == 0)
			{
				Console.LogError($"{NoMatchingEntityTypeErrorPart} {prefabName}.",
					Package.ModuleName.Infrastructure, nameof(EntityRegistry));

				return false;
			}

			if (types.Length > 1)
			{
				Console.LogError($"{MultipleMatchingEntityTypesErrorPart} {prefabName}.",
					Package.ModuleName.Infrastructure, nameof(EntityRegistry));

				return false;
			}

            return true;
		}
	}
}
