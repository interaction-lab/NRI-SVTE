using System.Collections.Generic;
using System.Linq;
using GraphViewBehaviorTree.Nodes;
using UnityEditor;
using UnityEngine;

namespace GraphViewBehaviorTree {
    public class Blackboard {
        public Dictionary<string, object> blackboard;
        public Blackboard(GameObject go) {
            blackboard = new Dictionary<string, object>();
            blackboard.Add("gameObject", go);
        }

        public void SetValue(string key, object value) {
            if (blackboard.ContainsKey(key)) {
                blackboard[key] = value;
            }
            else {
                blackboard.Add(key, value);
            }
        }

        public object GetValue(string key) {
            if (blackboard.ContainsKey(key)) {
                return blackboard[key];
            }
            else {
                return null;
            }
        }

        public bool HasValue(string key) {
            return blackboard.ContainsKey(key);
        }

    }
}