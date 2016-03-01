using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    public class WalkingCharacter : Sprite2D
    {
        protected RigidBody rigidBody;

        public WalkingCharacter(int x = 10, int y = 10) : base("../../Assets/Bird.png", x, y)
        {
            //Position = sprite.position;
            //width = texture.Width;
            //height = texture.Height;
            rigidBody = new RigidBody(this);
        }
    }
}
