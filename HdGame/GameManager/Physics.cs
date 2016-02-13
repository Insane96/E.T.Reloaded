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
            var pairs = new List<Tuple<GameObject, GameObject>>();
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
                    pairs.Add(Tuple.Create(obj1, obj2));
                }
            }
            // check performance, if too slow switch to on-demand calculations
            foreach (var pair in pairs)
            {
                foreach (var bound1 in pair.Item1.Hitboxes)
                {
                    foreach (var bound2 in pair.Item2.Hitboxes)
                    {
                        if (CheckBoundsCollision(
                            bound1 + pair.Item1.position, 
                            bound2 + pair.Item2.position))
                        {
                            var collision1 = new Collision(pair.Item2, bound1, bound2);
                            var collision2 = new Collision(pair.Item1, bound2, bound1);
                            pair.Item1.Collisions.Add(collision1);
                            pair.Item2.Collisions.Add(collision2);
                            pair.Item1.OnCollision(collision1);
                            pair.Item2.OnCollision(collision2);
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
    }
}