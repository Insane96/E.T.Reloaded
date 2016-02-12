using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager.Instance.AddObject("game", new Game());
            GameManager.Instance.Run();
        }
    }
}
