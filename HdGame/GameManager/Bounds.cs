using System;
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

        public static Bounds FromGameObject(GameObject gameObject)
        {
            var mr = (MeshRenderer) gameObject.GetComponent<MeshRenderer>();
            Vector2 half;
            if (mr != null)
                half = new Vector2(mr.Width / 2, mr.Height / 2);
            else
                throw new Exception("Trying to instantiate Bounds from GameObject without any renderer.");
            return new Bounds(half, half);
        }
        public Bounds(TexturePart texturePart) :
            this(new Vector2(texturePart.Width / 2, texturePart.Height / 2),
                new Vector2(texturePart.Width / 2, texturePart.Height / 2))
        { }

        public static Bounds operator +(Bounds b, Vector2 v)
        {
            return new Bounds(b.Center + v, b.Extents);
        }

        public Bounds Clone()
        {
            return (Bounds) MemberwiseClone();
        }
    }
}