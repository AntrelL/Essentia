namespace Essentia.ObjectControl
{
    public class ObjectСreationСonfig
    {
        public ObjectСreationСonfig(
            string name = null,
            Vector3? position = null,
            Quaternion? rotation = null,
            Transform parent = null,
            bool inWorldSpace = false,
            bool active = true)
        {
            Name = name;
            Position = position;
            Rotation = rotation;
            Parent = parent;
            InWorldSpace = inWorldSpace;
            Active = active;
        }

        public string Name { get; private set; }

        public Vector3? Position { get; private set; }

        public Quaternion? Rotation { get; private set; }

        public Transform Parent { get; private set; }

        public bool InWorldSpace { get; private set; }

        public bool Active { get; private set; }
    }
}
