using System;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    internal class UnityMainThread : MonoBehaviour {
        internal static UnityMainThread wkr;
        Queue<Action> jobs = new Queue<Action>();
        public UnityMainThread() {
            if (UnityMainThread.wkr == null) {
                UnityMainThread.wkr = this;
            }
        }

        private void Awake() {
            if (UnityMainThread.wkr == null) {
                UnityMainThread.wkr = this;
            }
        }

        void Update() {
            while (jobs.Count > 0)
                jobs.Dequeue().Invoke();
        }

        internal void AddJob(Action newJob) {
            jobs.Enqueue(newJob);
        }
    }
}