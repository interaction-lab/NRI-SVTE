using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree.Nodes {
    [System.Serializable]
    public class BoolConditionNode : ConditionNode {
        public bool condition;
        #region Overrides of Node
        protected override void OnStart() {
        }
        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            return condition ? State.Success : State.Failure;
        }

        #endregion
    }
}