using UnityGameObject = UnityEngine.GameObject;

namespace Essentia
{
    public partial class GameObject
    {
        private UnityGameObject _unityGameObject;

        public GameObject(UnityGameObject unityGameObject)
        {
            _unityGameObject = unityGameObject;
            Transform = _unityGameObject.transform;
        }

        public Transform Transform { get; private set; }

        public string Name
        {
            get => _unityGameObject.name;
            set => _unityGameObject.name = value;
        }

        public int Layer
        { 
            get => _unityGameObject.layer;
            set => _unityGameObject.layer = value;
        }

        public bool Active => _unityGameObject.activeSelf;

        public static implicit operator GameObject(UnityGameObject unityGameObject) => new(unityGameObject);

        public static implicit operator UnityGameObject(GameObject gameObject) => gameObject._unityGameObject;

        public void Activate() => SetActive(true);

        public void Deactivate() => SetActive(false);

        public void SetActive(bool value) => _unityGameObject.SetActive(value);

        public new string ToString() => _unityGameObject.ToString();
    }
}
