using System;
using System.Linq;
using System.Reflection;

namespace Essentia.Reflection
{
    public static class Metadata
    {
        public const string NamespaceSeparationSymbol = ".";
        public const string EntryPointMethodName = "Main";
        public const BindingFlags EntryPointMethodBindingFlags = 
            BindingFlags.NonPublic | BindingFlags.Static;

        public const string GitKeepFileName = ".gitkeep";
        public const string AssetExtension = ".asset";
        public const string PrefabExtension = ".prefab";
        public const string AssetRootFolderName = "Assets";

        private const string EditorPartOfNamespace = NamespaceSeparationSymbol + "Editor";
        private const string MissingModuleNameError = "Unable to get module name if namespace is missing.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = 
            new(Package.ModuleName.Reflection, nameof(Metadata));

        public static string GetModuleName(Type type)
        {
            string name = type.Namespace
                ?.Remove(EditorPartOfNamespace)
                .Split(NamespaceSeparationSymbol)
                .Last();

            if (name is null)
                Console.LogError(MissingModuleNameError, s_consoleOutputConfig);

            return name;
        }
    }
}
