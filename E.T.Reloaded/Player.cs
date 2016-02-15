using System;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
	public class Player
	{
		public Texture texture;
		public Sprite sprite;
		Obj2D obj2D;
		public Player ()
		{
			texture = new Texture ("../../Assets/RFVF_Rosso_1.png");
			sprite = new Sprite (texture.Width, texture.Height);
			sprite.scale = new Vector2 (3f, 3f);
			obj2D.Position = sprite.position;
		}
	}
}

