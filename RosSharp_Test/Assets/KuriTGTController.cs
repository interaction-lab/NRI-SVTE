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
            if (Vector3.Distance(KuriT.GroundPosition, PlayerT.GroundPosition) < distThreshold) {
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
            else if (KuriState.Rstate == KuriStateManager.States.StoppedByButton) {
                Stop();
            }
        }
        void Move() {
            Vector2 desiredForward = (PlayerT.TwoDPosition - KuriT.TwoDPosition);
            Vector2 k2DForward = KuriT.TwoDForward;
            float angleToUser = Vector2.SignedAngle(k2DForward, desiredForward);
            if (Mathf.Abs(angleToUser) > rotationThreshold) {
                KuriT.OriginT.RotateAround(KuriT.Position,
                Vector3.up,
                (angleToUser < 0 ? 1 : -1) * rotSpeedDegPerSec * Time.deltaTime);
            }
            else if (Vector3.Distance(KuriT.GroundPosition, PlayerT.GroundPosition) > distThreshold) {
                // make sure kuri rotates toward user
                Vector3 desiredForward3D = new Vector3(desiredForward.x, 0, desiredForward.y);
                KuriT.Rotation = Quaternion.LookRotation(desiredForward3D, Vector3.up);
                // make kuri move toward user over time
                KuriT.Position += KuriT.Forward * Time.deltaTime * speed;
            }
            // keep kuri on the ground
            KuriT.Position = new Vector3(KuriT.Position.x, KuriT.GroundYCord, KuriT.Position.z);
            // make sure kuri is aligned vertically with the ground
            KuriT.OriginT.rotation = Quaternion.LookRotation(KuriT.OriginT.forward, Vector3.up);
        }
        void Stop() {
        } // placeholder, does nothing for now
        #endregion
    }
}
