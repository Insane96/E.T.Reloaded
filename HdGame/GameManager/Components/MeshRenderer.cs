using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class MeshRenderer : Component
    {
        public MeshRenderer(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public TexturePart CurrentTexture { get; set; }

        public Sprite Sprite { get; protected set; }

        public float Width { get; }
        public float Height { get; }

        public Vector2 Size => new Vector2(Width, Height);

        public override void Update()
        {
            if (CurrentTexture != null) { 
                if (Sprite == null)
                    Sprite = new Sprite(Width, Height);
                Sprite.position = GameObject.Transform.Position;
                CurrentTexture.Draw(Sprite);
            }
        }

        public override void Start()
        {
        }

        public override Component Clone()
        {
            var mr = new MeshRenderer(Width, Height) {CurrentTexture = CurrentTexture};
            return mr;
        }
    }
}