using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
    public class Player
    {
        public float nrg { get; set; }
        public Texture texture;
        public Sprite EtMesh;
        public float speed;
        public Window window;
        public Hole InHole { get; set; }


        public Player(Window window)
        {
            this.window = window;

            this.texture = new Texture("../../Assets/prova.png");
            EtMesh = new Sprite(texture.Width, texture.Height);
            EtMesh.scale = new Vector2(0.5f, 0.5f);
            speed = 500;
            Console.WriteLine(this);
        }


        public void Draw()
        {
            EtMesh.DrawTexture(texture);
        }

        public void Update()
        {
            if (window.GetKey(KeyCode.Space))
            {
                EtMesh.position.Y = window.mouseY;
                EtMesh.position.X = window.mouseX;
                Console.WriteLine(EtMesh.position.X + "posizione X");
                Console.WriteLine(EtMesh.position.Y + "posizione Y");
            }
            if (window.GetKey(KeyCode.Up))
            {
                EtMesh.position.Y -= speed * window.deltaTime;
                //Console.WriteLine(EtMesh.position.Y + "posizione Y");
            }
            if (window.GetKey(KeyCode.Down))
            {
                EtMesh.position.Y += speed * window.deltaTime;
                //Console.WriteLine(EtMesh.position.Y + "posizione Y");
            }
            if (window.GetKey(KeyCode.Right))
            {
                EtMesh.position.X += speed * window.deltaTime;
                //Console.WriteLine(EtMesh.position.X + "posizione X");
            }
            if (window.GetKey(KeyCode.Left))
            {
                EtMesh.position.X -= speed * window.deltaTime;
                //Console.WriteLine(EtMesh.position.X + "posizione X");
            }


        }
    }
}
