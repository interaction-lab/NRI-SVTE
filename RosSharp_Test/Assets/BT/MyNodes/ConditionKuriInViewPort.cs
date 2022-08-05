using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace NRISVTE {
    public class ConditionKuriInViewPort : MonitorCondition {
        UIArrow _arrow;
        UIArrow viewportArrow {
            get {
                if (_arrow == null) {
                    _arrow = UIArrow.instance;
                }
                return _arrow;
            }
        }
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            return viewportArrow.IsInViewPort ? State.Success : State.Failure;
        }
    }
}
