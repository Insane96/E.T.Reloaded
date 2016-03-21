using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
    public class Room
    {
        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }
        public Hole hCurrent { get; set; }



        public Player Player { get; set; }

        protected Texture background;
        protected Sprite forestMesh;
        private int width;
        private int height;
        private string name;
        public List<Hole> Holes { get; set; }

        public Room(string name, int iWidth, int iHeight) : this(name, iWidth, iHeight, new List<Hole>())
        {

        }

        public Room(string sName, int iWidth, int iHeight, List<Hole> hHoles)
        {
            width = iWidth;
            height = iHeight;
            this.name = sName;
            background = new Texture(name);
            forestMesh = new Sprite(width, height);
            Holes = hHoles;
        }

        public Room Update()
        {
            Player.Update();

            if (Player.EtMesh.position.Y + Player.EtMesh.Height < 0 && this.North != null)
            {
                if (Player.InHole == null)
                    Player.EtMesh.position = new Vector2(Player.EtMesh.position.X, height - Player.EtMesh.Height);
                else
                {
                    Player.EtMesh.position = Player.InHole.SpawingPoint;
                    Player.InHole = null;
                }

                this.North.Player = this.Player;     //sposti il giocatore nell'altra stanza
                this.Player = null;                //il giocatore non sta piu in questa stanza


                return this.North;
            }
            else if (Player.EtMesh.position.X + Player.EtMesh.Width > width && this.East != null)
            {

                Player.EtMesh.position = new Vector2(0, Player.EtMesh.position.Y);


                this.East.Player = this.Player;     //sposti il giocatore nell'altra stanza
                this.Player = null;                 //il giocatore non sta piu in questa stanza
                return this.East;
            }
            else if (Player.EtMesh.position.Y + Player.EtMesh.Height > height && this.South != null)
            {
                Player.EtMesh.position = new Vector2(Player.EtMesh.position.X, 0);


                this.South.Player = this.Player;
                this.Player = null;
                return this.South;
            }
            else if (Player.EtMesh.position.X + Player.EtMesh.Width < 0 && this.West != null)
            {
                Player.EtMesh.position = new Vector2(width - Player.EtMesh.Width, Player.EtMesh.position.Y);

                this.West.Player = this.Player;
                this.Player = null;
                return this.West;
            }
            else
            {
                if (Holes.Count > 0)
                {
                    for (int i = 0; i < Holes.Count; i++)
                    {
                        hCurrent = Holes[i];

                        if (hCurrent.CheckFall(Player))
                        {
                            //set spawn position in the next room
                            Player.EtMesh.position = new Vector2(width / 2, height / 5);
                            hCurrent.Room.Player = this.Player;
                            Player.InHole = hCurrent;
                            this.Player = null;
                            return hCurrent.Room;
                        }
                    }
                }


                return this;
            }



            //else if ((index == 1) && (this.firstHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 149) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 149 + 91) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 224) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 224 + 100)) || 
            //        (index == 1) && (this.firstHole != null) &&
            //        ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 720) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 720 + 91) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 224) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 224 + 100)))
            //{ 
            //    this.firstHole.Player = this.Player;
            //    this.Player = null;
            //    return this.firstHole;
            //}
            //else if ((index == 2) && (this.secondHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 659) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 659 + 121) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 249) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 249 + 50)) ||
            //        (index == 2) && (this.secondHole != null) &&
            //        ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 178) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 178 + 120) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 249) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 249 + 50)) ||
            //        (index == 2) && (this.secondHole != null) &&
            //        ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 659) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 659 + 121) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 115) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 115 + 50)) ||
            //        (index == 2) && (this.secondHole != null) &&
            //        ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 178) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 178 + 120) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 115) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 115 + 50)))
            //{
            //    this.secondHole.Player = this.Player;
            //    this.Player = null;
            //    return this.secondHole;
            //}
            //else if ((index == 3) && (this.thirdHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 659) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 659 + 150) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 373) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 373 + 23)) ||
            //        (index == 3) && (this.thirdHole != null) &&
            //        ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 149) &&
            //        (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 149 + 50) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 373) &&
            //        (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 373 + 23)))
            //{
            //    this.thirdHole.Player = this.Player;
            //    this.Player = null;
            //    return this.thirdHole;
            //}
            //else if ((index == 4) && (this.fourthHole != null) &&
            //      ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 691) &&
            //       (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 691 + 59) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 382) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 382 + 50)) ||
            //       (index == 4) && (this.fourthHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 209) &&
            //       (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 209 + 60) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 382) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 382 + 50))  ||
            //       (index == 4) && (this.fourthHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 419) &&
            //       (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 419 + 120) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 82) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 82 + 44)) ||
            //       (index == 4) && (this.fourthHole != null) &&
            //       ((this.Player.EtMesh.position.X + this.Player.EtMesh.Width >= 419) &&
            //       (this.Player.EtMesh.position.X + this.Player.EtMesh.Width <= 419 + 120) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height >= 424) &&
            //       (this.Player.EtMesh.position.Y + this.Player.EtMesh.Height <= 424 + 40)))
            //{
            //    this.fourthHole.Player = this.Player;
            //    this.Player = null;
            //    return this.fourthHole;
            //}
            //else
            //    return this;
        }

        public virtual void Draw()
        {
            //Console.WriteLine(this);
            forestMesh.DrawTexture(background);
            Player.Draw();
        }


        public override string ToString() => name;
    }
}
