using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NRISVTE {
    public class PathSignalReceiver : MonoBehaviour {
        #region members
        public bool useKuris = false;
        public Color pathPointColor;
        public Color pathLineColor;
        public Color traversalColor;
        List<PathPoint> pathPoints;
        int curPoint = 0;
        #endregion

        #region unity
        private void Awake() {
            SetUpPathPoints();
        }
        #endregion

        #region public
        public void SignalNextPathPoint() {
            ResetColors(pathPoints[curPoint++]);
            if (curPoint < pathPoints.Count) {
                SetToTraversingColor(pathPoints[curPoint]);
            }
        }

        #endregion

        #region private
        void SetUpPathPoints() {
            pathPoints = new List<PathPoint>();
            foreach (PathPoint pp in GetComponentsInChildren<PathPoint>()) {
                pathPoints.Add(pp);
                if (useKuris) {
                    pp.UseKuris();
                }
                else {
                    pp.UseSpheres();
                }
                ResetColors(pp);
            }
        }

        void ResetColors(PathPoint pp) {
            pp.ChangePointColor(pathPointColor);
            pp.ChangeLRColor(pathLineColor);
        }

        void SetToTraversingColor(PathPoint pp) {
            pp.ChangeLRColor(traversalColor);
            pp.ChangePointColor(traversalColor);
        }
        #endregion
    }
}
