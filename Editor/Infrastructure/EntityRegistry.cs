using Essentia.Deployment.Editor;
using Essentia.Disk.Editor;
using Essentia.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.AddressableAssets.Settings;

using UnityGameObject = UnityEngine.GameObject;

namespace Essentia.Infrastructure.Editor
{
    public class EntityRegistry : IPreprocessBuildWithReport
	{
		private const string NoMatchingEntityTypeErrorPart = "No matching type for entity named";
        private const string MultipleMatchingEntityTypesErrorPart = "Multiple matching types for entity named";

		private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(typeof(EntityRegistry), true, true);

		public int callbackOrder => 0;

		[InitializeOnLoadMethod]
		[MenuItem(Package.Name + "/Update Entity Registry")]
		public static void Update()
		{
			List<EntityConnectionData> entityConnections = GetEntityConnections();

			if (Installer.IsPackageDeployed == false)
			{
				Console.LogError(Installer.InvalidDeployedPackageError, s_consoleOutputConfig);
				return;
			}

			SettingsFile<EntityRegistryData>.Load(EntityRegistryReader.DataFilePath, true)
				.MarkAsAddressable(Package.SystemAddressablesGroupName)
				.Edit(entityRegistryData => entityRegistryData.Connections = entityConnections, true);
		}

		public void OnPreprocessBuild(BuildReport report) => Update();

		private static List<EntityConnectionData> GetEntityConnections()
		{
			List<AddressableAssetEntry> prefabs = ResourceScanner.FindPrefabs();

			Type[] allTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.ToArray();

			return prefabs
				.Select(prefab => GetEntityConnection(prefab, allTypes))
				.Where(connectionData => connectionData.TypeName is not null)
				.ToList();
		}

		private static EntityConnectionData GetEntityConnection(AddressableAssetEntry entry, Type[] allTypes)
        {
			UnityGameObject prefab = AssetDatabase.LoadAssetAtPath<UnityGameObject>(entry.AssetPath);
			EntityConnectionData emptyConnectionData = new(null, null);

            if (prefab.TryGetComponent(out SocketHandle socketHandle) == false)
                return emptyConnectionData;

            if (socketHandle.IsEntity == false)
                return emptyConnectionData;

            Type[] suitableTypes = allTypes
                .Where(type => 
                    type.Name == prefab.name &&
					typeof(Entity).IsAssignableFrom(type))
                .ToArray();

            if (CheckNumberOfEntityTypes(suitableTypes, prefab.name) == false)
                return emptyConnectionData;

            return new(suitableTypes[0].FullName, entry.address);
		}

		private static bool CheckNumberOfEntityTypes(Type[] types, string prefabName)
        {
			if (types.Length == 0)
			{
				Console.LogError($"{NoMatchingEntityTypeErrorPart} {prefabName}.", s_consoleOutputConfig);
				return false;
			}

			if (types.Length > 1)
			{
				Console.LogError($"{MultipleMatchingEntityTypesErrorPart} {prefabName}.", s_consoleOutputConfig);
				return false;
			}

            return true;
		}
	}
}
