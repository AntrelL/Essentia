using UnityVector3 = UnityEngine.Vector3;

namespace Essentia
{
    public partial struct Vector3
    {
        public Vector3(float x = default, float y = default, float z = default)
        {
            X = x; 
            Y = y; 
            Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; } 

        public static implicit operator Vector3(UnityVector3 unityVector3) => 
            new(unityVector3.x, unityVector3.y, unityVector3.z);

        public static implicit operator UnityVector3(Vector3 vector3) =>
            new(vector3.X, vector3.Y, vector3.Z);

        public Vector3 Copy(float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? X, y ?? Y, z ?? Z);
        }
    }
}
