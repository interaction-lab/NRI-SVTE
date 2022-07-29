using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
	public class SphereCastAStarPlanner : MonoBehaviour{
		#region members
		List<Vector3> _path = new List<Vector3>();
		public List<Vector3> Path {
			get {
				return _path;
			}
		}

		Vector3 _startPos;
		public Vector3 StartPos {
			get {
				return _startPos;
			}
		}
		Vector3 _endPos;
		public Vector3 EndPos {
			get {
				return _endPos;
			}
		}
		Vector3 _currentPos;
		public Vector3 CurrentPos {
			get {
				return _currentPos;
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
