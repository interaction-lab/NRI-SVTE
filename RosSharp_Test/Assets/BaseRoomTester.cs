using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace NRISVTE {
    public class BaseRoomTester : Singleton<BaseRoomTester> {
        #region members	
        public List<ARPlane> _fakeplanes = null;
        public List<ARPlane> FakePlanes {
            get {
                if (_fakeplanes == null || _fakeplanes.Empty()) {
                    _fakeplanes = new List<ARPlane>();
                    foreach (ARPlane plane in GetComponentsInChildren<ARPlane>(true)) {
                        _fakeplanes.Add(plane);
                    }
                }
                return _fakeplanes;
            }
        }
        #endregion

        #region unity
        #endregion

        #region public
        public BaseRoomTester() {
#if UNITY_EDITOR
            UnityMainThread.wkr?.AddJob(() => {
                gameObject.SetActive(true);
            });
#else
            UnityMainThread.wkr?.AddJob(() => {
                gameObject.SetActive(false);
            });
#endif
        }
        #endregion

        #region private
        #endregion
    }
}
