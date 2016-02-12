using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace AttritoGravita
{
    public class RigidBody:IUpdatable
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

        public void AddVelocity(Vector2 velAmount)//modificare la velocita
        {
			Velocity.Add(velAmount);//velamount vector
        }

		protected float ComputeSpeed(ref float speedVal)//velocita corrente
        {
            
            if (speedVal > 0.0f)
            {
                speedVal -= friction * GameManager.window.deltaTime;//attrito (velocita contraria)
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

        }

		public void Update()
        {
			ComputeSpeed(ref velocity.X);
			parent.x += (int)(velocity.X * GameManager.window.deltaTime);
            Console.WriteLine("X:"+velocity.X);

			velocity.Y += 150.0f * GameManager.window.deltaTime;
			ComputeSpeed(ref velocity.Y);
             
			Console.WriteLine("Y:"+velocity.Y);
			parent.y += (int)(velocity.Y * GameManager.window.deltaTime);
        }
    }
}
