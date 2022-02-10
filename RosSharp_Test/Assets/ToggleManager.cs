using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ToggleManager : Singleton<ToggleManager> {

        public void ToggleLidar(bool on) {
            FindObjectOfType<ShowLasers>().ToggleAllLidarRenderers(on);
        }

    }
}