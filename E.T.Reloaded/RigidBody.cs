using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace E.T.Reloaded
{
    public class RigidBody : IUpdatable
    {
        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        protected Obj2D parent;
        protected float friction;
        protected float minSpeed;
        protected float maxSpeed;

        public RigidBody(Obj2D parentObj)
        {
            parent = parentObj;
            friction = 880f;
            minSpeed = 0.5f;
            maxSpeed = 200.0f;
        }

        public void AddVelocity(Vector2 velAmount)
        {
            Velocity += velAmount;
        }

        protected float ComputeSpeed(ref float speedVal)
        {

            if (speedVal > 0.0f)
            {
                speedVal -= friction * GameManager.window.deltaTime;
                if (speedVal < minSpeed)
                    speedVal = 0.0f;
                else if (speedVal > maxSpeed)
                    speedVal = maxSpeed;
            }
            else if (speedVal < 0.0f)
            {
                speedVal += friction * GameManager.window.deltaTime;
                if (speedVal > -minSpeed)
                    speedVal = 0.0f;
                else if (speedVal < -maxSpeed)
                    speedVal = -maxSpeed;
            }

            return speedVal;

<<<<<<< HEAD
        }

        public void Update()
        {
            ComputeSpeed(ref velocity.X);
            float newX = parent.Position.X + velocity.X * GameManager.window.deltaTime;

            ComputeSpeed(ref velocity.Y);
            velocity.Y += 1290.0f * GameManager.window.deltaTime;
            float newY = parent.Position.Y + velocity.Y * GameManager.window.deltaTime;

            parent.Position = new Vector2(newX, newY);
        }
=======
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
>>>>>>> parent of ddaa95f... Add FlyingBike
    }
}
