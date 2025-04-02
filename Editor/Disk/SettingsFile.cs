using System;
using UnityEditor;
using UnityEngine;

namespace Essentia.Disk.Editor
{
    public class SettingsFile<T> where T : ScriptableObject
	{
        private const string FailedToLoadAssetAtPathErrorPart = "Failed to load asset at path";
        private const string AssetAlreadyExistsErrorPart = "Error creating asset, asset with this name already exists, path:";

		private T _asset;

        private SettingsFile(T asset, string path)
        {
            _asset = asset;
            Path = path;
        }
        
        public string Path { get; private set; }

        public static SettingsFile<T> Create(string path)
		{
            if (Exists(path))
            {
                Console.LogError<SettingsFile<T>>($"{AssetAlreadyExistsErrorPart} {path}.");
                return null;
            }

			T asset = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(asset, path);

            return new(asset, path);
		}

		public static SettingsFile<T> Load(string path)
		{
            if (TryLoad(path, out SettingsFile<T> settingsFile) == false)
                Console.LogError<SettingsFile<T>>($"{FailedToLoadAssetAtPathErrorPart} {path}.");

			return settingsFile;
		}

		public static bool TryLoad(string path, out SettingsFile<T> settingsFile)
        {
			T asset = AssetDatabase.LoadAssetAtPath<T>(path);
			settingsFile = null;

            if (asset is null)
                return false;

            settingsFile = new(asset, path);
            return true;
		}

		public static bool Exists(string path) => TryLoad(path, out _);

        public void Edit(Action<T> changer, bool autoSave = true)
        {
            changer.Invoke(_asset);

            if (autoSave)
                Save();
        }

        public void Save()
        {
            EditorUtility.SetDirty(_asset);
            AssetDatabase.SaveAssets();
        }
	}
}
