using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree.Nodes {
    [System.Serializable]
    public abstract class ConditionNode : ActionNode {

        public virtual bool IsTrue() {
            return Update() == State.Failure;
        }
    }
}