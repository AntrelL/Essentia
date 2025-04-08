using Essentia.Infrastructure;
using Essentia.Reflection;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine.SceneManagement;

using UnityGameObject = UnityEngine.GameObject;

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

        private const string MainSceneInitializedMessage = "Main scene initialized.";
        private const string FailedToCreateMainSceneError = "Failed to create main scene.";

        private const string FolderPathWithFolderStructures = 
            Package.Path + "/Editor/Deployment/FolderArchitecture/FolderStructures/";

        private const string FolderStructurePathForPackageData = 
            FolderPathWithFolderStructures + "DynamicPackageData" + Metadata.AssetExtension;

        private const string FolderStructurePathForUserData = 
            FolderPathWithFolderStructures + "UserData" + Metadata.AssetExtension;

        private static readonly ConsoleOutputConfig s_consoleOutputConfig = new(Package.ModuleName.Deployment, null);

        public static bool IsPackageDeployed => AssetDatabase.IsValidFolder(Package.PathToDynamicDataFolder);

        public static bool Update()
        {
            bool isPackageDeployed = IsPackageDeployed;

            if (isPackageDeployed == false)
                Deploy();

            return isPackageDeployed ^ IsPackageDeployed;
        }

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

            if (TryGenerateFolderStructures(out errorMessage) == false)
                return false;

            if (TryInitializeAddressables(out errorMessage) == false)
                return false;

            if (TryInitializeMainScene(out errorMessage) == false)
                return false;

            return true;
        }

        private static bool TryGenerateFolderStructures(out string errorMessage)
        {
            if (FolderArchitecture.TryGenerate(FolderStructurePathForPackageData, out errorMessage) == false)
                return false;

            Console.Log(PackageFoldersGeneratedMessage, s_consoleOutputConfig);

            if (FolderArchitecture.TryGenerate(FolderStructurePathForUserData, out errorMessage) == false)
                return false;

            Console.Log(UserFoldersGeneratedMessage, s_consoleOutputConfig);

            return true;
        }

        private static bool TryInitializeAddressables(out string errorMessage)
        {
            errorMessage = null;
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);

            if (settings is null)
            {
                errorMessage = FailedToInitializeAddressablesError;
                return false;
            }

            settings.CreateGroup(Package.SystemAddressablesGroupName, false, false, false, null);

            Console.Log(AddressablesInitializedMessage, s_consoleOutputConfig);
            return true;
        }

        private static bool TryInitializeMainScene(out string errorMessage)
        {
            errorMessage = null;

            if (SceneBuilder.TryCreateNew(Package.PathToMainScene, out Scene mainScene) == false)
            {
                errorMessage = FailedToCreateMainSceneError;
                return false;
            }

            SceneBuilder.CreateObjectInScene(mainScene, (UnityGameObject gameObject) =>
            {
                gameObject.AddComponent<EntryPoint>();
                gameObject.name = Package.SystemObjectName;
                gameObject.isStatic = true;

                return gameObject;
            });

            Console.Log(MainSceneInitializedMessage, s_consoleOutputConfig);
            return true;
        }
    }
}
