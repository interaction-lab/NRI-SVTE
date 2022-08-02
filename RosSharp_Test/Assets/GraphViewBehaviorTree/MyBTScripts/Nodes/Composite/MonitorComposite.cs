using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GraphViewBehaviorTree.Nodes {
    public class MonitorComposite : CompositeNode {
        List<State> childrenLeftToExecute = new List<State>();
        Dictionary<ConditionNode, int> childrenToMonitor = new Dictionary<ConditionNode, int>();

        #region Overrides of Node
        protected override void OnStart() {
            childrenLeftToExecute.Clear();
            children.ForEach(a => {
                childrenLeftToExecute.Add(State.Running);
            });
            SetUpMonitoringChildren();
        }
        protected override void OnStop() { }

        protected override State OnUpdate() {
            bool stillRunning = false;
            for (int i = 0; i < childrenLeftToExecute.Count; ++i) {
                if (childrenLeftToExecute[i] == State.Running) {
                    var status = children[i].Update();
                    if (status == State.Failure) {
                        AbortRunningChildren();
                        return State.Failure;
                    }

                    if (status == State.Running) {
                        stillRunning = true;
                    }

                    childrenLeftToExecute[i] = status;
                }
            }

            if (stillRunning) {
                ResetAllMonitoringChildren();
            }

            return stillRunning ? State.Running : State.Success;
        }

        void AbortRunningChildren() {
            for (int i = 0; i < childrenLeftToExecute.Count; ++i) {
                if (childrenLeftToExecute[i] == State.Running) {
                    children[i].Abort();
                }
            }
        }

        void ResetAllMonitoringChildren() {
            foreach (var child in childrenToMonitor.Keys) {
                child.NodeReset();
                childrenLeftToExecute[childrenToMonitor[child]] = State.Running;
            }
        }

        private void SetUpMonitoringChildren() {
            for (int i = 0; i < children.Count; ++i) {
                if (children[i] is ConditionNode) {
                    childrenToMonitor.Add((ConditionNode)children[i], i);
                }
            }
        }


        #endregion
    }
}