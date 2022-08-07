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
            Vector3 dirInWorld = (blackboard.goalPosition - KuriT.Position).normalized;
            // goal roation is relative to kuri forward direction
            Quaternion goalRotation = Quaternion.LookRotation(dirInWorld, KuriT.Forward);
            blackboard.goalRotation = goalRotation.eulerAngles;
            return State.Success;
        }
    }
}
