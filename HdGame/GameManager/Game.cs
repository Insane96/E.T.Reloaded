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
            Player.Hitboxes.Add(new Bounds(Player));
            Player.States.Add(new StateManager(5) { Textures = playerIdle }); // idle
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // left
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // right
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // down
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // up
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
                position = new Vector2(5f, 0f)
            };
            player2.Hitboxes.Add(new Bounds(player2));
            player2.States.Add(new StateManager(5) { Textures = playerIdle }); // idle
            player2.States.Add(new StateManager(5) { Textures = playerMovingRight }); // left
            player2.States.Add(new StateManager(5) { Textures = playerMovingRight }); // right
            player2.States.Add(new StateManager(5) { Textures = playerMovingRight }); // down
            player2.States.Add(new StateManager(5) { Textures = playerMovingRight }); // up
            GameManager.Instance.AddObject("player2", player2);
        }

        public Player Player { get; set; }

        public World World { get; }
    }
}