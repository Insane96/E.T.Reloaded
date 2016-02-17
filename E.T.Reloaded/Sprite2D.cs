using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.T.Reloaded
{
	public class Sprite2D:Obj2D, IDrawable
    {
		public Texture texture{ get; set;}
		public Sprite sprite{ get; set;}

        public Sprite2D(string fileName, int x, int y):base(x,y,0,0)
        {
			texture = new Texture (fileName);
			sprite = new Sprite (texture.Width, texture.Height);
        }

        public void Draw()
        {
			sprite.DrawTexture (texture, 0, 0, texture.Width, texture.Height);
        }
    }
}
