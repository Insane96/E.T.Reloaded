using OpenTK;

namespace HdGame
{
    public class Transform : Component
    {
        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public Vector2 Rotation;

        public Vector2 LastPosition;
    }
}