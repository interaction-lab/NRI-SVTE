using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NRISVTE {
    public class DialogueManager : Singleton<DialogueManager> {
        #region members
        ObjectToPickUpManager _obj2pikUPMaNaGeR; // lol if any ever sees this
        ObjectToPickUpManager ObjectToPickUpManager_ {
            get {
                if (_obj2pikUPMaNaGeR == null) {
                    _obj2pikUPMaNaGeR = ObjectToPickUpManager.instance;
                }
                return _obj2pikUPMaNaGeR;
            }
        }
        public enum State {
            SayingQuestion,
            WaitingForResponse,
            SayingAnswer,
            None
        }
        public State state = State.None;

        TextMeshProUGUI questionText, optionAText, optionBText;

        #endregion
        #region unity
        void Awake(){
            questionText = GetComponentInChildren<QuestionText>(true).GetComponent<TextMeshProUGUI>();
            questionText.enabled = false;
        }
        #endregion
        #region public
        public void UpdateQuestionText() {
            if (ErrorIfSelectedNull()) {
                return;
            }
            state = State.SayingQuestion;
            questionText.enabled = true;
            // play audio over time, will update the state update to wait until the audio is done
            questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.Question;
            state = State.WaitingForResponse;
        }
        public void ResetQuestionText(){
            questionText.text = "";
            questionText.enabled = false;
        }
        #endregion
        #region private
        bool ErrorIfSelectedNull() {
            bool isNull = ObjectToPickUpManager_.CurrentlyPickedUpObject == null;
            if (isNull) {
                Debug.LogError("Currently Selected Object is null");
            }
            return isNull;
        }
        #endregion
    }
}
