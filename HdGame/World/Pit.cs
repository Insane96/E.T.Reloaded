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
        public bool HasFlower { get; set; }
        public bool HasPhone { get; set; }

        private Vector2 pitZonePosition;
        private PitZone pitZone;

        public Pit(float width, float height)
        {
            AddComponent(new MeshRenderer(width, height));
            AddComponent(new RigidBody());
        }

        public override void Start()
        {
            base.Start();
            ((RigidBody) GetComponent<RigidBody>()).Hitboxes.Add(Bounds.FromGameObject(this));

            PitZoneSize = new Vector2(
                GameManager.Instance.Window.aspectRatio * pitZoneHeight, pitZoneHeight);
            pitZonePosition = World.Boundings.Max + PitZoneSize * 2 * pitCount++;
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
            LastPlayerPosition = player.Transform.LastPosition;
            Debug.WriteLine("Fall " + Name + " " + LastPlayerPosition + " " + pitZonePosition);
            ((RigidBody) pitZone.GetComponent<RigidBody>()).Enabled = true;
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
        public Vector2 LastPlayerPosition { get; set; }
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

        public static float CandySpawnRate = 1f;

        public PitZone(Pit pit, Vector2 pitZonePosition, Vector2 pitZoneSize)
        {
            Size = pitZoneSize;
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

        public Vector2 Size { get; set; }

        public override void Start()
        {
            base.Start();

            CreateProceduralDetails();

            // spawn flower
            if (pit.HasFlower)
            {

            }
            // randomly spawn candy
            if (Utils.Random.NextDouble() < CandySpawnRate)
            {
                var candy = new Candy();
                candy.Transform.Position = Transform.Position + new Vector2(Size.X * 0.25f, Size.Y - 1.33f);
                GameManager.Instance.AddObject($"{Name}_candy", candy);
            }
            if (pit.HasPhone)
            {
                var phone = new Phone();
                phone.Transform.Position = Transform.Position + new Vector2(Size.X * 0.35f, Size.Y - 1.33f);
                GameManager.Instance.AddObject($"{Name}_phone", phone);
            }
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
                Debug.WriteLine("Back " + pit.Name + " " + pit.LastPlayerPosition);
                player.Transform.Position = pit.LastPlayerPosition;
                player.Transform.LastPosition = player.Transform.Position;
                GameManager.Instance.Camera.position = player.Transform.Position;
                ((TimerManager)pit.GetComponent<TimerManager>()).Set("pitFallDelay", 2f);
                ((RigidBody) GetComponent<RigidBody>()).Enabled = false;
            }
        }
    }
}