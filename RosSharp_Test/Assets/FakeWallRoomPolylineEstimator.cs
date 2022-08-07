using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace NRISVTE {
    public class FakeWallRoomPolylineEstimator : Singleton<FakeWallRoomPolylineEstimator> {
        #region members
        KuriTransformManager kuriTransformManager;
        KuriTransformManager KuriT {
            get {
                if (kuriTransformManager == null) {
                    kuriTransformManager = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return kuriTransformManager;
            }
        }
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
#if !UNITY_EDITOR
                foreach (ARPlane plane in ARPlaneManager_.trackables) {
                    if (plane.alignment == PlaneAlignment.Vertical) {
                        wallARPlanes.Add(plane);
                    }
                }
#else
                wallARPlanes = BaseRoomTester.instance.FakePlanes;
#endif
                return wallARPlanes;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public List<List<float>> GetWallPolyLines() {
            List<List<float>> res = new List<List<float>>();
            // go through all WallARPlane get get the right and left bounds
#if !UNITY_EDITOR
            // this likely needs sorting but going to use this for now
            foreach (ARPlane plane in WallARPlanes) {
                LeftAndRightBound leftAndRightBound = plane.GetComponent<LeftAndRightBound>();
                if (leftAndRightBound != null) {
                    res.Add(new List<float>() { leftAndRightBound.LeftBound.x, leftAndRightBound.LeftBound.y });
                    res.Add(new List<float>() { leftAndRightBound.RightBound.x, leftAndRightBound.RightBound.y });
                }
            }
#else
            // fake
            foreach (ARPlane plane in WallARPlanes) {
                FakeLeftRightBound leftAndRightBound = plane.GetComponent<FakeLeftRightBound>();
                if (leftAndRightBound != null) {
                    res.Add(new List<float>() { leftAndRightBound.LeftBound.x, leftAndRightBound.LeftBound.y });
                    res.Add(new List<float>() { leftAndRightBound.RightBound.x, leftAndRightBound.RightBound.y });
                }
            }
#endif
            // convert to kuri space
            ConvertToKuriSpace(res);
            return res;
        }
        #endregion
        #region private
        private void ConvertToKuriSpace(List<List<float>> polylines) {
            for (int i = 0; i < polylines.Count; i++) {
                polylines[i][0] = (polylines[i][0] - KuriT.Position.x) * 100;
                polylines[i][1] = (polylines[i][1] - KuriT.Position.y) * 100;
            }
        }
    }
    #endregion
}

