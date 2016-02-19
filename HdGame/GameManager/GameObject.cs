using System;
using System.Collections.Generic;
using System.Linq;
using Aiv.Fast2D;
using OpenTK;

namespace HdGame
{
    public class GameObject
    {
        public delegate void OrderChangeEventHandler(object sender);
        public event OrderChangeEventHandler OnOrderChange;
        public delegate void DestroyEventHandler(object sender);
        public event DestroyEventHandler OnDestroy;

        public List<Collision> Collisions { get; private set; }

        public Transform Transform { get; }

        protected List<Component> Components { get; }

        public bool Disposed { get; private set; }
        private int order;

        public GameObject()
        {
            Collisions = new List<Collision>();
            Components = new List<Component>();

            Transform = (Transform) AddComponent(new Transform());
            AddComponent(new TimerManager());
        }

        public Component GetComponent<T>()
        {
            foreach (var component in Components)
                if (component is T)
                    return component;
            return null;
        }

        public void Destroy()
        {
            if (!Disposed) { 
                Disposed = true;
                OnDestroy?.Invoke(this);
            }
        }

        public virtual void OnCollision(Collision collision)
        {
            foreach (var component in Components)
            {
                var behaviour = component as Behaviour;
                if (behaviour != null && !behaviour.Enabled) continue;
                component.OnCollision(collision);
            }
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            foreach (var component in Components)
            {
                // lazy vs method?
                if (!component.Started)
                {
                    component.Started = true;
                    component.GameObject = this;
                    component.Start();
                }
                var behaviour = component as Behaviour;
                if (behaviour != null && !behaviour.Enabled) continue;
                component.Update();
            }
        }


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
        public bool Started { get; set; }

        public virtual GameObject Clone()
        {
            var clone = new GameObject
            {
                Order = Order,
            };
            foreach (var component in Components)
            {
                if (!(component is Transform) && !(component is TimerManager))
                    clone.AddComponent(component.Clone());
            }
            return clone;
        }

        public Component AddComponent(Component component)
        {
            Components.Add(component);
            component.GameObject = this;
            return component;
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