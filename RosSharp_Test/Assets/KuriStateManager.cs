using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine.Events;


namespace NRISVTE {
    public class KuriStateManager : Singleton<KuriStateManager> {
        #region members
        public enum States {
            Idle,
            BeingPlaced,
            Moving,
			StoppedByButton
        }
        [SerializeField]
        States _state = States.Idle;
        public States Rstate {
            get {
                return _state;
            }
            private set {
                _state = value;
            }
        }


        public UnityEvent OnStateChanged;
        #endregion

        #region unity
        void Start() {
        }
        #endregion

        #region public
        public void SetState(States state) {
            Rstate = state;
            OnStateChanged.Invoke();
        }
        #endregion

        #region private
        #endregion
    }
}
