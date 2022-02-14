using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class NavPath {
        public List<NavPathPoint> Path;

        public NavPath(List<Transform> transformPath) {
            SetPath(transformPath);
        }
        public NavPath(List<Vector3> vectorPath) {
            SetPath(vectorPath);
        }

        public void SetPath(List<Transform> transformPath) {
            ResetPath();
            foreach (Transform t in transformPath) {
                Path.Add(new NavPathPoint(t));
            }
        }

        public void SetPath(List<Vector3> vectorPath) {
            ResetPath();
            foreach (Vector3 v in vectorPath) {
                Path.Add(new NavPathPoint(v));
            }
        }

        public void ResetPath() {
            Path?.Clear();
            Path = new List<NavPathPoint>();
        }
    }
}
