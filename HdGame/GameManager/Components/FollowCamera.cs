using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace HdGame
{
    class FollowCamera : Component
    {
        public float Delay { get; set; } = 0.25f;

        public override void Update()
        {
            base.Update();
            GameManager.Instance.Camera.position = Vector2.Lerp(
                GameManager.Instance.Camera.position, GameObject.Transform.Position,
                GameManager.Instance.DeltaTime * (1/Delay));
        }
    }
}
