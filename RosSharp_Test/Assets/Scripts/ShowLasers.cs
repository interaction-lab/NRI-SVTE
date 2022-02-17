using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class ShowLasers : MonoBehaviour {
        public GameObject prefab;
        public bool StaticLinesEnabled = false;
        public bool StaticPointsEnabled = false;

        public Slider widthSlider;
        public Slider colorSlider;
        public FlexibleColorPicker cp;

        public GameObject[] prefabs;
        private Color lidarColor = Color.white;
        private float lidarWidth = 0.00f;
        float noiseLevel = 0.001f;
        float publishHZ = 15, lastTimeStamp = 0, rateInMS = -1;

        public float[] rangies;


        void Start() {
            if (widthSlider == null) {
                // enabled = false;
                Debug.LogWarning("Disabling ShowLasers as Slider is not set, might be intended for this scene. This script should be migrated to not use public member/drag and drop.");
                // return;
            }
            prefabs = new GameObject[180];

            for (int i = 0; i < 180; i++) {
                float angle = i * Mathf.PI / 180;
                float angleDegrees = 180 + angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                GameObject pf = Instantiate(prefab, transform.position, rot, transform) as GameObject;
                prefabs[i] = pf;
                LineRenderer laserLine = pf.GetComponent<LineRenderer>();
                laserLine.enabled = StaticLinesEnabled;
                TrailRenderer lidarTrail = pf.GetComponent<TrailRenderer>();
                lidarWidth = widthSlider.value;


                lidarTrail.startWidth = lidarWidth;
            }

            rateInMS = 1000f / publishHZ;
        }

        public class Message {
            public float[] ranges;
            public float maxRange;
        }

        public void UpdateRanges(Message message) {
            if (Time.time * 1000f - lastTimeStamp < rateInMS) {
                return;
            }
            lastTimeStamp = Time.time * 1000f;

            float[] ranges = message.ranges;
            rangies = ranges;
            float maxRange = message.maxRange;
            for (int i = 0; i < 180; i++) {
                GameObject pf = prefabs[i];
                LineRenderer laserLine = pf.GetComponent<LineRenderer>();
                laserLine.enabled = StaticLinesEnabled;

                // calculate end point
                Vector3 pos = pf.transform.position;
                pos = pos + (pf.transform.forward * ranges[i]);
                pos.x *= (1 + Random.Range(-noiseLevel, noiseLevel));
                pos.z *= (1 + Random.Range(-noiseLevel, noiseLevel));

                // Turn on/off points
                if (StaticPointsEnabled) {
                    Transform point = pf.transform.GetChild(1);
                    point.gameObject.SetActive(true);
                    point.transform.position = pos;
                }
                else {
                    pf.transform.GetChild(1).gameObject.SetActive(false);
                }

                // Turn on/off trailrenderer
                Transform sphere = pf.transform.GetChild(0);
                TrailRenderer lidarTrail = sphere.GetComponent<TrailRenderer>();
                var PM = sphere.GetComponent<ProjectileMotion>();
                PM.startPose = pf.transform.position;
                PM.endPose = pf.transform.position + (pf.transform.forward * maxRange);
                PM.maxRange = maxRange;
                PM.range = ranges[i];

                // laser line positions
                laserLine.startWidth = lidarWidth;
                laserLine.SetPosition(0, pf.transform.position);
                laserLine.SetPosition(1, pos);
                laserLine.material.color = lidarColor;
                lidarWidth = widthSlider.value;
                lidarColor = cp.color;

                lidarTrail.startWidth = lidarWidth;
                lidarTrail.material.color = lidarColor;


            }
        }

        public void ToggleAllLidarRenderers(bool on) {
            foreach (GameObject go in prefabs) {
                go.SetActive(on);
            }
        }
    }
}
