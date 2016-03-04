using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace E.T.Reloaded
{
    public class RigidBody
    {
		// when higher than 0, the player is in jump mode
		public float jumping{get; set;}

		// check if the jump key is pressed or not
		public bool jumpReleased {get; set;}

		// how much time we can stay in jump mode
		public float jumpTime {get; set;}

		public float gravityForce {get; set;}
		public float gravity{get; set;}

		// how much force we generate at the start of the jump
		public float jumpForce{get; set;}

		// horizontal speed
		public float speed{get; set;}

		// fake collision
		public float collisionLine{get; set;}

		public RigidBody ()
		{
			jumpReleased = true;
			jumpTime = 0.3f;
			gravityForce = 2000f;
			jumpForce = 3500f;
			speed = 300;
			collisionLine = 640;
		}
    }
}
