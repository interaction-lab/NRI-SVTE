using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class TriggerObjectDialogue : ActionNode {
        ObjectToPickUpManager _objectToPickUpManager;
        ObjectToPickUpManager objectToPickUpManager {
            get {
                if (_objectToPickUpManager == null) {
                    _objectToPickUpManager = ObjectToPickUpManager.instance;
                }
                return _objectToPickUpManager;
            }
        }

        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            return objectToPickUpManager.TriggerObjectDialogue() ? State.Success : State.Running;
        }
    }
}
