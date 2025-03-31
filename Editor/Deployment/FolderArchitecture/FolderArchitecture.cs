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

		public static FolderStructure LoadFolderStructure(string path)
		{
			FolderStructure folderStructure = AssetDatabase.LoadAssetAtPath<FolderStructure>(path);

			if (folderStructure is null)
			{
				Console.LogError(
					$"{FolderStructureNotFoundErrorPart} {path}.",
					moduleName: Package.ModuleName.Deployment);
			}

			return folderStructure;
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
			if (folderStructure is null)
			{
				Console.LogError(NullFolderStructureError, moduleName: Package.ModuleName.Deployment);
				return;
			}

			foreach (string folder in folderStructure.Folders)
			{
				if (Directory.Exists(folder) == false)
					Directory.CreateDirectory(folder);

				if (createGitKeepFiles)
					File.WriteAllText(Path.Combine(folder, Metadata.GitKeepFileName), string.Empty);
			}

			AssetDatabase.Refresh();
			Console.Log(successMessage ?? FolderStructureGeneratedMessage, moduleName: Package.ModuleName.Deployment);
		}

		public static void DeleteGitkeepFiles(string rootFolderName)
		{
			string[] gitkeepFiles = Directory.GetFiles(
				rootFolderName, Metadata.GitKeepFileName, SearchOption.AllDirectories);

			foreach (string gitkeepFile in gitkeepFiles)
				File.Delete(gitkeepFile);

			AssetDatabase.Refresh();
			Console.Log(GitKeepFilesDeletedMessage, moduleName: Package.ModuleName.Deployment);
		}
	}
}
