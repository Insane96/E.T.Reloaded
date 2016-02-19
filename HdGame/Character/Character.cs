using System;
using OpenTK;

namespace HdGame
{
    public class Character : GameObject
    {

        public Character(float width, float height)
        {
            AddComponent(new StateRenderer(width, height));
            AddComponent(new RigidBody());
            AddComponent(new CharacterMovement());
        }

        public override void Update()
        {
            base.Update();
        }
    }
}