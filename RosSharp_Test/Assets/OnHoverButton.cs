using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace NRISVTE {
    public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public UnityEvent OnHoverEnter, OnHoverExit;
        private void Awake() {
            OnHoverEnter = new UnityEvent();
            OnHoverExit = new UnityEvent();
        }
        public void OnPointerEnter(PointerEventData eventData) {
            OnHoverEnter.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData) {
            OnHoverExit.Invoke();
        }
    }
}
