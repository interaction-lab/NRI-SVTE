using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ObjToPickUpJSONFileManager : Singleton<ObjToPickUpJSONFileManager> {
        #region members
        static string resourcePathToJSONs = "ObjectToPickUpFolder/Dialogue/";
        Dictionary<string, DialogueJSON> _dialogueJSONs;
        // filename (lowercase) -> DialogueJSON
        Dictionary<string, DialogueJSON> DialogueJSONs {
            get {
                if (_dialogueJSONs == null) {
                    SetUpDialogueJSONs();
                }
                return _dialogueJSONs;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public DialogueJSON GetDialogueJSON(string objname) {
            if (DialogueJSONs.ContainsKey(objname)) {
                return DialogueJSONs[objname];
            }
            Debug.LogError("Did not find dialogue json for " + objname);
            return null;
        }

        #endregion
        #region private
        private void SetUpDialogueJSONs() {
            _dialogueJSONs = new Dictionary<string, DialogueJSON>();
            TextAsset[] files = Resources.LoadAll<TextAsset>(resourcePathToJSONs);
            foreach (TextAsset file in files) {
                _dialogueJSONs.Add(file.name.ToLower(),
                    Newtonsoft.Json.JsonConvert.DeserializeObject<DialogueJSON>(file.text));
            }
        }
        #endregion
    }
}