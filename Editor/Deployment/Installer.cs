using Essentia.Reflection;
using UnityEditor;
using UnityEditor.AddressableAssets;

namespace Essentia.Deployment.Editor
{
    public static class Installer
    {
        public const string InvalidDeployedPackageError = Package.Name + " is not deployed correctly.";

		private const string PackageAlreadyDeployedError = Package.Name + " is already deployed.";
        private const string PackageDeployedSuccessfullyMessage = Package.Name + " successfully deployed.";

		private const string DeploymentStartMessage = "Starting deployment of Essentia.";

		private const string UserFoldersGeneratedMessage = "Folder structure for user data generated successfully.";
		private const string PackageFoldersGeneratedMessage = "Folder structure for package data generated successfully.";

		private const string AddressablesInitializedMessage = "Addressables initialized.";
		private const string FailedToInitializeAddressablesError = "Failed to initialize addressables.";

		private const string FolderPathWithFolderStructures = 
			Package.Path + "/Editor/Deployment/FolderArchitecture/FolderStructures/";

		private const string FolderStructurePathForPackageData = 
			FolderPathWithFolderStructures + "DynamicPackageData" + Metadata.AssetExtension;

		private const string FolderStructurePathForUserData = 
			FolderPathWithFolderStructures + "UserData" + Metadata.AssetExtension;

		private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(Package.ModuleName.Deployment, null);

		public static bool IsPackageDeployed => AssetDatabase.IsValidFolder(Package.PathToDynamicDataFolder);

		public static void Deploy()
        {
			Console.Log(DeploymentStartMessage, s_consoleOutputConfig);

            if (TryDeploy(out string errorMessage) == false)
            {
				Console.LogError(errorMessage, s_consoleOutputConfig);
				return;
			}

            Console.Log(PackageDeployedSuccessfullyMessage, s_consoleOutputConfig);
		}

        private static bool TryDeploy(out string errorMessage)
        {
			errorMessage = null;

			if (IsPackageDeployed)
			{
				errorMessage = PackageAlreadyDeployedError;
				return false;
			}

			GenerateFolderStructures();
			InitializeAddressables();

			return true;
        }

		private static void GenerateFolderStructures()
		{
			FolderArchitecture.Generate(FolderStructurePathForPackageData, successMessage: PackageFoldersGeneratedMessage);
			FolderArchitecture.Generate(FolderStructurePathForUserData, successMessage: UserFoldersGeneratedMessage);
		}

		private static void InitializeAddressables()
		{
			var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);

			if (settings is null)
			{
				Console.LogError(FailedToInitializeAddressablesError, s_consoleOutputConfig);
				return;
			}

			settings.CreateGroup(Package.SystemAddressablesGroupName, false, false, false, null);

			Console.Log(AddressablesInitializedMessage, s_consoleOutputConfig);
		}
	}
}
