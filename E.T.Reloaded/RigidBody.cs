using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
    public class RigidBody:IUpdatable
    {
		Player player;
		// when higher than 0, the player is in jump mode
		private float jumping;

		// check if the jump key is pressed or not
		private bool jumpReleased = true;

		// how much time we can stay in jump mode
		private float jumpTime = 0.3f;

		private float gravityForce = 2000f;
		private float gravity;

		// how much force we generate at the start of the jump
		private float jumpForce = 3500f;

		// horizontal speed
		private float speed = 300;

		// fake collision
		private float collisionLine = 640;

		/*
		public Vector2 position {
			get {
				return this.sprite.position;
			}
			set {
				this.sprite.position = value;
			}
		}
		*/

		public RigidBody ()
		{
			player = new Player ();
			//player.sprite.scale = new Vector2 (0.2f, 0.2f);
		}

		private bool IsGrounded ()
		{
			// check if the collision line is below the player
			if (player.sprite.position.Y + 1 >= collisionLine)
				return true;
			return false;
		}

		public void Update ()
		{

			// horizontal movements
			if (GameManager.window.GetKey (KeyCode.Right))
				player.sprite.position.X += speed * GameManager.window.deltaTime;

			if (GameManager.window.GetKey (KeyCode.Left))
				player.sprite.position.X -= speed * GameManager.window.deltaTime;



			// check for space press
			bool space = GameManager.window.GetKey (KeyCode.Space);
			if (space) {
				if (jumpReleased && jumping <= 0 && IsGrounded()) {
					jumping = jumpTime;
					jumpReleased = false;
				}
				jumpReleased = false;
			} else {
				jumpReleased = true;
			}

			// jump deceleration (starts strong, ends weaker)
			if (jumping > 0) {
				if (!space) {
					jumping = 0;
				} else {
					// the multiplication by "jumping" allows deceleration simulation, as jumping decreases autoatically at each timestep
					player.sprite.position.Y -= this.jumpForce * jumping * GameManager.window.deltaTime;
					jumping -= GameManager.window.deltaTime;
				}
			}

			// gravity acceleration simulation (the more time passes the stronger gravity will be)
			player.sprite.position.Y += this.gravity * GameManager.window.deltaTime;
			this.gravity += this.gravityForce * GameManager.window.deltaTime;


			// check collisions (fake, just check for Y)
			if (player.sprite.position.Y > collisionLine) {
				player.sprite.position.Y = collisionLine;
				this.gravity = 0;
			}


			// draw
			player.sprite.DrawTexture (player.texture, 0, 0, player.texture.Width, player.texture.Height);
		}
    }
}
