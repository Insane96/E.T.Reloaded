using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace HdGame
{
    public class CharacterMovement : Behaviour
    {
        private enum CharacterStates
        {
            Idle, MovingLeft, MovingRight, MovingDown, MovingUp
        }

        public float MinVelocity = 0.75f;
        public Vector2 MovingVector { get; set; }
        public float Speed { get; set; }

        public override void Update()
        {
            base.Update();
            MovementHandler();
        }

        private void MovementHandler()
        {
            var stateRenderer = (StateRenderer) GameObject.GetComponent<StateRenderer>();
            MovingVector.Normalize();
            var velocityVector = Vector2.Zero;
            // use length? this should be faster?
            if (MovingVector.X != 0 || MovingVector.Y != 0)
            {
                velocityVector = MovingVector * Speed;
            }
            else if (velocityVector.LengthFast > MinVelocity)
            {
                // should stop in 0.25s
                velocityVector = Vector2.Lerp(velocityVector, Vector2.Zero, GameManager.Instance.DeltaTime * 4);
            }
            if (velocityVector.LengthFast <= MinVelocity)
            {
                velocityVector = Vector2.Zero;
                stateRenderer.CurrentState = (int)CharacterStates.Idle;
            }
            else
            {
                if (Math.Abs(velocityVector.Y) > Math.Abs(velocityVector.X))
                    stateRenderer.CurrentState = (int)(velocityVector.Y >= 0 ? CharacterStates.MovingDown : CharacterStates.MovingUp);
                else
                    stateRenderer.CurrentState = (int)(velocityVector.X >= 0 ? CharacterStates.MovingRight : CharacterStates.MovingLeft);
            }
            ((RigidBody)GameObject.GetComponent<RigidBody>()).Velocity = velocityVector;
        }
    }
}
