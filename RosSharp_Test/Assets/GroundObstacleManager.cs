using System.Collections;
using System.Collections.Generic;


namespace NRISVTE {
    public class GroundObstacleManager : Singleton<GroundObstacleManager> {
        #region members

        List<NavMeshObstaclePolyline> _GroundObstaclePolyLines;
        List<NavMeshObstaclePolyline> GroundObstaclePolyLines {
            get {
                if (_GroundObstaclePolyLines == null) {
                    _GroundObstaclePolyLines = new List<NavMeshObstaclePolyline>();
                    foreach (var o in GetComponentsInChildren<UnityEngine.AI.NavMeshObstacle>(true)) {
                        NavMeshObstaclePolyline polyline = o.GetComponent<NavMeshObstaclePolyline>();
                        if (polyline == null) {
                            polyline = o.gameObject.AddComponent<NavMeshObstaclePolyline>();
                        }
                        _GroundObstaclePolyLines.Add(polyline);
                    }
                }
                return _GroundObstaclePolyLines;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public List<List<List<float>>> GetObstaclePolyLines(){
            List<List<List<float>>> obstaclePolyLines = new List<List<List<float>>>();
            foreach (var polyline in GroundObstaclePolyLines) {
                obstaclePolyLines.Add(polyline.CalcPolylineInKuriCords());
            }
            return obstaclePolyLines;
        }
        #endregion
        #region private
        #endregion
    }

}
