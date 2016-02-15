using System;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
	public class Player:Obj2D
	{
		public Texture texture;
		public Sprite sprite;

		public Player (int x = 0, int y = 0):base(x,y,0,0)
		{
			texture = new Texture ("../../Assets/RFVF_Rosso_1.png");
			sprite = new Sprite (texture.Width, texture.Height);
			sprite.scale = new Vector2 (3f, 3f);
			Position = sprite.position;
			width = texture.Width;
			height = texture.Height;
		}
	}
}

