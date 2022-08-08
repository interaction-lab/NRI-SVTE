using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class StopButtonManager : Singleton<StopButtonManager> {
        #region members
        // get kuristatemanager
        KuriStateManager _kuriStateManager;
        KuriStateManager KuriState {
            get {
                if (_kuriStateManager == null) {
                    _kuriStateManager = KuriStateManager.instance;
                }
                return _kuriStateManager;
            }
        }
        Button _stopButton = null;
        public Button StopButton {
            get {
                if (_stopButton == null) {
                    _stopButton = GetComponent<Button>();
                }
                return _stopButton;
            }
        }

        KuriBTEventRouter _kuriBTEventRouter;
        KuriBTEventRouter kuriBTEventRouter {
            get {
                if (_kuriBTEventRouter == null) {
                    _kuriBTEventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _kuriBTEventRouter;
            }
        }
        bool IsPressed = false;

        #endregion

        #region unity
        private void Awake() {
            kuriBTEventRouter.AddEvent(EventNames.StopButtonPressed, StopButton.onClick);
        }
        void Start() {
            KuriState.OnStateChanged.AddListener(OnStateChanged);
            StopButton.onClick.AddListener(OnStopButtonPressed);
        }
        #endregion

        #region public
        #endregion

        #region private
        void OnStateChanged() {
            if (KuriState.Rstate == KuriStateManager.States.Moving) {
                //StopButton.interactable = true;
            }
            else {
                // StopButton.interactable = false;
            }
        }
        void OnStopButtonPressed() {
            if (KuriState.Rstate == KuriStateManager.States.Moving) {
                KuriState.SetState(KuriStateManager.States.StoppedByButton);
            }
            else {
                KuriState.SetState(KuriStateManager.States.Idle);
            }
        }
        #endregion
    }
}
