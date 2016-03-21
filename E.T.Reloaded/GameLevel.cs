using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace E.T.Reloaded
{
    public abstract class GameLevel
    {
        protected Room Current;
        private Player player;

        public void Update()
        {
            Current = Current.Update(); //aggiorna player, aggiorna stanza corrente
        }

        public void Draw()
        {
            Current.Draw();
        }
    }
}
