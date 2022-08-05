using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace NRISVTE {
    public class UIArrow : MonoBehaviour {
        #region members
        KuriTransformManager _kuriTransformManager;
        KuriTransformManager kuriTransformManager {
            get {
                if (_kuriTransformManager == null) {
                    _kuriTransformManager = FindObjectOfType<KuriTransformManager>();
                }
                return _kuriTransformManager;
            }
        }

        Camera _mainCam;
        Camera MainCam {
            get {
                if (_mainCam == null) {
                    _mainCam = Camera.main;
                }
                return _mainCam;
            }
        }

        Image _arrowImage;
        Image ArrowImage {
            get {
                if (_arrowImage == null) {
                    _arrowImage = GetComponent<Image>();
                }
                return _arrowImage;
            }
        }

        float center3Up = 0.15f;
        bool wasOutOfView = false;
        UnityEvent KuriEnterViewPort = new UnityEvent();
        KuriBTEventRouter _kuriBTEventRouter;
        KuriBTEventRouter kuriBTEventRouter {
            get {
                if (_kuriBTEventRouter == null) {
                    _kuriBTEventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _kuriBTEventRouter;
            }
        }

        #endregion

        #region unity
        void Awake() {
            kuriBTEventRouter.AddEvent(EventNames.KuriEnterViewPort, KuriEnterViewPort);
        }
        void Update() {
            // get viewport position of kuri
            Vector3 viewportPos = MainCam.WorldToViewportPoint(kuriTransformManager.Position + Vector3.up * center3Up);

            // check if on screen
            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1) {
                // not on screen
                ArrowImage.enabled = true;
                wasOutOfView = true;
            }
            else {
                // on screen
                ArrowImage.enabled = false;
                if (wasOutOfView) {
                    KuriEnterViewPort.Invoke();
                    wasOutOfView = false;
                }
                return; // done
            }

            // get on screen position of kuri
            Vector2 screenPos = new Vector2(
                (viewportPos.x * Screen.width) - (Screen.width / 2f),
                (viewportPos.y * Screen.height) - (Screen.height / 2f)
            );

            // get largest offset from center
            float maxOffset = Mathf.Max(
                Mathf.Abs(screenPos.x),
                Mathf.Abs(screenPos.y)
            );

            // convert back to viewport space
            screenPos = (screenPos / (maxOffset * 2f)) + new Vector2(0.5f, 0.5f);

            // set arrow position to screenPos
            ArrowImage.rectTransform.anchorMin = screenPos;
            ArrowImage.rectTransform.anchorMax = screenPos;
            ArrowImage.rectTransform.anchoredPosition = Vector2.zero;

            // convert screenPos to back to screen space
            screenPos = (screenPos - new Vector2(0.5f, 0.5f)) * maxOffset * 2f;
            // get direction from screenPos to center
            Vector2 direction = screenPos.normalized;
            // get angle from direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // set arrow angle to angle
            ArrowImage.rectTransform.localEulerAngles = new Vector3(0, 0, angle + 180);

        }


        #endregion

        #region public
        #endregion

        #region private
        #endregion
    }
}
