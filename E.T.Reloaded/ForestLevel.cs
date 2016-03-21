using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
    public class ForestLevel : GameLevel
    {
        private Window window;
        public ForestLevel(Window window)
        {
            this.window = window;
            Room forest = new Room("../../Assets/Forest_0.png", window.Width, window.Height);
            Room bush1  = new Room("../../Assets/Forest_1.png", window.Width, window.Height);
            Room bush2  = new Room("../../Assets/Forest_2.png", window.Width, window.Height);
            Room bush3  = new Room("../../Assets/Forest_3.png", window.Width, window.Height);
            Room bush4  = new Room("../../Assets/Forest_4.png", window.Width, window.Height);
            Room city   = new Room("../../Assets/Forest_5.png", window.Width, window.Height);
            Room hole   = new Room("../../Assets/Forest_6.png", window.Width, window.Height);

            forest.North = bush1;
            forest.South = bush3;
            forest.East = bush4;
            forest.West = bush2;

            bush1.North = forest;
            bush1.South = city;
            bush1.East = bush2;
            bush1.West = bush4;
            

            bush2.North = forest;
            bush2.South = city;
            bush2.East = bush3;
            bush2.West = bush1;
            

            bush3.North = forest;
            bush3.South = city;
            bush3.East = bush4;
            bush3.West = bush2;
            

            bush4.North = forest;
            bush4.South = city;
            bush4.East = bush1;
            bush4.West = bush3;
            

            city.North = bush1;
            city.South = bush3;
            city.East = bush2;
            city.West = bush4;



            bush1.Holes.Add(new Hole(new Vector2(200, 264), 110, 130, hole, new Vector2(100, 400)));
            bush1.Holes.Add(new Hole(new Vector2(770, 264), 110, 130, hole, new Vector2(600, 400)));
            bush1.Holes.Add(new Hole(new Vector2(500, 200), 80, 60, hole, new Vector2(300, 200)));
            bush1.Holes.Add(new Hole(new Vector2(500, 407), 80, 60, hole, new Vector2(300, 350)));
            hole.North = bush1;

            bush2.Holes.Add(new Hole(new Vector2(180, 117), 120, 50, hole, new Vector2(200, 400)));
            bush2.Holes.Add(new Hole(new Vector2(180, 382), 120, 50, hole, new Vector2(600, 400)));
            bush2.Holes.Add(new Hole(new Vector2(660, 382), 120, 60, hole, new Vector2(300, 200)));
            bush2.Holes.Add(new Hole(new Vector2(660, 117), 120, 60, hole, new Vector2(300, 350)));
            //hole.North = bush2;

            //bush3.Holes.Add(new Hole(new Vector2(200, 264), 110, 130, hole, new Vector2(100, 400)));
            //bush3.Holes.Add(new Hole(new Vector2(770, 264), 110, 130, hole, new Vector2(600, 400)));
            //bush3.Holes.Add(new Hole(new Vector2(500, 200), 80, 60, hole, new Vector2(300, 200)));
            //bush3.Holes.Add(new Hole(new Vector2(500, 407), 80, 60, hole, new Vector2(300, 350)));
            //hole.North = bush3;

            //bush4.Holes.Add(new Hole(new Vector2(200, 264), 110, 130, hole, new Vector2(100, 400)));
            //bush4.Holes.Add(new Hole(new Vector2(770, 264), 110, 130, hole, new Vector2(600, 400)));
            //bush4.Holes.Add(new Hole(new Vector2(500, 200), 80, 60, hole, new Vector2(300, 200)));
            //bush4.Holes.Add(new Hole(new Vector2(500, 407), 80, 60, hole, new Vector2(300, 350)));
            //hole.North = bush4;



            Current = forest;//starting room
            Current.Player = new Player(window);
            Current.Player.EtMesh.position = new Vector2(window.Width / 2, window.Height / 2);
           
        }


    }
}
