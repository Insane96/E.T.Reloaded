using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aiv.Fast2D;

namespace HdGame
{
    public class GameManager
    {
        private static GameManager instance;
        private int objectsCount;
        public static GameManager Instance => instance ?? (instance = new GameManager());

        private GameManager()
        {
            // 20 meters camera, so objects need to have dimensions similiar to reality
            Context.orthographicSize = 10f;
            Window = new Window(1920, 1080, "E.T. HD", false);
            Camera = new Camera(0, 0);

            Objects = new Dictionary<string, GameObject>();
            SortedObjects = new SortedSet<GameObject>(new GameObjectComparer());
        }

        public void AddObject(string name, GameObject gameObject)
        {
            Debug.Assert(!Objects.ContainsKey(name));
            gameObject.Name = name;
            gameObject.Id = objectsCount++;
            gameObject.OnOrderChange += sender =>
            {
                // may not be needed since we're using sorteddictionary? TODO: check this.
            };
            gameObject.OnDestroy += sender =>
            {
                Objects.Remove(gameObject.Name);
                SortedObjects.Remove(gameObject);
            };
            Objects[name] = gameObject;
            SortedObjects.Add(gameObject);
        }
        // "RemoveObject" is automatically called on object destroy.

        // dictionary added so we can get objects by key, could be removed if not needed
        public Dictionary<string, GameObject> Objects { get; }
        private SortedSet<GameObject> SortedObjects { get; }

        public Camera Camera { get; }

        public Window Window { get; }
        public static string AssetPath { get; set; }
        public float Time { get; private set; }
        public float DeltaTime { get; private set; }

        public void Run()
        {
            while (Window.opened)
            {
                Window.Update();
                DeltaTime = Window.deltaTime;
                Time += DeltaTime;
                // clone dictionary before looping, so it can be modified inside the loop
                foreach (var pair in SortedObjects.ToDictionary(key => key, value => value))
                {
                    var gameObject = pair.Value;
                    if (!gameObject.Started)
                    {
                        gameObject.Started = true;
                        gameObject.Start();
                    }
                    if (gameObject.Disposed) continue;
                    gameObject.Update();
                }
                Physics.UpdateCollisions();
            }
        }
    }
}