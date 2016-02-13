using System;
using OpenTK;

namespace HdGame
{
    public class Character : GameObject
    {
        private float minVelocity = 0.1f;
        protected Vector2 MovingVector { get; set; }
        protected Vector2 VelocityVector { get; private set; }
        protected float Speed { get; set; }
        private enum CharacterStates
        {
            Idle, MovingLeft, MovingRight, MovingDown, MovingUp
        }

        public Character(float width, float height) : base(width, height)
        {
            
        }

        public override void Update()
        {
            base.Update();
            MovementHandler();
        }

        private void MovementHandler()
        {
            LastPosition = position;
            MovingVector.Normalize();
            // use length? this should be faster?
            if (MovingVector.X != 0 || MovingVector.Y != 0)
            {
                VelocityVector = MovingVector*Speed;
            }
            else if (VelocityVector.LengthFast > minVelocity)
            {
                // should stop in 0.25s
                VelocityVector = Vector2.Lerp(VelocityVector, Vector2.Zero, GameManager.Instance.DeltaTime*4);
            }
            if (VelocityVector.LengthFast <= minVelocity)
            {
                VelocityVector = Vector2.Zero;
                CurrentState = (int) CharacterStates.Idle;
            } else
            {
                position += VelocityVector * GameManager.Instance.DeltaTime;
                if (Math.Abs(VelocityVector.Y) > Math.Abs(VelocityVector.X))
                    CurrentState = (int) (VelocityVector.Y >= 0 ? CharacterStates.MovingDown : CharacterStates.MovingUp);
                else
                    CurrentState = (int) (VelocityVector.X >= 0 ? CharacterStates.MovingRight : CharacterStates.MovingLeft);
            }
        }
        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            position = LastPosition;
        }

        public Vector2 LastPosition { get; private set; }
    }
}