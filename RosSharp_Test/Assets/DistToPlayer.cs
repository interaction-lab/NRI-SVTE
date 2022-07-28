using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class DistToPlayer : MonoBehaviour {
        #region members
        TransformManager _playerTransformManager;
        public TransformManager PlayerT {
            get {
                if (_playerTransformManager == null) {
                    _playerTransformManager = Camera.main.transform.GetComponent<TransformManager>();
                }
                return _playerTransformManager;
            }
        }
		TransformManager _kuriTransformManager;
		public TransformManager KuriT {
			get {
				if (_kuriTransformManager == null) {
					_kuriTransformManager = GetComponent<TransformManager>();
				}
				return _kuriTransformManager;
			}
		}
        #endregion

        #region unity
        #endregion

        #region public
		public float GetDistanceToPlayer() {
			return Vector3.Distance(KuriT.Position, PlayerT.Position);
		}
        #endregion

        #region private
        #endregion
    }
}
