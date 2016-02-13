﻿using System;
using System.Collections.Generic;

namespace HdGame
{
    public class TimerManager
    {
        private readonly Dictionary<string, Tuple<Action<GameObject, object[]>, object[]>> callBacks;

        private readonly GameObject owner;
        private readonly Dictionary<string, float> timers;

        private TimerManager()
        {
            timers = new Dictionary<string, float>();
            callBacks = new Dictionary<string, Tuple<Action<GameObject, object[]>, object[]>>();
        }

        public TimerManager(GameObject owner) : this()
        {
            this.owner = owner;
        }

        public float Get(string key)
        {
            float result;
            if (!timers.TryGetValue(key, out result))
                return 0f; //float.MaxValue; // default value
            return result;
        }

        public void Set(
            string key, float value, Action<GameObject, object[]> callback = null,
            object[] extraArgs = null)
        {
            timers[key] = value;
            if (callback != null)
                callBacks[key] = Tuple.Create(callback, extraArgs);
            else if (callBacks.ContainsKey(key) && callBacks[key] != null)
                callBacks[key] = Tuple.Create((Action<GameObject, object[]>)null, (object[])null);
        }

        public bool Contains(string key)
        {
            return timers.ContainsKey(key);
        }

        public void Update()
        {
            var keys = new string[timers.Count];
            timers.Keys.CopyTo(keys, 0);
            foreach (var key in keys)
            {
                if (timers[key] > 0f)
                {
                    timers[key] -= GameManager.Instance.DeltaTime;
                    if (timers[key] <= 0 && callBacks.ContainsKey(key))
                        callBacks[key].Item1(owner, callBacks[key].Item2);
                }
            }
        }
    }
}