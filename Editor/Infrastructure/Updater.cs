using Essentia.Deployment.Editor;
using Essentia.Reflection;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Essentia.Infrastructure.Editor
{
    public class Updater : IPreprocessBuildWithReport
    {
        private const string UpdateButtonName = "Update";

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report) => EntityRegistry.Update();

        [InitializeOnLoadMethod]
        private static void SubscribeUpdate()
        {
            EditorApplication.CallbackFunction updater = null;

            updater = () =>
            {
                UpdateSystems();
                EditorApplication.update -= updater;
            };

            EditorApplication.update += updater;
        }

        [MenuItem(Package.Name + "/" + UpdateButtonName)]
        private static void UpdateSystems()
        {
            Installer.Update();
            EntityRegistry.Update();
        }
    }
}
