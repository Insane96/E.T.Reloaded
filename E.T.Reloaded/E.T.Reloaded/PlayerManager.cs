using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
     static class PlayerManager
    {
        static Player Et;

        static PlayerManager()
        {
            //playerOne= new Player("../../Assets/pig.png", 0, 0, 1, 1);
        }

        static public Player GetPlayer()
        {
            return Et;

        }
    }
}
