using OpenTK;

namespace HdGame
{
    public class Bounds
    {
        public string Name { get; set; }
        public Vector2 Center { get; }
        public Vector2 Extents { get; }
        public Vector2 Min { get; }
        public Vector2 Max { get; }
        public Vector2 Size { get; }
        public Bounds(Vector2 center, Vector2 extents)
        {
            Center = center;
            Extents = extents;
            Min = center - extents;
            Max = center + extents;
            Size = extents*2;
        }

        public Bounds(string name, Vector2 center, Vector2 extents) : this(center, extents)
        {
            Name = name;
        }

        public Bounds(GameObject gameObject) :
            this(new Vector2(gameObject.Width / 2, gameObject.Height / 2),
                new Vector2(gameObject.Width / 2, gameObject.Height / 2))
        { }
        public Bounds(TexturePart texturePart) :
            this(new Vector2(texturePart.Width / 2, texturePart.Height / 2),
                new Vector2(texturePart.Width / 2, texturePart.Height / 2))
        { }

        public static Bounds operator +(Bounds b, Vector2 v)
        {
            return new Bounds(b.Center + v, b.Extents);
        }

        private Bounds Clone()
        {
            return (Bounds) MemberwiseClone();
        }
    }
}