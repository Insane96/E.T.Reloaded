using System;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
<<<<<<< HEAD
    public class Player : Sprite2D
    {
        //public RigidBody rigidBody { get; set; }

        public Player(int x = 10, int y = 10) : base("../../Assets/RFVF_Rosso_1.png", x, y)
        {
            Position = sprite.position;
            width = texture.Width;
            height = texture.Height;
            //rigidBody = new RigidBody();
        }
    }
=======
	public class Player:Sprite2D, IUpdatable
	{  
		public RigidBody rigidBody{ get; set;}

		public Player (int x = 0, int y = 0):base("../../Assets/RFVF_Rosso_1.png",x,y)
		{
			Position = sprite.position;
			width = texture.Width;
			height = texture.Height;
			rigidBody = new RigidBody ();
		}

		private bool IsGrounded ()
		{
			// check if the collision line is below the player
			if (sprite.position.Y + 1 >= rigidBody.collisionLine)
				return true;
			return false;
		}

		public void Update ()
		{
			// horizontal movements
			if (GameManager.window.GetKey (KeyCode.Right))
				sprite.position.X += rigidBody.speed * GameManager.window.deltaTime;

			if (GameManager.window.GetKey (KeyCode.Left))
				sprite.position.X -= rigidBody.speed * GameManager.window.deltaTime;



			// check for space press
			bool space = GameManager.window.GetKey (KeyCode.Space);
			if (space) {
				if (rigidBody.jumpReleased && rigidBody.jumping <= 0 && IsGrounded ()) {
					rigidBody.jumping = rigidBody.jumpTime;
					rigidBody.jumpReleased = false;
				}
				rigidBody.jumpReleased = false;
			} else {
				rigidBody.jumpReleased = true;
			}

			// jump deceleration (starts strong, ends weaker)
			if (rigidBody.jumping > 0) {
				if (!space) {
					rigidBody.jumping = 0;
				} else {
					// the multiplication by "jumping" allows deceleration simulation, as jumping decreases autoatically at each timestep
					sprite.position.Y -= rigidBody.jumpForce * rigidBody.jumping * GameManager.window.deltaTime;
					rigidBody.jumping -= GameManager.window.deltaTime;
				}
			}

			// gravity acceleration simulation (the more time passes the stronger gravity will be)
			sprite.position.Y += rigidBody.gravity * GameManager.window.deltaTime;
			rigidBody.gravity += rigidBody.gravityForce * GameManager.window.deltaTime;


			// check collisions (fake, just check for Y)
			if (sprite.position.Y > rigidBody.collisionLine) {
				sprite.position.Y = rigidBody.collisionLine;
				rigidBody.gravity = 0;
			}
		}
	}
>>>>>>> parent of ddaa95f... Add FlyingBike
}
