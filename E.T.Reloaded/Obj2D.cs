using System;
using OpenTK;


namespace AttritoGravita
{
	public class Obj2D
	{
		private Vector2 position;
		public Vector2 Position {
			get {
				return position;
			}
			set
			{
				position = value;
			}
		}
		public int width { get; protected set; }
		public int height { get; protected set; }

		public float x
		{
			get
			{
				return position.X;
			}
			set
			{
				position.X = value;
			}
		}

		public float y
		{
			get
			{
				return position.Y;
			}
			set
			{
				position.Y = value;
			}
		}

		public Obj2D(int x, int y, int w, int h)
		{
			this.position = new Vector2(x, y);
			width = w;
			height = h;
		}
	}
}

