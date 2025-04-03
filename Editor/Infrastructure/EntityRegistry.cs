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

        [MenuItem(Package.Name + "/Update Entity Registry")]
        public static void Update()
        {
            if (EditorApplication.isUpdating)
                return;

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

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.CallbackFunction updater = null;

            updater = () =>
            {
                Update();
                EditorApplication.update -= updater;
            };

            EditorApplication.update += updater;
        }

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
                    type.Name == prefab.name && IsEntity(type))
                .ToArray();

                if (CheckNumberOfEntityTypes(suitableTypes, prefab.name) == false)
                return emptyConnectionData;

            return new(suitableTypes[0].FullName, entry.address);
        }

        private static bool IsEntity(Type type)
        {
            Type baseType = type.BaseType;

            if (baseType == typeof(Entity))
                return true;

            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Entity<>))
                return true;

            return false;
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
