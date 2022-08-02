using System.Collections.Generic;
using System.Linq;
using GraphViewBehaviorTree.Nodes;
using UnityEditor;
using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree {
    public class Blackboard {
        public Dictionary<string, object> blackboard;
        public Blackboard(GameObject go) {
            blackboard = new Dictionary<string, object>();
            blackboard.Add(GameObjectBBK, go);
            blackboard.Add(TransformBBK, go.transform);
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

        /// <summary>
        /// Returns the value of the key if it exists, otherwise returns the default value.
        /// Makes trying for keys to override easier/cleaner
        /// </summary>
        public object TryG(string key, object o) {
            if (blackboard.ContainsKey(key)) {
                return blackboard[key];
            }
            else {
                return o;
            }
        }
    }
}