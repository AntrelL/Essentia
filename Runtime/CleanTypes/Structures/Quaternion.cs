using UnityQuaternion = UnityEngine.Quaternion;

namespace Essentia
{
    public struct Quaternion
    {
        public Quaternion(float x = default, float y = default, float z = default, float w = default)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float W { get; set; }

        public static implicit operator Quaternion(UnityQuaternion unityQuaternion) =>
            new(unityQuaternion.x, unityQuaternion.y, unityQuaternion.z, unityQuaternion.w);

        public static implicit operator UnityQuaternion(Quaternion quaternion) =>
            new(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }
}
