using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

using UnityGameObject = UnityEngine.GameObject;

namespace Essentia.Deployment.Editor
{
    public static class SceneBuilder
    {
        public static bool TryCreateNew(string path, out Scene scene)
        {
            scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            return EditorSceneManager.SaveScene(scene, path);
        }

        public static void CreateObjectInScene(Scene scene, Func<UnityGameObject, UnityGameObject> objectCreator)
        {
            SceneManager.MoveGameObjectToScene(objectCreator.Invoke(new UnityGameObject()), scene);
        }
    }
}
