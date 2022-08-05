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

        float timeEventHappened = -1f, lastFrameTime = -1f;
        bool processedEvent = true;

        protected override void OnStart() {
            if (evt == null) {
                evt = eventRouter.GetEvent(eventName);
                evt.AddListener(OnEvent);
            }
        }

        protected override void OnStop() {
        }

        protected virtual void OnEvent() {
            timeEventHappened = Time.time;
            processedEvent = false;
        }

        protected override State OnUpdate() {
            bool ret = false;
            if (timeEventHappened == Time.time || // happened this frame
               (timeEventHappened == lastFrameTime && !processedEvent)) { // happened last frame and was not processed
                processedEvent = true;
                ret = true;
            }
            lastFrameTime = Time.time;
            return ret ? State.Success : State.Failure;
        }
    }
}
