using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class ConnectButtonManager : MonoBehaviour {
        #region members
        Button _button;
        public Button button {
            get {
                if (_button == null) {
                    _button = GetComponent<Button>();
                }
                return _button;
            }
        }
        #endregion
        #region unity
        void Awake() {
            button.onClick.AddListener(OnClick);
        }
        void OnDestroy() {
            button.onClick.RemoveListener(OnClick);
        }
        #endregion
        #region private
        private void OnClick() {
            ConnectionManager.instance.Connect();
        }
        #endregion
    }
}
