using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

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
            SayingResponse,
            SayingOptions,
            None
        }
        public State state = State.None;

        TextMeshProUGUI questionText, optionAText, optionBText;
        Button optionAButton, optionBButton;
        UserOptionsButtonObject userOptionButtonObject;
        public enum Option {
            A,
            B,
            None
        }
        public Option CurrentlySelectedOption = Option.None;
        UnityEvent OnUserOptionSelected;
        KuriBTEventRouter _eventRouter;
        KuriBTEventRouter eventRouter {
            get {
                if (_eventRouter == null) {
                    _eventRouter = KuriManager.instance.GetComponent<KuriBTEventRouter>();
                }
                return _eventRouter;
            }
        }

        #endregion
        #region unity
        void Awake() {
            questionText = GetComponentInChildren<QuestionText>(true).GetComponent<TextMeshProUGUI>();
            userOptionButtonObject = GetComponentInChildren<UserOptionsButtonObject>(true);
            optionAButton = userOptionButtonObject.OptionAButton;
            optionBButton = userOptionButtonObject.OptionBButton;
            optionAText = userOptionButtonObject.OptionAText;
            optionBText = userOptionButtonObject.OptionBText;
            questionText.enabled = false;
            DisableOptionsObj();
            OnUserOptionSelected = new UnityEvent();
            eventRouter.AddEvent(EventNames.OnUserOptionSelected, OnUserOptionSelected);
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
            state = State.None;
        }
        public void ResetQuestionText() {
            questionText.text = "";
            questionText.enabled = false;
        }
        public void EnableOptions() {
            if (ErrorIfSelectedNull()) {
                return;
            }
            state = State.SayingOptions;
            EnableOptionsObj();
            UpdateOptionAText(ObjectToPickUpManager_.CurrentlyPickedUpObject.OptionA);
            UpdateOptionBText(ObjectToPickUpManager_.CurrentlyPickedUpObject.OptionB);
            state = State.WaitingForResponse;
        }
        public void ClickedOption(Option option) {
            if (ErrorIfSelectedNull()) {
                return;
            }
            CurrentlySelectedOption = option;
            OnUserOptionSelected.Invoke();
            state = State.None;
        }

        public void UpdateResponseText() {
            if (ErrorIfSelectedNull()) {
                return;
            }
            DisableOptionsObj();
            state = State.SayingResponse;
            DisableOptionsObj();
            if (CurrentlySelectedOption == Option.A) {
                questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseA;
            }
            else if (CurrentlySelectedOption == Option.B) {
                questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseB;
            }
            // play audio over time, will update the state update to wait until the audio is done
            state = State.None;
        }

        #endregion
        #region private
        void DisableOptionsObj() {
            userOptionButtonObject.gameObject.SetActive(false);
        }
        void EnableOptionsObj() {
            userOptionButtonObject.gameObject.SetActive(true);
        }
        private void UpdateOptionAText(string text) {
            optionAText.text = text;
        }
        private void UpdateOptionBText(string text) {
            optionBText.text = text;
        }
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
