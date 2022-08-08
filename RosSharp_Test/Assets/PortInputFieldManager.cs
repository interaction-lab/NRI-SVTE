using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NRISVTE {
    public class PortInputFieldManager : Singleton<PortInputFieldManager> {
        #region members
        TMP_InputField _inputField;
        public TMP_InputField inputField {
            get {
                if (_inputField == null) {
                    _inputField = GetComponentInChildren<TMP_InputField>(true);
                }
                return _inputField;
            }
        }

        public string PortNumber = "";
        #endregion
        #region unity
        void Awake() {
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.text = PortNumber;
        }

        void OnDestroy() {
            inputField.onValueChanged.RemoveListener(OnValueChanged);
        }
        #endregion

        #region private
        private void OnValueChanged(string value) {
            PortNumber = value;
        }
        #endregion
    }
}