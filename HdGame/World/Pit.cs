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
        public Vector2 PitZoneSize { get; set; }
        private Vector2 pitZonePosition;
        private PitZone pitZone;

        public Pit(float width, float height)
        {
            AddComponent(new MeshRenderer(width, height));
            AddComponent(new RigidBody());

            pitCount++;
        }

        public override void Start()
        {
            base.Start();
            ((RigidBody) GetComponent<RigidBody>()).Hitboxes.Add(Bounds.FromGameObject(this));

            PitZoneSize = new Vector2(
                GameManager.Instance.Window.aspectRatio * pitZoneHeight, pitZoneHeight);
            pitZonePosition = World.Boundings.Max + new Vector2(PitZoneSize.X*3, 0f) * pitCount;
        }

        private void CreatePitZone()
        {
            pitZone = new PitZone(this, pitZonePosition, PitZoneSize);
            GameManager.Instance.AddObject($"{Name}_pitZone", pitZone);
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
            if (((TimerManager) GetComponent<TimerManager>()).Get("pitFallDelay") > 0) return;
            if (pitZone == null)
            {
                CreatePitZone();
            }
            pitZone.LastPlayerPosition = player.Transform.LastPosition;
            player.Transform.Position = pitZonePosition + 
                new Vector2(
                    PitZoneSize.X/2, 
                    PitZoneSize.Y - ((MeshRenderer) player.GetComponent<MeshRenderer>()).Height);
            player.Transform.LastPosition = player.Transform.Position;
            GameManager.Instance.Camera.position = player.Transform.Position;
        }

        public override GameObject Clone()
        {
            //var cl = (Pit) base.Clone();
            //return cl;
            var mr = (MeshRenderer)GetComponent<MeshRenderer>();
            var clone = new Pit(mr.Width, mr.Height);
            foreach (var component in Components)
            {
                if (!(component is Transform) && !(component is TimerManager))
                    clone.AddComponent(component.Clone());
            }
            return clone;
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

        public PitZone(Pit pit, Vector2 pitZonePosition, Vector2 pitZoneSize) 
        {
            AddComponent(new MeshRenderer(pitZoneSize.X, pitZoneSize.Y));

            this.pit = pit;

            Transform.Position = pitZonePosition;
            var backgroundTexture = new TexturePart("box.png");
            ((MeshRenderer) GetComponent<MeshRenderer>()).CurrentTexture = backgroundTexture;

            var rigidBody = (RigidBody) AddComponent(new RigidBody());
            // hitboxes
            var hitBoxDepth = new Vector2(6f, 2f);
            rigidBody.Hitboxes.Add(
                new Bounds(
                    new Vector2(pitZoneSize.X / 2, pitZoneSize.Y + hitBoxDepth.Y / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2))); // bottom
            rigidBody.Hitboxes.Add(
                new Bounds(
                    "exit", 
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2),
                    new Vector2(pitZoneSize.X / 2, hitBoxDepth.Y / 2))); // top
            rigidBody.Hitboxes.Add(
                new Bounds(
                    new Vector2(hitBoxDepth.X / 2, pitZoneSize.Y / 2),
                    new Vector2(hitBoxDepth.X / 2, pitZoneSize.Y / 2))); // left
            rigidBody.Hitboxes.Add(
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
            var hitBoxes = new Dictionary<GameObject, Bounds>();
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
                var detail = new GameObject();
                var detailRenderer = (StateRenderer) detail.AddComponent(new StateRenderer(width, height));
                hitBoxes[detail] = Bounds.FromGameObject(detail);

                var detailPosition = Utils.PickRandomPoint(detailsBoundaries[detailIndex]);
                detailPosition = detailPosition * pit.PitZoneSize + Transform.Position; // * detailRenderer.Size
                detail.Transform.Position = detailPosition;

                // too much linq?
                // check if new detail collides with any other detail, if so repeat
                if (details[detailIndex].Any(gobj => Physics.CheckBoundsCollision(hitBoxes[gobj], hitBoxes[detail]))) continue;

                // spawn detail..
                detailRenderer.States.Add(new StateManager(textureInfo.Item4) { Textures = textures });
                GameManager.Instance.AddObject($"{Name}_detail{detailsCount}", detail);
                details[detailIndex].Add(detail);
                detailsCount++;
            }
            Debug.WriteLine($"spawned n.{detailsCount} details in PitZone {Name}");
        }

        public override void OnCollision(Collision collision)
        {
            base.OnCollision(collision);
            var player = collision.GameObject as Player;
            if (collision.OwnerBounds.Name == "exit" && player != null)
            {
                player.Transform.Position = LastPlayerPosition;
                player.Transform.LastPosition = player.Transform.Position;
                GameManager.Instance.Camera.position = player.Transform.Position;
                ((TimerManager)pit.GetComponent<TimerManager>()).Set("pitFallDelay", 2f);
            }
        }
    }
}