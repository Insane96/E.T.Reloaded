using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;

namespace HdGame
{
    public static class Physics
    {
        public static void UpdateCollisions()
        {
            var objects = new Queue(GameManager.Instance.Objects.Values);
            while (objects.Count > 0)
            {
                var obj1 = (GameObject) objects.Dequeue();
                obj1.Collisions.Clear();
                var rigid1 = (RigidBody) obj1.GetComponent<RigidBody>();
                if (rigid1 == null || rigid1.Hitboxes.Count == 0)
                    continue;
                foreach (GameObject obj2 in objects)
                {
                    var rigid2 = (RigidBody)obj2.GetComponent<RigidBody>();
                    if (rigid2 == null || rigid2.Hitboxes.Count == 0) continue;
                    if ((rigid1.CollisionMask != null && !rigid1.CollisionMask(obj2)) ||
                        (rigid2.CollisionMask != null && !rigid2.CollisionMask(obj1)))
                        continue;

                    foreach (var bound1 in rigid1.Hitboxes)
                    {
                        foreach (var bound2 in rigid2.Hitboxes)
                        {
                            if (CheckBoundsCollision(
                                bound1 + obj1.Transform.Position,
                                bound2 + obj2.Transform.Position))
                            {
                                var collision1 = new Collision(obj2, bound1, bound2);
                                var collision2 = new Collision(obj1, bound2, bound1);
                                obj1.Collisions.Add(collision1);
                                obj2.Collisions.Add(collision2);
                                obj1.OnCollision(collision1);
                                obj2.OnCollision(collision2);
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckBoundsCollision(Bounds b1, Bounds b2)
        {
            return (b1.Max.X >= b2.Min.X && b1.Min.X <= b2.Max.X &&
                    b1.Max.Y >= b2.Min.Y && b1.Min.Y <= b2.Max.Y);
        }

        internal static bool CheckObjectsCollision(List<GameObject> gameObjects)
        {
            // is this faster than UpdateCollisions's algorithm? asynth. equal but looks better?
            var queue = new Queue<GameObject>(gameObjects);
            while (queue.Count > 0)
            {
                var gameObject = queue.Dequeue();
                foreach (var otherGameObject in queue)
                    if (CheckObjectsCollision(gameObject, otherGameObject))
                        return true;
            }
            return false;
        }

        public static bool CheckObjectsCollision(GameObject gobj1, GameObject gobj2)
        {
            var rigid1 = (RigidBody) gobj1.GetComponent<RigidBody>();
            var rigid2 = (RigidBody) gobj2.GetComponent<RigidBody>();
            foreach (var bound1 in rigid1.Hitboxes)
                foreach (var bound2 in rigid2.Hitboxes)
                    if (CheckBoundsCollision(bound1 + gobj1.Transform.Position, bound2 + gobj2.Transform.Position))
                        return true;
            return false;
        }

        public static Vector2 Gravity { get; set; } = new Vector2(0f, 0.2f);
    }
}