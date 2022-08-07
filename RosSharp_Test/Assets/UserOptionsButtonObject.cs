using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace NRISVTE {
    public class UserOptionsButtonObject : MonoBehaviour {
        #region members
        Button optionAButton, optionBButton;
        public Button OptionAButton {
            get {
                if (optionAButton == null) {
                    optionAButton = GetComponentInChildren<UserOptionAButton>(true).GetComponent<Button>();
                }
                return optionAButton;
            }
        }
        public Button OptionBButton {
            get {
                if (optionBButton == null) {
                    optionBButton = GetComponentInChildren<UserOptionBButton>(true).GetComponent<Button>();
                }
                return optionBButton;
            }
        }

        TextMeshProUGUI optionAText, optionBText;
        public TextMeshProUGUI OptionAText {
            get {
                if (optionAText == null) {
                    optionAText = OptionAButton.GetComponentInChildren<TextMeshProUGUI>(true);
                }
                return optionAText;
            }
        }
        public TextMeshProUGUI OptionBText {
            get {
                if (optionBText == null) {
                    optionBText = OptionBButton.GetComponentInChildren<TextMeshProUGUI>(true);
                }
                return optionBText;
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
