using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class NavPathManager : Singleton<NavPathManager> {
        #region members
        private NavPath nVPath;
        public NavPath NVPath {
            get {
                if (nVPath == null) {
                    if (TPath != null) {
                        nVPath = new NavPath(TPath);
                    }
                    else if (VPath != null) {
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
        public bool IsNavigating { get; set; }
        #endregion

        #region public
        public void StartNavigatePath(NavPathGoalController npgc) {
            IsNavigating = false;
            Debug.LogError("kldfsklsfdkjlfds");
            StartCoroutine(RunThroughPath(npgc));
        }

        #endregion

        #region private
        IEnumerator RunThroughPath(NavPathGoalController npgc) {
            if (IsNavigating) {
                yield break;
            }
            Debug.LogError("fdkljs");
            Debug.Log(NVPath.Path.Count);
            IsNavigating = true;
            foreach (NavPathPoint np in NVPath.Path) {
                Debug.LogError(np.PointPosition);
                npgc.SendNewGoal(np.PointPosition);
                yield return new WaitUntil(() => npgc.AtGoal);
            }
            IsNavigating = false;
        }
        #endregion
    }
}
