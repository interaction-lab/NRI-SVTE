using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KeyboardTester : MonoBehaviour {
        #region members
        #endregion
        #region unity
        void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha0)) {
                // send server msg
                ServerJSONManager.instance.SendLabeledPoint(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                // send server msg
                ServerJSONManager.instance.RequestNextSamplePoint();
            }
        }
        #endregion
        #region public
        #endregion
        #region private
        #endregion
    }
}
