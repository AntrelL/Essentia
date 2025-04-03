using Essentia.Deployment.Editor;
using Essentia.Reflection;
using System;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace Essentia.Disk.Editor
{
    public static class ResourceScanner
    {
        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Disk, nameof(ResourceScanner));

        public static List<AddressableAssetEntry> FindAll(Predicate<AddressableAssetEntry> match = null)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

            if (settings is null)
            {
                Console.LogError(Installer.InvalidDeployedPackageError, s_consoleOutputConfig);
                return null;
            }

            List<AddressableAssetEntry> resources = new();

            foreach (AddressableAssetGroup group in settings.groups)
            {
                foreach (AddressableAssetEntry entry in group.entries)
                {
                    if (CheckCompliance(entry, match))
                        resources.Add(entry);
                }				
            }

            return resources;
        }

        public static List<AddressableAssetEntry> FindPrefabs(Predicate<AddressableAssetEntry> match = null)
        {
            return FindAll(
                entry => entry.AssetPath.EndsWith(Metadata.PrefabExtension) &&
                CheckCompliance(entry, match));
        }

        private static bool CheckCompliance<T>(T @object, Predicate<T> predicate = null)
        {
            return predicate is null || predicate.Invoke(@object);
        }
    }
}
