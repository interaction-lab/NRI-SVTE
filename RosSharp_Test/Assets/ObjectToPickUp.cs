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
        public string OptionA {
            get {
                return DialogueJSON_.OptionA;
            }
        }
        public string OptionB {
            get {
                return DialogueJSON_.OptionA;
            }
        }
         public string Question {
            get {
                return DialogueJSON_.Question;
            }
        }
         public string ResponseA {
            get {
                return DialogueJSON_.ResponseA;
            }
        }
         public string ResponseB {
            get {
                return DialogueJSON_.ResponseB;
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
