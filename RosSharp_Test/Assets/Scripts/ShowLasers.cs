using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class ShowLasers : MonoBehaviour {

        #region members
        public GameObject prefab;
        public bool StaticLinesEnabled = false;
        public bool StaticPointsEnabled = false;
        public bool EmitTrail = false;

        private float movingLaserWidth = 0.02f, lidarWidth = 0.01f;
        public Color movingLaserColor;

        private GameObject[] lidarPrefabArray;
        public Color lidarColor = Color.white;
        float noiseLevel = 0.002f;
        float publishHZ = 15, lastTimeStamp = 0, rateInMS = -1;

        private float[] rangies;
        public class Message {
            public float[] ranges;
            public float maxRange;
        }

        #endregion

        #region Unity
        void Start() {
            CreateLidarPrefabArray();
            rateInMS = 1000f / publishHZ;
            transform.localRotation = Quaternion.Euler(0, 135, 0); // Not sure why this is the offset but it is
        }
        #endregion

        #region public
        public void UpdateRanges(Message message) {
            if (Time.time * 1000f - lastTimeStamp < rateInMS) {
                return;
            }

            lastTimeStamp = Time.time * 1000f;
            float[] ranges = message.ranges;
            rangies = ranges;
            float maxRange = message.maxRange;
            for (int i = 0; i < 180; i++) {
                GameObject lidarPrefab = lidarPrefabArray[i];
                Vector3 pos = calculateLocalEndPoint(lidarPrefab, ranges[i]);
                SetUpStaticPoints(lidarPrefab, pos);
                SetUpTrailRenderer(ranges, maxRange, i, lidarPrefab, lidarWidth, lidarColor);
                SetUpLaserLines(lidarPrefab, pos);
            }
        }

        public void ToggleAllLidarRenderers(bool on) {
            foreach (GameObject go in lidarPrefabArray) {
                go.SetActive(on);
            }
        }
        #endregion

        #region private
        private void CreateLidarPrefabArray() {
            lidarPrefabArray = new GameObject[180];
            for (int i = 0; i < 180; i++) {
                float angle = i * Mathf.PI / 180;
                float angleDegrees = 180 + angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                GameObject pf = Instantiate(prefab, transform.position, rot, transform) as GameObject;
                lidarPrefabArray[i] = pf;
                LineRenderer laserLine = pf.GetComponent<LineRenderer>();
                laserLine.enabled = StaticLinesEnabled;
                TrailRenderer lidarTrail = pf.GetComponent<TrailRenderer>();
                lidarWidth = movingLaserWidth;
                lidarTrail.startWidth = lidarWidth;
            }
        }

        private void SetUpLaserLines(GameObject lidarPrefab, Vector3 pos) {
            LineRenderer laserLine = lidarPrefab.GetComponent<LineRenderer>();
            laserLine.enabled = StaticLinesEnabled;
            laserLine.startWidth = lidarWidth;
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, pos);
            laserLine.material.color = lidarColor;
        }

        bool firstRun = true;
        private void SetUpTrailRenderer(float[] ranges, float maxRange, int i, GameObject lidarPrefab, float lidarWidth, Color lidarColor) {
            Transform sphere = lidarPrefab.transform.GetChild(0);
            TrailRenderer lidarTrail = sphere.GetComponent<TrailRenderer>();
            if (!EmitTrail) {
                lidarTrail.enabled = false;
                return;
            }
            var PM = sphere.GetComponent<ProjectileMotion>();
            PM.startPose = lidarPrefab.transform.position;
            PM.endPose = transform.position + (lidarPrefab.transform.forward * maxRange);
            PM.maxRange = maxRange;
            PM.range = ranges[i];
            lidarTrail.startWidth = movingLaserWidth;
            lidarTrail.material.color = movingLaserColor;
            if (firstRun) {
                PM.EmitTrail();
            }
            if (i == 179) {
                firstRun = false;
            }
        }

        private void SetUpStaticPoints(GameObject lidarPrefab, Vector3 pos) {
            if (StaticPointsEnabled) {
                Transform point = lidarPrefab.transform.GetChild(1);
                point.gameObject.SetActive(true);
                point.transform.position = pos;
            }
            else {
                lidarPrefab.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        private Vector3 calculateLocalEndPoint(GameObject lidarPrefab, float range) {
            Vector3 pos = transform.position;
            pos = pos + (lidarPrefab.transform.forward * range);
            pos.x *= (1 + Random.Range(-noiseLevel, noiseLevel));
            pos.z *= (1 + Random.Range(-noiseLevel, noiseLevel));
            return pos;
        }

        #endregion
    }
}
