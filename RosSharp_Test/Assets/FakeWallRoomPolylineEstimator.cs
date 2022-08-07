using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace NRISVTE {
    public class FakeWallRoomPolylineEstimator : Singleton<FakeWallRoomPolylineEstimator> {
        #region members
        ARPlaneManager _planeManager;
        ARPlaneManager ARPlaneManager_ {
            get {
                if (_planeManager == null) {
                    _planeManager = FindObjectOfType<ARPlaneManager>();
                }
                return _planeManager;
            }
        }
        List<ARPlane> WallARPlanes {
            get {
                List<ARPlane> wallARPlanes = new List<ARPlane>();
#if !UnityEditor
                foreach (ARPlane plane in ARPlaneManager_.trackables) {
                    if (plane.alignment == PlaneAlignment.Vertical) {
                        wallARPlanes.Add(plane);
                    }
                }
#else
                // get fake walls here
                Debug.Log("here");
                wallARPlanes = BaseRoomTester.instance.FakePlanes;
#endif
                return wallARPlanes;
            }
        }
        #endregion
        #region unity
        void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha0)) {
                Debug.Log(0);
                foreach (var plane in WallARPlanes) {
                    Debug.Log(plane.boundary);
                }
            }
        }
        #endregion
        #region public
        #endregion
        #region private
        #endregion
    }
}
