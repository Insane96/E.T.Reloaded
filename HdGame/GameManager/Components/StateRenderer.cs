using System.Collections.Generic;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class StateRenderer : MeshRenderer
    {
        public StateRenderer(float width, float height) : base(width, height)
        {
            States = new List<StateManager>();
        }

        // default to 0, so putting Idle state on States[0] is a nice idea  
        public int CurrentState { get; set; }

        public List<StateManager> States { get; private set; }

        public override void Update()
        {
            CalculateTexture();
            base.Update();
        }

        private void CalculateTexture()
        {
            if (States.Count <= CurrentState)
            {
                CurrentTexture = null;
                return;
            }
            var state = States[CurrentState];
            // lets skip all the calculations if there is a single texturepart in the statemanager
            if (state.Textures.Count == 1)
            {
                CurrentTexture = state.CurrentTexture;
                return;
            }

            if (state.Playing && GameManager.Instance.Time - state.LastTime >= state.Frequency)
            {
                state.LastTime = GameManager.Instance.Time;
                state.CurrentFrame++;
                if (state.CurrentFrame > state.Textures.Count - 1)
                {
                    if (state.Loop)
                    {
                        state.CurrentFrame = 0;
                    }
                    else // lock to last
                    {
                        state.CurrentFrame = state.Textures.Count - 1;
                    }
                }
            }
            CurrentTexture = state.CurrentTexture;
        }

        public override void Start()
        {
        }

        public override Component Clone()
        {
            var mr = new StateRenderer(Width, Height) {CurrentState = CurrentState};
            foreach (var state in States)
            {
                mr.States.Add(state.Clone());
            }
            return mr;
        }
    }
}