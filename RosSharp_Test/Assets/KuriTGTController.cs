using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriTGTController : MonoBehaviour {
        #region members
        public float speed = 0.1f;
        public float rotSpeedDegPerSec = 90f;
        public float distThreshold = 0.2f;
        public float rotationThreshold = 0.1f;

        KuriTransformManager _kuriT;
        public KuriTransformManager KuriT {
            get {
                if (_kuriT == null) {
                    _kuriT = GetComponent<KuriTransformManager>();
                }
                return _kuriT;
            }
        }
        PlayerTransformManager _playerT;
        public PlayerTransformManager PlayerT {
            get {
                if (_playerT == null) {
                    _playerT = Camera.main.transform.GetComponent<PlayerTransformManager>();
                }
                return _playerT;
            }
        }
        KuriStateManager _kuriState;
        public KuriStateManager KuriState {
            get {
                if (_kuriState == null) {
                    _kuriState = GetComponent<KuriStateManager>();
                }
                return _kuriState;
            }
        }
        #endregion

        #region unity
        void Update() {
            if (KuriState.Rstate == KuriStateManager.States.BeingPlaced) {
                return;
            }
            UpdateState();
            ReactToState();
        }


        #endregion

        #region public
        #endregion

        #region private
        private void UpdateState() {
            if (KuriState.Rstate == KuriStateManager.States.StoppedByButton) {
                return; // do nothing
            }
            if (Vector3.Distance(KuriT.FlatPosition, PlayerT.FlatPosition) < distThreshold) {
                KuriState.SetState(KuriStateManager.States.Idle);
            }
            else {
                KuriState.SetState(KuriStateManager.States.Moving);
            }
        }
        private void ReactToState() {
            if (KuriState.Rstate == KuriStateManager.States.Moving) {
                Move();
            }
			else if(KuriState.Rstate == KuriStateManager.States.StoppedByButton) {
				Stop();
			}
        }
        void Move() {
            Vector3 desiredForward = PlayerT.FlatPosition - KuriT.FlatPosition;
            float angleToUser = Vector3.SignedAngle(KuriT.FlatForward, desiredForward, KuriT.OriginT.up);
            if (Mathf.Abs(angleToUser) > rotationThreshold) {
                KuriT.OriginT.RotateAround(KuriT.Position,
                KuriT.OriginT.up,
                (angleToUser < 0 ? -1 : 1) * rotSpeedDegPerSec * Time.deltaTime);
            }
            else if (Vector3.Distance(KuriT.FlatPosition, PlayerT.FlatPosition) > distThreshold) {
                // make sure kuri rotates toward user
                KuriT.Rotation = Quaternion.LookRotation(desiredForward);
                // make kuri move toward user over time
                KuriT.Position += KuriT.Forward * Time.deltaTime * speed;
            }
            // keep kuri on the ground
            KuriT.Position = new Vector3(KuriT.Position.x, 0, KuriT.Position.z);
            // make sure kuri is rotationally aligned with the ground
            KuriT.Rotation = Quaternion.LookRotation(KuriT.FlatForward, Vector3.up);
        }
		void Stop() {
		} // placeholder, does nothing for now
        #endregion
    }
}
