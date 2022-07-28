using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NRISVTE {
	public class DebugTextManager : Singleton<DebugTextManager> {
		#region members
		TextMeshProUGUI _debugText;
		TextMeshProUGUI debugText {
			get {
				if (_debugText == null) {
					_debugText = GetComponent<TextMeshProUGUI>();
				}
				return _debugText;
			}
		}
		#endregion

		#region unity
		void Awake(){
			SetDebugText("Awake");
		}
		#endregion

		#region public
		public void SetDebugText(string text) {
			debugText.text = text;
		}
		#endregion

		#region private
		#endregion
	}
}
