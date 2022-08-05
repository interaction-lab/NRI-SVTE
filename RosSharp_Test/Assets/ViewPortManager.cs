using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace NRISVTE {
    public class ViewPortManager : Singleton<ViewPortManager> {
        #region members
        // targets
        public Vector3 kuriWorldTarget, screenPos, kuriRelativeToCamera, cappedScreenPos, viewPortPos;
        Camera _mainCam;
        Camera MainCam {
            get {
                if (_mainCam == null) {
                    _mainCam = Camera.main;
                }
                return _mainCam;
            }
        }
        KuriTransformManager _kuriTransformManager;
        KuriTransformManager kuriTransformManager {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = FindObjectOfType<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }
        UnityEvent KuriEnterViewPort = new UnityEvent();
        float center3Up = 0.15f;
        bool wasOutOfView = false;
        KuriBTEventRouter _kuriBTEventRouter;
        KuriBTEventRouter kuriBTEventRouter {
            get {
                if (_kuriBTEventRouter == null) {
                    _kuriBTEventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _kuriBTEventRouter;
            }
        }

        bool _isInViewPort;
        public bool IsInViewPort {
            get {
                return _isInViewPort;
            }
        }

        public bool IsBehindPlayer, IsOffScreen;
        #endregion
        #region unity
        void Awake() {
            kuriBTEventRouter.AddEvent(EventNames.KuriEnterViewPort, KuriEnterViewPort);
        }
        void Update() {
            UpdateAllTargets();
            UpdateIsInViewPort();
        }
        #endregion
        #region public
        #endregion
        #region private
        void UpdateAllTargets() {
            kuriWorldTarget = kuriTransformManager.Position + Vector3.up * center3Up; // offset a bit up
            screenPos = MainCam.WorldToScreenPoint(kuriWorldTarget);
            kuriRelativeToCamera = MainCam.transform.InverseTransformPoint(kuriWorldTarget);
            cappedScreenPos = new Vector3(
                Mathf.Clamp(screenPos.x, 0, Screen.width),
                Mathf.Clamp(screenPos.y, 0, Screen.height),
                screenPos.z);
            viewPortPos = MainCam.WorldToViewportPoint(kuriWorldTarget);
        }

        private void UpdateIsInViewPort() {
            IsOffScreen = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;
            IsBehindPlayer = kuriRelativeToCamera.z < 0;
            _isInViewPort = !IsOffScreen && !IsBehindPlayer;
            if (wasOutOfView && IsInViewPort) {
                KuriEnterViewPort.Invoke();
                wasOutOfView = false;
            }
            if (!IsInViewPort)
                wasOutOfView = true;
        }
        #endregion
    }
}
