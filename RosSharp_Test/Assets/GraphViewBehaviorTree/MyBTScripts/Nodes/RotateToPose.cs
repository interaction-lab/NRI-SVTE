using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree.Nodes {
    [System.Serializable]
    public class RotateToPose : ActionNode {
        public Vector3 targetRotation;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private float rotationThreshold = 0.1f;
        [SerializeField] private float timeOut = 0;
        [SerializeField] private float timeOutTimer = 0;
        private Transform myTransform;


        #region Overrides of Node
        protected override void OnStart() {
            timeOutTimer = timeOut;
            myTransform = blackBoard.GetValue(TransformBBK) as Transform;
        }
        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            if (timeOutTimer > 0) {
                timeOutTimer -= Time.deltaTime;
                if (timeOutTimer <= 0) {
                    return State.Failure;
                }
            }
            if (Vector3.Distance(myTransform.eulerAngles, targetRotation) < rotationThreshold) {
                // move to exact rotation
                myTransform.eulerAngles = targetRotation;
                return State.Success;
            }
            // move toward target rotation
            myTransform.eulerAngles = Vector3.MoveTowards(myTransform.eulerAngles, targetRotation, rotationSpeed * Time.deltaTime);

            return State.Running;
        }

        #endregion
    }
}