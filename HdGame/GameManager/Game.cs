using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class Game : GameObject
    {
        public Game()
        {
            GameManager.AssetPath = "../../assets";

            World = new World();

            var playerIdle = TexturePart.LoadSpriteSheet("et_old_idle.png", 5, 1);
            var playerMovingRight = TexturePart.LoadSpriteSheet("et_old_right.png", 5, 1);
            var aratio = playerIdle[0].Height / playerIdle[0].Width;
            // large 1meter
            Player = new Player(1f, 1f * aratio);
            Player.AddComponent(new FollowCamera());
            ((RigidBody) Player.GetComponent<RigidBody>()).Hitboxes.Add(Bounds.FromGameObject(Player));
            var mr = (StateRenderer) Player.GetComponent<StateRenderer>();
            mr.States.Add(new StateManager(5) { Textures = playerIdle }); // idle
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // left
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // right
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // down
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // up
            GameManager.Instance.AddObject("player", Player);
            // test hitbox
            var player2 = new Player(1f, 1f*aratio)
            {
                Controls = new Dictionary<KeyCode, Vector2>()
                {
                    {KeyCode.Up, new Vector2(0, -1)},
                    {KeyCode.Down, new Vector2(0, 1)},
                    {KeyCode.Left, new Vector2(-1, 0)},
                    {KeyCode.Right, new Vector2(1, 0)}
                },
            };
            player2.Transform.Position = new Vector2(5f, 0f);
            ((RigidBody) player2.GetComponent<RigidBody>()).Hitboxes.Add(Bounds.FromGameObject(player2));
            mr = (StateRenderer) player2.GetComponent<StateRenderer>();
            mr.States.Add(new StateManager(5) { Textures = playerIdle }); // idle
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // left
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // right
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // down
            mr.States.Add(new StateManager(5) { Textures = playerMovingRight }); // up
            GameManager.Instance.AddObject("player2", player2);
        }

        public Player Player { get; set; }

        public World World { get; }
    }
}