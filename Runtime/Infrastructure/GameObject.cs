using UnityGameObject = UnityEngine.GameObject;

namespace Essentia
{
    public class GameObject
    {
        private UnityGameObject _unityGameObject;

        public GameObject(UnityGameObject unityGameObject)
        {
            _unityGameObject = unityGameObject;
        }
    }
}
