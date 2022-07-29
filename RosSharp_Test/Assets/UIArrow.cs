using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        #endregion

        #region unity
        void Update() {
            // get viewport position of kuri
            Vector3 viewportPos = MainCam.WorldToViewportPoint(kuriTransformManager.Position);


            // check if on screen
            if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1) {
                // not on screen
                ArrowImage.enabled = true;
            }
            else {
                // on screen
                ArrowImage.enabled = false;
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

			// undo mapping from viewport to screen
			screenPos = (screenPos / (maxOffset * 2f)) + new Vector2(0.5f, 0.5f);

			// set arrow position to screenPos
			ArrowImage.rectTransform.anchorMin = screenPos;
			ArrowImage.rectTransform.anchorMax = screenPos;
			ArrowImage.rectTransform.anchoredPosition = Vector2.zero;

			
            
        }


        #endregion

        #region public
        #endregion

        #region private
        #endregion
    }
}
