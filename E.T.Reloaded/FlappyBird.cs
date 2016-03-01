using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace E.T.Reloaded
{
    class FlappyBird : WalkingCharacter, IUpdatable
    {
        private float speed;
        private float topLineY;
        private float downLineY;

        private bool check;
        private bool check2;

        public FlappyBird()
        {
            x = GameManager.window.Width + sprite.Width;
            y = RandomGenerator.Randomize(0, GameManager.window.Height - (int)sprite.Height - 150);

            topLineY = y - 150;
            downLineY = y + 150;
            speed = 200;
        }

        public void Update()
        {
            rigidBody.AddVelocity(new Vector2(-speed, 0));

            if (sprite.position.Y >= downLineY || check)
            {
                rigidBody.AddVelocity(new Vector2(0, -speed));
                check = true;
                check2 = false;
            }

            if (sprite.position.Y <= topLineY || check2)
            {
                check = false;
                check2 = true;
            }

            rigidBody.Update();
            sprite.position = Position;

            if (x + sprite.Width < 0)
            {
                x = GameManager.window.Width + sprite.Width;
                y = RandomGenerator.Randomize(0, GameManager.window.Height - (int)sprite.Height - 150);
            }
        }
    }
}
