using UnityEngine;
using static GraphViewBehaviorTree.BlackBoardConstants;

namespace GraphViewBehaviorTree.Nodes {
    [System.Serializable]
    public class MoveToPose : ActionNode {
        public Vector3 targetPosition;
        public Vector3 targetRotation;
        [SerializeField] private float speed = 1;
        [SerializeField] private float distanceThreshold = 0.1f;
        [SerializeField] private float timeOut = 0;
        [SerializeField] private float timeOutTimer = 0;
        private Transform myTransform;


        #region Overrides of Node
        protected override void OnStart() {
            timeOutTimer = timeOut;
            myTransform = (blackBoard.GetValue(GameObjectBBK) as GameObject).transform;
            targetPosition = (Vector3)blackBoard.TryG(GoalPosBBK, targetPosition);
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
            if (Vector3.Distance(myTransform.position, targetPosition) < distanceThreshold) {
                // move to exact position and rotation
                myTransform.position = targetPosition;
                myTransform.eulerAngles = targetRotation;
                return State.Success;
            }
            // move to target position and rotation
            myTransform.position = Vector3.MoveTowards(myTransform.position, targetPosition, speed * Time.deltaTime);
            myTransform.eulerAngles = targetRotation;

            return State.Running;
        }

        #endregion
    }
}