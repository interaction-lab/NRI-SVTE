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

        public float[] rangies;

        void Start() {
            if (widthSlider == null) {
                // enabled = false;
                Debug.LogWarning("Disabling ShowLasers as Slider is not set, might be intended for this scene. This script should be migrated to not use public member/drag and drop.");
                // return;
            }
            // return;
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
        }

        public class Message {
            public float[] ranges;
            public float maxRange;
        }

        public void UpdateRanges(Message message) {
            // return;
            float[] ranges = message.ranges;
            rangies = ranges;
            float maxRange = message.maxRange;
            for (int i = 0; i < 180; i++) {
                GameObject pf = prefabs[i];
                LineRenderer laserLine = pf.GetComponent<LineRenderer>();
                laserLine.enabled = StaticLinesEnabled;

                if (StaticPointsEnabled){
                  // var point = pf.transform.Find("Point");
                  Transform point = pf.transform.GetChild(1);
                  point.gameObject.SetActive(true);
                  // point.SetActive(true);
                  point.transform.position = pf.transform.position + (pf.transform.forward * ranges[i]);
                  } else {
                    pf.transform.GetChild(1).gameObject.SetActive(false);
                  }

                // var sphere = pf.transform.Find("Sphere");
                Transform sphere = pf.transform.GetChild(0);
                TrailRenderer lidarTrail = sphere.GetComponent<TrailRenderer>();
                var PM = sphere.GetComponent<ProjectileMotion>();
                PM.startPose = pf.transform.position;
                PM.endPose = pf.transform.position + (pf.transform.forward * maxRange);
                PM.maxRange = maxRange;
                PM.range = ranges[i];

                laserLine.startWidth = lidarWidth;
                // laserLine.SetPosition (0, transform.position);
                // laserLine.SetPosition(1,transform.position + (pf.transform.forward * ranges[i]));
                laserLine.SetPosition (0, pf.transform.position);
                laserLine.SetPosition(1,pf.transform.position + (pf.transform.forward * ranges[i]));
                laserLine.material.color = lidarColor;
                lidarWidth = widthSlider.value;
                // lidarColor = Color.HSVToRGB(colorSlider.value, 1, 1);
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
