using System;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
	public class Player:Sprite2D
	{  
		public RigidBody rigidBody{ get; set;}

		public Player (int x = 10, int y = 10):base("../../Assets/RFVF_Rosso_1.png",x,y)
		{
			Position = sprite.position;
			width = texture.Width;
			height = texture.Height;
			rigidBody = new RigidBody ();
		}
	}
}

