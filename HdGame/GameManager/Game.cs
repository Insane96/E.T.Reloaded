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
            var aratio = playerIdle[0].Width/playerIdle[0].Height;
            // large 1meter
            Player = new Player(1f, 1f*aratio);
            Player.States.Add(new StateManager(5) { Textures = playerIdle }); // idle
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // left
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // right
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // down
            Player.States.Add(new StateManager(5) { Textures = playerMovingRight }); // up
            GameManager.Instance.AddObject("player", Player);
        }

        public Player Player { get; set; }

        public World World { get; }
    }
}