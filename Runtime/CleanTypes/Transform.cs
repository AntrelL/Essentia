using UnityTransform = UnityEngine.Transform;

namespace Essentia
{
    public class Transform
    {
        private UnityTransform _unityTransform;

        public Transform(UnityTransform unityTransform)
        {
            _unityTransform = unityTransform;
        }

        public Vector3 Position
        {
            get => _unityTransform.position;
            set => _unityTransform.position = value;
        }

        public Vector3 LocalPosition
        {
            get => _unityTransform.localPosition;
            set => _unityTransform.localPosition = value;
        }

        public Quaternion Rotation
        {
            get => _unityTransform.rotation;
            set => _unityTransform.rotation = value;
        }

        public Quaternion LocalRotation
        {
            get => _unityTransform.localRotation;
            set => _unityTransform.localRotation = value;
        }

        public Vector3 LocalScale
        {
            get => _unityTransform.localScale;
            set => _unityTransform.localScale = value;
        }

        public Vector3 Forward
        {
            get => _unityTransform.forward;
            set => _unityTransform.forward = value;
        }

        public Vector3 Right
        {
            get => _unityTransform.right;
            set => _unityTransform.right = value;
        }

        public Vector3 Up
        {
            get => _unityTransform.up;
            set => _unityTransform.up = value;
        }

        public Transform Parent => new(_unityTransform.parent);

        public static implicit operator Transform(UnityTransform unityTransform) => new(unityTransform);

        public static implicit operator UnityTransform(Transform transform) => transform._unityTransform;

        public void SetParent(Transform parent, bool worldPositionStays = true) => 
            _unityTransform.SetParent(parent._unityTransform, worldPositionStays);
    }
}
