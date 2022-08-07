using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class TurnToPose : ActionNode {
        float turnSpeed = 30f; // degrees per second
        float goalYRot;
        float minAngle;
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
            goalYRot = blackboard.goalRotation.y;
            // get goal roation between 0 and 360 degrees
            if (goalYRot < 0) {
                goalYRot += 360;
            }
            if (goalYRot > 360) {
                goalYRot -= 360;
            }
            minAngle = turnSpeed * Time.deltaTime * 2f;
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            float kuriRot = KuriT.Rotation.eulerAngles.y;
            // get kuri rotation between 0 and 360 degrees
            if (kuriRot < 0) {
                kuriRot += 360;
            }
            if (kuriRot > 360) {
                kuriRot -= 360;
            }
            KuriT.Rotation = Quaternion.Euler(KuriT.Rotation.eulerAngles.x, kuriRot, KuriT.Rotation.eulerAngles.z);
            float delta = (goalYRot - KuriT.Rotation.eulerAngles.y);
            // delta range is 0 to 360
            // clamp delta to -180 to 180 degrees
            Debug.Log("b4: " + delta.ToString());
            if (delta > 180) {
                delta -= 360;
            }
            if (delta < -180) {
                delta += 360;
            }
            Debug.Log(delta);
            if (Mathf.Abs(delta) <= minAngle) {
                KuriT.Rotation = Quaternion.Euler(new Vector3(KuriT.Rotation.eulerAngles.x, goalYRot, KuriT.Rotation.eulerAngles.z));
                return State.Success;
            }

            float timedSpeed = turnSpeed * Time.deltaTime;
            timedSpeed = Mathf.Min(timedSpeed, delta);

            if (delta > 0f) {
                KuriT.Rotation = Quaternion.Euler(new Vector3(KuriT.Rotation.eulerAngles.x, KuriT.Rotation.eulerAngles.y + timedSpeed, KuriT.Rotation.eulerAngles.z));
            }
            else {
                KuriT.Rotation = Quaternion.Euler(new Vector3(KuriT.Rotation.eulerAngles.x, KuriT.Rotation.eulerAngles.y - timedSpeed, KuriT.Rotation.eulerAngles.z));
            }

            return State.Running;
        }
    }
}
