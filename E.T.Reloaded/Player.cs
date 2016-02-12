using System;
using Aiv.Fast2D;
using OpenTK;

namespace AttritoGravita
{
	public class Player
	{
		Texture p1Text;
		public Sprite p1Mash;
		Obj2D p1;
		/*
		 Vector2 newPos;
		float frequency = 0.2f;
		public int indexY;
		public int indexX;
		float t;
		float scroll;
		int startIndexX; 
		int finalIndexX;
		int startIndexY; 
		int finalIndexY;
		*/
		public Player ()
		{
			p1Text = new Texture ("../../Assets/RFVF_Rosso_1.png");
			p1Mash = new Sprite (p1Text.Width, p1Text.Height);
			p1Mash.scale = new Vector2 (3f, 3f);
			p1.Position = p1Mash.position;
		}
			

		public void Update()
		{
			if (GameManager.window.GetKey (KeyCode.Right))
				p1.x += 50;

			if (GameManager.window.GetKey (KeyCode.Left))
				p1.x -= 50;

				p1Mash.position = p1.Position;
		}
		public void Draw()
		{
			p1Mash.DrawTexture (p1Text, p1Text.Width, p1Text.Height, p1.width, p1.height);
		}
	}
}

