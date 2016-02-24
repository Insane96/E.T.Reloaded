using System.Collections.Generic;

namespace HdGame
{
    public class Phone : GameObject
    {
        private static List<TexturePart> possibleTextures;
        public static float Width = 0.9f;
        public Phone()
        {
            if (possibleTextures == null)
                possibleTextures = new List<TexturePart>
                {
                    new TexturePart("phone0.png"),
                    new TexturePart("phone1.png")
                    //new TexturePart("phone2.png")
                };
            var texture = Utils.ChooseRandom(possibleTextures);
            ((MeshRenderer)AddComponent(new MeshRenderer(Width, Width * texture.Height / texture.Width))).CurrentTexture = texture;
            ((RigidBody)AddComponent(new RigidBody())).Hitboxes.Add(Bounds.FromGameObject(this));
        }

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            Game.Player.PickPhone(this);
        }
    }
}