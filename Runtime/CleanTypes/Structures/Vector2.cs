using UnityVector2 = UnityEngine.Vector2;

namespace Essentia
{
    public struct Vector2
    {
        public Vector2(float x = default, float y = default)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public static implicit operator Vector2(UnityVector2 unityVector2) =>
            new(unityVector2.x, unityVector2.y);

        public static implicit operator UnityVector2(Vector2 vector2) =>
            new(vector2.X, vector2.Y);

        public Vector3 Copy(float? x = null, float? y = null)
        {
            return new Vector3(x ?? X, y ?? Y);
        }
    }
}
