using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

using UnityScene = UnityEngine.SceneManagement.Scene;
using UnityGameObject = UnityEngine.GameObject;

namespace Essentia.Editor
{
    public class Scene
    {
        private UnityScene _unityScene;
        private string _path;

        public Scene(string path)
        {
            _unityScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            _path = path;
        }

        public void AddObject(Func<UnityGameObject, UnityGameObject> objectInitializer)
        {
            SceneManager.MoveGameObjectToScene(objectInitializer.Invoke(new UnityGameObject()), _unityScene);
        }

        public bool Save()
        {
            return EditorSceneManager.SaveScene(_unityScene, _path);
        }
    }
}
