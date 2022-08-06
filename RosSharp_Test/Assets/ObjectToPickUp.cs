using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ObjectToPickUp : MonoBehaviour {
        #region members
        ObjToPickUpJSONFileManager _objFileManager;
        ObjToPickUpJSONFileManager ObjDialogueFileManager {
            get {
                if (_objFileManager == null) {
                    _objFileManager = ObjToPickUpJSONFileManager.instance;
                }
                return _objFileManager;
            }
        }
        DialogueJSON _dialogueJSON;
        public DialogueJSON DialogueJSON_ {
            get {
                if (_dialogueJSON == null) {
                    _dialogueJSON = ObjDialogueFileManager.GetDialogueJSON(name.ToLower());
                }
                return _dialogueJSON;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public void Dialogue() {
            Debug.Log(DialogueJSON_.OptionA);
        }
        #endregion
        #region private
        #endregion
    }
}
