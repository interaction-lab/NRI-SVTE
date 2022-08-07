using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class TurnToPose : ActionNode {
        public float turnSpeed = 30f; // degrees per second
        Quaternion goalRotation;
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
            goalRotation = Quaternion.Euler(blackboard.goalRotation);
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            KuriT.Rotation = Quaternion.RotateTowards(KuriT.Rotation, goalRotation, turnSpeed * Time.deltaTime);
            return KuriT.Rotation == goalRotation ? State.Success : State.Running;
        }
    }
}
