using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
    public static class GameManager
    {
		public static Window window;
<<<<<<< HEAD
		//static FlyingBike p1;
        static Bird enemyBird;
        static FlappyBird enemyFlappyBird;

        static GameManager()
        {
            window = new Window(1280, 720, "Animations");
			//p1 = new FlyingBike();
            enemyBird = new Bird();
            enemyFlappyBird = new FlappyBird();
=======
		static Player p1;

        static GameManager()
        {
            window = new Window(720, 720, "Animations");
			p1 = new Player();
>>>>>>> parent of ddaa95f... Add FlyingBike
        }

        public static void Play()
        {
            while (window.opened)
            {
				//p1.Update ();
				//p1.Draw ();

                enemyBird.Update();
                enemyBird.Draw();

                enemyFlappyBird.Update();
                enemyFlappyBird.Draw();

                window.Update();
            }
        }
    }
}
