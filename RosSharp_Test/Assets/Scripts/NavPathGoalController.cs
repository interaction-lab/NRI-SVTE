using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class NavPathGoalController : MonoBehaviour {
        #region members
        public bool AtGoal = false;
        private float distThreshold = 0.025f;
        private float speed = 0.005f;
        #endregion

        #region unity
        #endregion

        #region public
        public void SendNewGoal(Vector3 goalPos) {
            StartCoroutine(GoToGoal(goalPos));
        }
        #endregion

        #region private
        IEnumerator GoToGoal(Vector3 goalPos) {
            AtGoal = false;
            while (Vector3.Distance(goalPos, transform.position) > distThreshold) {
                Vector3 newPos = transform.position;
                Vector3 dir = (goalPos - transform.position).normalized; // this is why the kuri in m2c is slow
                newPos += dir * speed;
                transform.position = newPos;
                yield return null;
            }
            AtGoal = true;
        }
        #endregion
    }
}
