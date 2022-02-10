using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace NRISVTE {
    public class NavPathVisualizer : MonoBehaviour {
        #region members
        List<GameObject> SphereList { get; set; } = new List<GameObject>();
        GameObject spherePrefab = null;
        GameObject SpherePrefab {
            get {
                if (spherePrefab == null) {
                    spherePrefab = Resources.Load<GameObject>(ResourcePathConstants.NavGoalSphere);
                }
                return spherePrefab;
            }
        }
        private float lrWidth = 0.1f;
        #endregion

        #region unity
        private void Start() {
            CreateSpheres(NavPathManager.instance.NVPath);
        }
        #endregion

        #region public

        #endregion

        #region private
        private void CreateSpheres(NavPath np) {
            foreach (NavPathPoint pp in np.Path) {
                LoadAndAddSphere(pp);
            }
            UpdateLineRenderers();
        }
        private void LoadAndAddSphere(NavPathPoint pp) {
            SphereList.Add(Instantiate(original: SpherePrefab, parent: transform, instantiateInWorldSpace: true) as GameObject);
            SphereList.Last().transform.position = pp.PointPosition;
        }
        private void UpdateLineRenderers() {
            // LR are back to forward, no update to first point in path LR
            for (int i = SphereList.Count - 1; i >= 1; --i) {
                LineRenderer lr = SphereList[i].GetComponent<LineRenderer>();
                lr.SetPositions(new Vector3[] { SphereList[i].transform.position, SphereList[i - 1].transform.position });
                lr.startWidth = lr.endWidth = lrWidth;
            }
        }
        #endregion
    }
}
