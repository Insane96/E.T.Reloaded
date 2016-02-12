using OpenTK;

namespace HdGame
{
    public class Game : GameObject
    {
        public Game()
        {
            GameManager.AssetPath = "../../assets";
            var texturePart = new TexturePart("weed.png");
            var testWeed = new GameObject(texturePart.Width, texturePart.Height);
            var idleState = new StateManager();
            idleState.Textures.Add(texturePart);
            testWeed.States.Add(idleState);

            GameManager.Instance.AddObject("testWeed", testWeed);
        }
    }
}