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
            Score = 9999;
            ((CharacterMovement) GetComponent<CharacterMovement>()).Speed = 2.33f;
            //((RigidBody) GetComponent<RigidBody>()).UseGravity = true;

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
            var cm = ((CharacterMovement) GetComponent<CharacterMovement>());
            cm.MovingVector = Vector2.Zero;
            foreach (var pair in Controls)
            {
                if (GameManager.Instance.Window.GetKey(pair.Key))
                    cm.MovingVector += pair.Value;
            }
            // todo: tune
            Score -= cm.MovingVector.Normalized().Length * GameManager.Instance.DeltaTime;
        }

        public void PickCandy(Candy candy)
        {
            Candies++;
            candy.Destroy();
        }

        public int Candies { get; private set; }

        public void PickPhone(Phone phone)
        {
            if (Phones < 3) { 
                Phones++;
                phone.Destroy();
            }
        }

        public int Phones { get; set; }
    }
}