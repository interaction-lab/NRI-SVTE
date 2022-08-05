using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TheKiwiCoder;

namespace NRISVTE {
    public class SuccessOnEvent : MonitorCondition {
        KuriBTEventRouter _eventRouter;
        KuriBTEventRouter eventRouter {
            get {
                if (_eventRouter == null) {
                    _eventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _eventRouter;
            }
        }
        public string eventName;
        UnityEvent evt;

        public bool eventHappened = false;

        protected override void OnStart() {
            evt = eventRouter.GetEvent(eventName);
            evt.AddListener(OnEvent);
            eventHappened = false;
        }

        protected override void OnStop() {
            evt.RemoveListener(OnEvent);
        }

        protected virtual void OnEvent(){
            eventHappened = true;
        }

        protected override State OnUpdate() {
            return eventHappened ? State.Success : State.Failure;
        }
    }
}
