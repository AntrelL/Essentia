using Essentia.Reflection;
using System.IO;
using UnityEditor;

namespace Essentia.Deployment.Editor
{
    public static class FolderArchitecture
    {
		private const string NullFolderStructureError = "Folder structure cannot be null.";
		private const string FolderStructureGeneratedMessage = "Folder structure generated successfully.";
		private const string GitKeepFilesDeletedMessage = Metadata.GitKeepFileName + " files deleted successfully.";

		public static FolderStructure LoadFolderStructure(string path)
		{
			return AssetDatabase.LoadAssetAtPath<FolderStructure>(path);
		}

		public static void Generate(string pathToFolderStructure, bool createGitKeepFiles = true) =>
			Generate(LoadFolderStructure(pathToFolderStructure), createGitKeepFiles);

		public static void Generate(FolderStructure folderStructure, bool createGitKeepFiles = true)
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
			Console.Log(FolderStructureGeneratedMessage, moduleName: Package.ModuleName.Deployment);
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
