using System;
using System.Collections.Generic;
using OpenTK;

namespace HdGame
{
    public class RigidBody : Component
    {
        // if not null this function is used to check if two gameobjects can collide
        public Func<GameObject, bool> CollisionMask;

        public List<Bounds> Hitboxes { get; set; }

        public Vector2 Velocity { get; set; }

        public bool UseGravity { get; set; }
        public bool TriggersOnly { get; set; }

        // TODO: forces

        public RigidBody()
        {
            Hitboxes = new List<Bounds>();
            Velocity = Vector2.Zero;
        }

        public override void Update()
        {
            GameObject.Transform.LastPosition = GameObject.Transform.Position;
            var velocity = Velocity;
            if (UseGravity)
                velocity += Physics.Gravity;
            GameObject.Transform.Position += velocity*GameManager.Instance.DeltaTime;
        }

        public override void Start()
        {
        }

        public override Component Clone()
        {
            var cl = (RigidBody) MemberwiseClone();
            cl.Hitboxes = new List<Bounds>();
            foreach (var hitbox in Hitboxes)
                cl.Hitboxes.Add(hitbox.Clone());
            return cl;
        }

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            if (!TriggersOnly)
                GameObject.Transform.Position = GameObject.Transform.LastPosition;
        }

    }
}