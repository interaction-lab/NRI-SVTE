using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace NRISVTE {
    public class NavMeshObstaclePolyline : MonoBehaviour {
        #region members
        List<List<float>> Polyline = new List<List<float>>();
        KuriTransformManager _KuriT;
        KuriTransformManager KuriT {
            get {
                if (_KuriT == null) {
                    _KuriT = KuriManager.instance.GetComponent<KuriTransformManager>();
                }
                return _KuriT;
            }
        }
        #endregion
        #region unity
        private void FixedUpdate() {
            CalcPolylineInKuriCords();    
        }
        #endregion
        #region public
        public List<List<float>> CalcPolylineInKuriCords() {
            if (Polyline.Empty()) {
                PopulatePolyline();
            }
            return Polyline;
        }
        #endregion
        #region private
        private void PopulatePolyline(){
            // get navmesh obstacle
            NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
            // get the shape of the obstacle
            bool isBox = obstacle.shape == NavMeshObstacleShape.Box;
            Assert.IsTrue(isBox, "NavMeshObstaclePolyline only supports box obstacles");
            // get the box size of the obstacle
            Vector3 boxSize = obstacle.size;
            float width = boxSize.x;
            // float height = boxSize.y; // not used for polyline
            float depth = boxSize.z;

            // scale the box size with lossyScale
            Vector3 lossyScale = transform.lossyScale;
            width *= lossyScale.x;
            depth *= lossyScale.z;


            Vector3 objectPosition = transform.position;
            Vector3 obstacleBoxPosition = objectPosition + obstacle.center;

            // get the corners of the obstacle
            Polyline.Add(new List<float>(){obstacleBoxPosition.x - width / 2f, obstacleBoxPosition.z - depth / 2f});
            Polyline.Add(new List<float>(){obstacleBoxPosition.x - width / 2f, obstacleBoxPosition.z + depth / 2f});
            Polyline.Add(new List<float>(){obstacleBoxPosition.x + width / 2f, obstacleBoxPosition.z + depth / 2f});
            Polyline.Add(new List<float>(){obstacleBoxPosition.x + width / 2f, obstacleBoxPosition.z - depth / 2f});

            // need to subtract off KuriT.Position to get the correct position in KuriCords
            for (int i = 0; i < 4; ++i) {
                Polyline[i][0] -= KuriT.Position.x;
                Polyline[i][1] -= KuriT.Position.z;
                Polyline[i][0] *= 100; // m to cm
                Polyline[i][1] *= 100; // m to cm
            }
        }
        #endregion
    }
}