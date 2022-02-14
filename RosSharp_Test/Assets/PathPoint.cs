using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class PathPoint : MonoBehaviour {
        #region members
        MeshRenderer sphereRend;
        GameObject kuri;
        LineRenderer lr;
        #endregion

        #region unity
        private void Awake() {
            sphereRend = GetComponent<MeshRenderer>();
            kuri = transform.GetChild(0).gameObject; // hardCoded
            lr = GetComponent<LineRenderer>();
        }
        #endregion

        #region public
        public void UseKuris() {
            sphereRend.enabled = false;
            kuri.SetActive(true);
        }
        public void UseSpheres() {
            sphereRend.enabled = true;
            kuri.SetActive(false);
        }

        public void ChangePointColor(Color c) {
            sphereRend.material.color = c;
        }
        public void ChangeLRColor(Color c) {
            lr.colorGradient = new Gradient();
            lr.startColor = lr.endColor = c;
        }
        #endregion

        #region private
        #endregion
    }
}
