namespace HdGame
{
    public class Collision
    {
        public GameObject GameObject { get; } // other gameobject
        public Bounds OwnerBounds { get; }
        public Bounds OtherBounds { get; }

        public Collision(GameObject gameObject, Bounds ownerBounds, Bounds otherBounds)
        {
            GameObject = gameObject;
            OwnerBounds = ownerBounds;
            OtherBounds = otherBounds;
        }
    }
}