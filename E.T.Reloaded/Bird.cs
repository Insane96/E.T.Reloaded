using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    class Bird : WalkingCharacter, IUpdatable
    {
        private int speed = 200;

        public Bird()
        {
            sprite.position.X = GameManager.window.Width + sprite.Width;
            sprite.position.Y = RandomGenerator.Randomize(0, GameManager.window.Height - (int)sprite.Height);
        }

        public void Update()
        {
            sprite.position.X -= speed * GameManager.window.deltaTime;

            if (sprite.position.X + sprite.Width < 0)
            {
                sprite.position.X = GameManager.window.Width + sprite.Width;
                sprite.position.Y = RandomGenerator.Randomize(0, GameManager.window.Height - (int)sprite.Height);
            }
        }
    }
}
