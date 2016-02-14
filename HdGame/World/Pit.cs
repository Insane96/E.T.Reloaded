using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        // filename, cols, rows, width, fps (height = width * aspectRatio)
        private readonly List<Tuple<string, int, int, float, int>> detailsTextures = new List<Tuple<string, int, int, float, int>>
        {
            Tuple.Create("rocks.png", 1, 1, 0.33f, 1),
            Tuple.Create("roots.png", 1, 1, 0.33f, 1)
        };
        // percentual positions (0 is the start of the pit, 1 the end)
        private List<Bounds> detailsBoundaries = new List<Bounds>
        {
            new Bounds(new Vector2(0.1f, 0.5f), new Vector2(0.075f, 0.5f)),
            new Bounds(new Vector2(1f-0.1f, 1f-0.5f), new Vector2(0.075f, 0.5f))
        }; 
        private readonly int detailsNumber = 10;
        private readonly Pit pit;
        private readonly int maxProceduralAttempts = 100;
        public Vector2 LastPlayerPosition { get; set; }

        public PitZone(Pit pit, Vector2 pitZonePosition, Vector2 pitZoneSize) : 
            base(pitZoneSize.X, pitZoneSize.Y)
        {
            this.pit = pit;

            position = pitZonePosition;
            var backgroundTexture = new TexturePart("box.png");
            States.Add(new StateManager() { Textures = new List<TexturePart> {backgroundTexture} });

            // hitboxes
            var hitBoxDepth = new Vector2(6f, 2f);
            Hitboxes.Add(
                new Bounds(
                    new Vector2(pitZoneSize.X / 2, pitZoneSize.Y + hitBoxDepth.Y / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2))); // bottom
            Hitboxes.Add(
                new Bounds(
                    "exit", 
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2))); // top
            Hitboxes.Add(
                new Bounds(
                    new Vector2(hitBoxDepth.X / 2, pitZoneSize.Y / 2),
                    new Vector2(hitBoxDepth.X / 2, pitZoneSize.Y / 2))); // left
            Hitboxes.Add(
                new Bounds(
                    new Vector2(pitZoneSize.X - hitBoxDepth.X / 2, pitZoneSize.Y / 2),
                    new Vector2(hitBoxDepth.X / 2, pitZoneSize.Y / 2))); // right

        }

        public override void Start()
        {
            base.Start();

            CreateProceduralDetails();
        }

        private void CreateProceduralDetails()
        {
            var details = new List<GameObject>[detailsBoundaries.Count];
            for (int i = 0; i < detailsBoundaries.Count; i++)
                details[i] = new List<GameObject>();
            var attempts = 0;
            var detailsCount = 0;
            while (attempts < maxProceduralAttempts && detailsCount < detailsNumber)
            {
                attempts++;
                // pick random texture
                var textureInfo = Utils.ChooseRandom(detailsTextures);
                var textures = TexturePart.LoadSpriteSheet(
                    textureInfo.Item1, textureInfo.Item2, textureInfo.Item3);
                var width = textureInfo.Item4;
                var height = textures[0].Height/textures[0].Width*textureInfo.Item4;

                // pick random position
                int detailIndex = Utils.Random.Next(details.Length);
                var detailPosition = Utils.PickRandomPoint(detailsBoundaries[detailIndex]);
                detailPosition = detailPosition*Size + position;
                var detail = new GameObject(width, height) { position = detailPosition };
                detail.Hitboxes.Add(new Bounds(detail));

                // too much linq?
                // check if new detail collides with any other detail, if so repeat
                if (details[detailIndex].Any(gobj => Physics.CheckObjectsCollision(gobj, detail))) continue;

                // spawn detail..
                detail.States.Add(new StateManager(textureInfo.Item4) { Textures = textures });
                GameManager.Instance.AddObject($"{Name}_detail{detailsCount}", detail);
                details[detailIndex].Add(detail);
                detailsCount++;
            }
            foreach (var detailList in details)
                foreach (var detail in detailList)
                    detail.Hitboxes.Clear();
            Debug.WriteLine($"spawned n.{detailsCount} details in PitZone {Name}");
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