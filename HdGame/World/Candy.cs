using System.Collections.Generic;

namespace HdGame
{
    public class Candy : GameObject
    {
        private static List<TexturePart> possibleTextures;
        public static float Width = 0.75f;
        public Candy()
        {
            if (possibleTextures == null)
                possibleTextures = TexturePart.LoadSpriteSheet("candies.png", 4, 1);
            var texture = Utils.ChooseRandom(possibleTextures);
            ((MeshRenderer) AddComponent(new MeshRenderer(Width, Width * texture.Height/texture.Width))).CurrentTexture = texture;
            ((RigidBody) AddComponent(new RigidBody())).Hitboxes.Add(Bounds.FromGameObject(this));
        }

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            Game.Player.PickCandy(this);
            Destroy();
        }
    }
}