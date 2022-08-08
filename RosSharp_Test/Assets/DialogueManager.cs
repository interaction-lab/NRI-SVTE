using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace NRISVTE {
    public class DialogueManager : Singleton<DialogueManager> {
        #region members
        KuriAudioManager _kuriAudioManager;
        KuriAudioManager kuriAudioManager {
            get {
                if (_kuriAudioManager == null) {
                    _kuriAudioManager = KuriAudioManager.instance;
                }
                return _kuriAudioManager;
            }
        }
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
            ResetAllUI();
            OnUserOptionSelected = new UnityEvent();
            eventRouter.AddEvent(EventNames.OnUserOptionSelected, OnUserOptionSelected);
            SetUpHoverOptionEvents();
        }
        #endregion
        #region public
        public void ResetAllUI() {
            questionText.enabled = false;
            DisableOptionsObj();
        }
        public void UpdateQuestionText() {
            if (ErrorIfSelectedNull()) {
                return;
            }
            state = State.SayingQuestion;
            questionText.enabled = true;
            // play audio over time, will update the state update to wait until the audio is done
            questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.Question;
            float timeToStall = kuriAudioManager.PlayAudioClip(ObjectToPickUpManager_.CurrentlyPickedUpObject.QuestionAudioClipName);
            StartCoroutine(WaitForAudioToFinish(timeToStall));
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
            if (AudioIsPlaying()) {
                kuriAudioManager.StopAudio();
            }
            DisableOptionsObj();
            state = State.SayingResponse;
            DisableOptionsObj();
            if (CurrentlySelectedOption == Option.A) {
                questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseA;
                float timeToStall = kuriAudioManager.PlayAudioClip(ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseAAudioClipName);
                StartCoroutine(WaitForAudioToFinish(timeToStall));
            }
            else if (CurrentlySelectedOption == Option.B) {
                questionText.text = ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseB;
                float timeToStall = kuriAudioManager.PlayAudioClip(ObjectToPickUpManager_.CurrentlyPickedUpObject.ResponseBAudioClipName);
                StartCoroutine(WaitForAudioToFinish(timeToStall));
            }
        }

        #endregion
        #region private
        IEnumerator WaitForAudioToFinish(float timeToStall) {
            yield return new WaitForSeconds(timeToStall);
            state = State.None;
        }
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

        bool AudioIsPlaying() {
            return kuriAudioManager.IsPlaying;
        }
        void AHoverEnter() {
            if (AudioIsPlaying()) {
                kuriAudioManager.StopAudio();
            }
            kuriAudioManager.PlayAudioClip(ObjectToPickUpManager_.CurrentlyPickedUpObject.OptionAAudioClipName);
        }
        void AHoverExit() {
            if (AudioIsPlaying()) {
                kuriAudioManager.StopAudio();
            }
        }
        void BHoverEnter() {
            if (AudioIsPlaying()) {
                kuriAudioManager.StopAudio();
            }
            kuriAudioManager.PlayAudioClip(ObjectToPickUpManager_.CurrentlyPickedUpObject.OptionBAudioClipName);
        }
        void BHoverExit() {
            if (AudioIsPlaying()) {
                kuriAudioManager.StopAudio();
            }
        }
        void SetUpHoverOptionEvents() {
            OnHoverButton hoverA = optionAButton.GetComponent<OnHoverButton>();
            hoverA.OnHoverEnter.AddListener(AHoverEnter);
            hoverA.OnHoverExit.AddListener(AHoverExit);
            OnHoverButton hoverB = optionBButton.GetComponent<OnHoverButton>();
            hoverB.OnHoverEnter.AddListener(BHoverEnter);
            hoverB.OnHoverExit.AddListener(BHoverExit);
        }
        #endregion
    }
}
