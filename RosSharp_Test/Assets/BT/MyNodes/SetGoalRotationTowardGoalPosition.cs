using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class SetGoalRotationTowardGoalPosition : ActionNode {
        KuriTransformManager kuriTransformManager;
        KuriTransformManager KuriT {
            get {
                if (kuriTransformManager == null) {
                    kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriTransformManager;
            }
        }
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            
            Vector2 goalPosition = new Vector2(blackboard.goalPosition.x, blackboard.goalPosition.z);
            Vector2 kuriPosition = new Vector2(KuriT.Position.x, KuriT.Position.z);
            Vector2 direction = goalPosition - kuriPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            blackboard.goalRotation = new Vector3(0, angle, 0);
            return State.Success;
        }
    }
}
