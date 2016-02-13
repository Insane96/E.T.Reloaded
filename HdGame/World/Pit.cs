using System.Collections.Generic;
using OpenTK;

namespace HdGame
{
    public class Pit : GameObject
    {
        private static int pitCount;
        private readonly float pitZoneHeight = 16.66f;
        private Vector2 pitZoneSize;
        private Vector2 pitZonePosition;
        private PitZone pitZone;

        public Pit(float width, float height) : base(width, height)
        {
        }

        public override void Start()
        {
            base.Start();
            Hitboxes.Add(new Bounds(this));
            InitPitZone();
        }

        private void InitPitZone()
        {
            pitCount++;
            pitZoneSize = new Vector2(
                GameManager.Instance.Window.aspectRatio * pitZoneHeight, pitZoneHeight);
            pitZonePosition = World.Boundings.Max + new Vector2(pitZoneSize.X*3, 0f) * pitCount;
        }

        private void CreatePitZone()
        {
            pitZone = new PitZone(this, pitZonePosition, pitZoneSize);
            GameManager.Instance.AddObject($"{Name}_pitZone", pitZone);
        }

        public Pit Clone()
        { // not a complete clone, but we're cloning what we need here
            var pit = new Pit(Width, Height);
            foreach (var state in States)
            {
                pit.States.Add(state.Clone());
            }
            return pit;
        }
        public override void OnCollision(Collision collision)
        {
            var player = collision.GameObject as Player;
            if (player != null)
            {
                PitFall(player);
            }
        }

        private void PitFall(Player player)
        {
            if (Timer.Get("pitFallDelay") > 0) return;
            if (pitZone == null)
            {
                CreatePitZone();
            }
            pitZone.LastPlayerPosition = player.LastPosition;
            player.position = pitZonePosition + 
                new Vector2(pitZoneSize.X/2, pitZoneSize.Y - player.Height);
            player.LastPosition = player.position;
            GameManager.Instance.Camera.position = player.position;
        }
    }

    public class PitZone : GameObject
    {
        private Pit pit;
        public Vector2 LastPlayerPosition { get; set; }

        public PitZone(Pit pit, Vector2 pitZonePosition, Vector2 pitZoneSize) : 
            base(pitZoneSize.X, pitZoneSize.Y)
        {
            this.pit = pit;

            position = pitZonePosition;
            var backgroundTexture = new TexturePart("box.png");
            States.Add(new StateManager() { Textures = new List<TexturePart> {backgroundTexture} });

            // hitboxes
            var hitBoxDepth = 2f;
            Hitboxes.Add(
                new Bounds(
                    new Vector2(pitZoneSize.X / 2, pitZoneSize.Y + hitBoxDepth / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth / 2))); // bottom
            Hitboxes.Add(
                new Bounds(
                    "exit", 
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth / 2))); // top
            Hitboxes.Add(
                new Bounds(
                    new Vector2(hitBoxDepth / 2, pitZoneSize.Y / 2),
                    new Vector2(hitBoxDepth / 2, pitZoneSize.Y / 2))); // left
            Hitboxes.Add(
                new Bounds(
                    new Vector2(hitBoxDepth / 2 + pitZoneSize.X, pitZoneSize.Y / 2),
                    new Vector2(hitBoxDepth / 2, pitZoneSize.Y / 2))); // right
        }

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            var player = collision.GameObject as Player;
            if (collision.OwnerBounds.Name == "exit" && player != null)
            {
                player.position = LastPlayerPosition;
                player.LastPosition = player.position;
                GameManager.Instance.Camera.position = player.position;
                pit.Timer.Set("pitFallDelay", 2f);
            }
        }
    }
}