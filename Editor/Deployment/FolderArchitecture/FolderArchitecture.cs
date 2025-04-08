using Essentia.Reflection;
using System.IO;
using UnityEditor;

namespace Essentia.Deployment.Editor
{
    public static class FolderArchitecture
    {
        private const string FolderStructureNotFoundErrorPart = "Folder structure not found at path";
        private const string NullFolderStructureError = "Folder structure cannot be null.";
        private const string FolderStructureGeneratedMessage = "Folder structure generated successfully.";
        private const string GitKeepFilesDeletedMessage = Metadata.GitKeepFileName + " files deleted successfully.";

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(Package.ModuleName.Deployment, null);

        public static bool TryLoadFolderStructure(string path, out FolderStructure folderStructure, out string errorMessage)
        {
            errorMessage = null;

            folderStructure = AssetDatabase.LoadAssetAtPath<FolderStructure>(path);

            if (folderStructure is null)
            {
                errorMessage = $"{FolderStructureNotFoundErrorPart} {path}.";
                return false;
            }
                
            return true;
        }

        public static FolderStructure LoadFolderStructure(string path)
        {
            if (TryLoadFolderStructure(path, out FolderStructure folderStructure, out string errorMessage) == false)
                Console.LogError(errorMessage, s_consoleOutputConfig);  

            return folderStructure;
        }

        public static bool TryGenerate(
            string pathToFolderStructure, out string errorMessage, bool createGitKeepFiles = true)
        {
            TryLoadFolderStructure(pathToFolderStructure, out FolderStructure folderStructure, out errorMessage);

            if (folderStructure is null)
                return false;
            
            return TryGenerate(folderStructure, out errorMessage, createGitKeepFiles);
        }

        public static bool TryGenerate(
            FolderStructure folderStructure, out string errorMessage, bool createGitKeepFiles = true)
        {
            errorMessage = null;

            if (folderStructure is null)
            {
                errorMessage = NullFolderStructureError;
                return false;
            }

            foreach (string folder in folderStructure.Folders)
            {
                if (Directory.Exists(folder) == false)
                    Directory.CreateDirectory(folder);

                if (createGitKeepFiles)
                    File.WriteAllText(Path.Combine(folder, Metadata.GitKeepFileName), string.Empty);
            }

            AssetDatabase.Refresh();
            return true;
        }

        public static void Generate(
            string pathToFolderStructure, bool createGitKeepFiles = true, string successMessage = null)
        {
            FolderStructure folderStructure = LoadFolderStructure(pathToFolderStructure);

            if (folderStructure is not null)
                Generate(folderStructure, createGitKeepFiles, successMessage);
        }

        public static void Generate(
            FolderStructure folderStructure, bool createGitKeepFiles = true, string successMessage = null)
        {
            if (TryGenerate(folderStructure, out string errorMessage, createGitKeepFiles) == false)
            {
                Console.LogError(errorMessage, s_consoleOutputConfig);
                return;
            }

            Console.Log(successMessage ?? FolderStructureGeneratedMessage, s_consoleOutputConfig);
        }

        public static void DeleteGitkeepFiles(string rootFolderName)
        {
            string[] gitkeepFiles = Directory.GetFiles(
                rootFolderName, Metadata.GitKeepFileName, SearchOption.AllDirectories);

            foreach (string gitkeepFile in gitkeepFiles)
                File.Delete(gitkeepFile);

            AssetDatabase.Refresh();
            Console.Log(GitKeepFilesDeletedMessage, s_consoleOutputConfig);
        }
    }
}
