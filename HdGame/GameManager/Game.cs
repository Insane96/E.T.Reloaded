using OpenTK;

namespace HdGame
{
    public class Game : GameObject
    {
        public Game()
        {
            GameManager.AssetPath = "../../assets";
            var wholeTexture = new TexturePart("weed.png");
            var texturePart0 = new TexturePart("weed.png", 0, 0, wholeTexture.Width / 3, wholeTexture.Height / 2);
            var texturePart1 = new TexturePart("weed.png", texturePart0.Width, 0, texturePart0.Width, texturePart0.Height);
            var testWeed = new GameObject(texturePart0.Width, texturePart0.Height);
            var idleState = new StateManager();
            idleState.Textures.Add(texturePart0);
            idleState.Textures.Add(texturePart1);
            testWeed.States.Add(idleState);

            GameManager.Instance.AddObject("testWeed", testWeed);
        }
    }
}