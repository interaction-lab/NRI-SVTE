using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class PathPoint : MonoBehaviour {
        #region members
        MeshRenderer sphereRend = null;
        MeshRenderer SphereRend {
            get {
                if (sphereRend == null) {
                    sphereRend = GetComponent<MeshRenderer>();
                }
                return sphereRend;
            }
        }
        GameObject kuri = null;
        GameObject Kuri {
            get {
                if (kuri == null) {
                    kuri = transform.GetChild(0).gameObject; // hardCoded
                }
                return kuri;
            }
        }
        LineRenderer lr = null;
        LineRenderer LR {
            get {
                if (lr == null) {
                    lr = GetComponent<LineRenderer>();
                }
                return lr;
            }
        }
        #endregion

        #region unity
        #endregion

        #region public
        public void UseKuris() {
            SphereRend.enabled = false;
            Kuri.SetActive(true);
        }
        public void UseSpheres() {
            SphereRend.enabled = true;
            Kuri.SetActive(false);
            transform.localScale *= 70f;
            transform.Rotate(new Vector3(90, 0, 0));
        }

        public void ChangePointColor(Color c) {
            if (SphereRend.enabled) {
                SphereRend.material.color = c;
            }
            else {

            }
        }
        public void ChangeLRColor(Color c) {
            LR.material.color = c;
            LR.startColor = LR.endColor = c;
        }
        #endregion

        #region private
        #endregion
    }
}
