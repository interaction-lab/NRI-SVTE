using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class BaseRoomTester : MonoBehaviour {
        #region members		
        #endregion

        #region unity
        #endregion

        #region public
        public BaseRoomTester() {
#if UNITY_EDITOR
            UnityMainThread.wkr.AddJob(() => {
                gameObject.SetActive(true);
            });
#else
 			UnityMainThread.wkr.AddJob(() => {
                gameObject.SetActive(false);
            });
#endif
        }
        #endregion

        #region private
        #endregion
    }
}
