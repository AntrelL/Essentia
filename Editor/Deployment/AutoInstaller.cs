using UnityEditor;

namespace Essentia.Deployment.Editor
{
    public static class AutoInstaller
    {
        [InitializeOnLoadMethod]
        private static void Deploy()
        {
            if (Installer.IsPackageDeployed == false)
                Installer.Deploy();
        }
    }
}
