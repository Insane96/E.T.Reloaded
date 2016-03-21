using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    class Program
    {
        private const int Width = 960;
        private const int Height = 540;

        static void Main(string[] args)
        {
            Window window = new Window(Width, Height, "Animations", false);
            GameLevel level = new ForestLevel(window);

            while (window.opened)
            {
                level.Update();
                level.Draw();

                if (window.GetKey(KeyCode.Esc))
                    break;

                window.Update();
            }
        }
    }
}



