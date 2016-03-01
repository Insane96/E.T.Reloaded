using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    class FlappyBirdRandom : FlappyBird
    {
        private float speed2;

        public FlappyBirdRandom()
        {
            speed2 = RandomGenerator.Randomize(500, 1000);
        }
    }
}
