using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NRISVTE {
    public class OptionsButtonManager : Singleton<OptionsButtonManager> {
        public GameObject inGameMenu, optionsMenu;
        Button _button;
        public Button button {
            get {
                if (_button == null) {
                    _button = GetComponent<Button>();
                }
                return _button;
            }
        }
        #region unity
        void Awake() {
            button.onClick.AddListener(OnClick);
            inGameMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        void OnDestroy() {
            button.onClick.RemoveListener(OnClick);
        }

        public void ToggleStop(bool stopOn){
            inGameMenu.SetActive(stopOn);
            optionsMenu.SetActive(!stopOn);
        }
        #endregion
        #region private
        private void OnClick() {
            bool optionsOn = optionsMenu.activeSelf;
            inGameMenu.SetActive(optionsOn);
            optionsMenu.SetActive(!optionsOn);
        }

        #endregion
    }
}
