using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TheKiwiCoder;

namespace NRISVTE {
    public class KuriBTEventRouter : MonoBehaviour {
        Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

        public void AddEvent(string eventName, UnityEvent evt) {
            if (!events.ContainsKey(eventName)) {
                events.Add(eventName, evt);
            }
            else {
                events[eventName] = evt;
            }
        }

        public UnityEvent GetEvent(string eventName) {
            if (events.ContainsKey(eventName)) {
                return events[eventName];
            }
            Debug.LogError("Event key" + eventName + " not found");
            return null;
        }
    }
}