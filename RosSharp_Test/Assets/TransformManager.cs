using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public abstract class TransformManager : MonoBehaviour {
        #region members
        Transform _originT;
        protected Transform OriginT {
            get {
                if (_originT == null) {
                    _originT = transform; // using my transform for now
                }
                return _originT;
            }
        }

		public abstract Vector3 Forward { get; }
		public abstract Vector3 Position { get; }

        #endregion

        #region unity
        #endregion

        #region public
        #endregion

        #region private
        #endregion
    }
}
