using UnityEngine;

namespace NRISVTE {
	public class KuriTransformManager : TransformManager {
		#region members
		public override Vector3 Forward {
			get {
				return -OriginT.forward;
			}
		}

		public override Vector3 Position {
			get {
				return OriginT.position;
			}
		}
		#endregion

		#region unity
		#endregion

		#region public
		#endregion

		#region private
		#endregion
	}
}
