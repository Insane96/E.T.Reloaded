using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class Player : Character
    {
        public float Score { get; private set; }
        private static bool firstPlayer;
        private bool followCamera;

        public Dictionary<KeyCode, Vector2> Controls = new Dictionary<KeyCode, Vector2>()
        {
            { KeyCode.W, new Vector2(0, -1) },
            { KeyCode.S, new Vector2(0, 1) },
            { KeyCode.A, new Vector2(-1, 0) },
            { KeyCode.D, new Vector2(1, 0) }
        };

        public Player(float width, float height) : base(width, height)
        {
            Score = 0;
            Speed = 2.33f;
            if (!firstPlayer)
            {
                firstPlayer = true;
                followCamera = true;
            }
        }

        public override void Update()
        {
            // need to be called BEFORE Character.Update
            ManageControls();
            base.Update();
            if (followCamera)
                UpdateCamera();
        }

        private void UpdateCamera()
        {
            GameManager.Instance.Camera.position = Vector2.Lerp(
                GameManager.Instance.Camera.position, position,
                GameManager.Instance.DeltaTime);
        }

        private void ManageControls()
        {
            // arcade style reset, try to remove to have "force"-like behaviour (decrease speed b4!)
            MovingVector = Vector2.Zero;
            foreach (var pair in Controls)
            {
                if (GameManager.Instance.Window.GetKey(pair.Key))
                    MovingVector += pair.Value;
            }
        }
    }
}