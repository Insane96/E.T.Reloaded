using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace E.T.Reloaded
{
    class GameLevel
    {
        public bool IsPlaying { get; protected set; }
        public GameLevel()
        {
            IsPlaying = true;
        }

        public virtual void Reset()
        {
            IsPlaying = true;

        }

        public virtual void Update() { }

        public virtual void Draw() { }
    }
}
