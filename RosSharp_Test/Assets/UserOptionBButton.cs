using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class UserOptionBButton : MonoBehaviour {
        #region members
        Button _button;
        Button button {
            get {
                if (_button == null) {
                    _button = GetComponent<Button>();
                }
                return _button;
            }
        }
        #endregion
        #region unity
        void Start() {
            button.onClick.AddListener(OnClick);
        }
        void OnDestroy() {
            button.onClick.RemoveListener(OnClick);
        }
        #endregion
        #region public
        #endregion
        #region private
        void OnClick() {
            DialogueManager.instance.ClickedOption(DialogueManager.Option.B);
        }
        #endregion
    }
}
