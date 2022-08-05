using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NRISVTE {
    public class StartButtonManager : MonoBehaviour {
        KuriBTEventRouter _kuriBTEventRouter;
        KuriBTEventRouter kuriBTEventRouter {
            get {
                if (_kuriBTEventRouter == null) {
                    _kuriBTEventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _kuriBTEventRouter;
            }
        }

        Button _startButton;
        Button startButton {
            get {
                if (_startButton == null) {
                    _startButton = GetComponent<Button>();
                }
                return _startButton;
            }
        }
        private void Awake() {
            kuriBTEventRouter.AddEvent(EventNames.StartButtonPressed, startButton.onClick);
        }
    }
}
