using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree.Nodes {
    [System.Serializable]
    public class BoolConditionNode : ConditionNode {
        public bool condition;
        float elpasedTime;
        #region Overrides of Node
        protected override void OnStart() {
            condition = true;
        }
        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            elpasedTime += Time.deltaTime;
            if (elpasedTime > 4) {
                elpasedTime = 0;
                condition = !condition;
            }
            return condition ? State.Success : State.Failure;
        }

        #endregion
    }
}