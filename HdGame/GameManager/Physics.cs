using System;
using System.Collections;
using System.Collections.Generic;

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
                if (obj1.Hitboxes.Count == 0)
                    continue;
                foreach (GameObject obj2 in objects)
                {
                    // untested mask
                    if ((obj1.CollisionMask != null && !obj1.CollisionMask(obj2)) ||
                        (obj2.CollisionMask != null && !obj2.CollisionMask(obj1)))
                        continue;

                    foreach (var bound1 in obj1.Hitboxes)
                    {
                        foreach (var bound2 in obj2.Hitboxes)
                        {
                            if (CheckBoundsCollision(
                                bound1 + obj1.position,
                                bound2 + obj2.position))
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
            foreach (var bound1 in gobj1.Hitboxes)
                foreach (var bound2 in gobj2.Hitboxes)
                    if (CheckBoundsCollision(bound1 + gobj1.position, bound2 + gobj2.position))
                        return true;
            return false;
        }
    }
}