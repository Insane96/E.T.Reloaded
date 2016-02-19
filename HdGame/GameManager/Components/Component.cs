namespace HdGame
{
    public abstract class Component
    {
        public bool Started { get; set; }
        public GameObject GameObject { get; set; }

        public virtual void Update()
        {
        }

        public virtual void Start()
        {
        }

        public virtual Component Clone()
        {
            return (Component) MemberwiseClone();
        }

        public virtual void OnCollision(Collision collision)
        {
        }
    }
}