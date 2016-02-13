using System;
using System.Collections.Generic;
using Aiv.Fast2D;

namespace HdGame
{
    public class GameObject : Sprite
    {
        public delegate void OrderChangeEventHandler(object sender);
        public event OrderChangeEventHandler OnOrderChange;
        public delegate void DestroyEventHandler(object sender);
        public event DestroyEventHandler OnDestroy;

        public List<Bounds> Hitboxes { get; private set; }
        public List<Collision> Collisions { get; private set; } 

        public bool Disposed { get; private set; }
        private int order;

        public GameObject(float width, float height) : base(width, height)
        {
            Init();
        }

        // should create another class that doesn't inherit sprite...
        // (ignore if using only 1-2 gameobjects that doesn't draw anything in whole game?)
        public GameObject() : base(1, 1)
        {
            Init();
        }

        private void Init()
        {
            States = new List<StateManager>();
            Hitboxes = new List<Bounds>();
            Collisions = new List<Collision>();
        }

        public void Destroy()
        {
            if (!Disposed) { 
                Disposed = true;
                OnDestroy?.Invoke(this);
                Dispose();
            }
        }

        public new virtual void Update()
        {
            base.Update();
            CalculateTexture();
            CurrentTexture?.Draw(this);
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

        // default to 0, so putting Idle state on States[0] is a nice idea  
        public int CurrentState { get; set; }

        public List<StateManager> States { get; private set; }

        public TexturePart CurrentTexture { get; private set; }

        public int Order
        {
            get { return order; }
            set
            {
                order = value;
                OnOrderChange?.Invoke(this);
            }
        }

        // setted by gamemanager, used to compare two objects with same order
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual void OnCollision(Collision collision)
        {
        }
    }

    public class GameObjectComparer : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            var result = y.Order.CompareTo(x.Order);
            if (result == 0)
                result = y.Id.CompareTo(x.Id);
            return -1 * result;
        }
    }
}