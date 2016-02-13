using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class Player : Character
    {
        public float Score { get; private set; }

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
            Speed = 1.33f;
        }

        public override void Update()
        {
            // need to be called BEFORE Character.Update
            ManageControls();
            base.Update();
        }

        private void ManageControls()
        {
            // arcade style reset, try to remove to have "force"-like behaviour (decrease speed b4!)
            MovingVector = Vector2.Zero;
            var w = GameManager.Instance.Window;
            foreach (var pair in Controls)
            {
                if (w.GetKey(pair.Key))
                    MovingVector += pair.Value;
            }
        }
    }
}