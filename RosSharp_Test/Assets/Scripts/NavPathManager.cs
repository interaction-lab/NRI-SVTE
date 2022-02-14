using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NRISVTE {
    public class NavPathManager : Singleton<NavPathManager> {
        #region members
        private NavPath nVPath;
        public NavPath NVPath {
            get {
                if (nVPath == null) {
                    if (TPath != null && !TPath.Empty()) {
                        TPath.Prepend(KuriManager.instance.transform);
                        nVPath = new NavPath(TPath);
                    }
                    else if (VPath != null && !VPath.Empty()) {
                        VPath.Prepend(KuriManager.instance.transform.position);
                        nVPath = new NavPath(VPath);
                    }
                    else {
                        throw new System.Exception("Did not initialize NavPath");
                    }
                }
                return nVPath;
            }
        }

        public List<Transform> TPath;
        public List<Vector3> VPath;
        public bool IsNavigating { get; set; } = false;
        #endregion

        #region public
        public void StartNavigatePath(NavPathGoalController npgc) {
            StartCoroutine(RunThroughPath(npgc));
        }

        #endregion

        #region private
        IEnumerator RunThroughPath(NavPathGoalController npgc) {
            if (IsNavigating) {
                yield break;
            }
            IsNavigating = true;
            foreach (NavPathPoint np in NVPath.Path) {
                npgc.SendNewGoal(np.PointPosition);
                yield return new WaitUntil(() => npgc.AtGoal);
            }
            IsNavigating = false;
        }
        #endregion
    }
}
