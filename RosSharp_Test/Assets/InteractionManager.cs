using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class InteractionManager : Singleton<InteractionManager> {
        #region members
        LoggingManager _loggingManager;
        LoggingManager loggingManager {
            get {
                if (_loggingManager == null) {
                    _loggingManager = LoggingManager.instance;
                }
                return _loggingManager;
            }
        }
        public enum SampleTypes {
            active,
            random
        }

        float interactionLengthInMinutes = 5f;
        public SampleTypes CurrentSampleType;
        public static string sampleTypeLogColumn = "sampleType";
        #endregion
        #region unity
        private void Start() {
            loggingManager.AddLogColumn(sampleTypeLogColumn, CurrentSampleType.ToString());
            StartCoroutine(SwitchSampleTypeAtTime());
        }
        #endregion
        #region public
        #endregion
        #region private
        IEnumerator SwitchSampleTypeAtTime() {
            yield return new WaitForSeconds(interactionLengthInMinutes / 2);
            if (CurrentSampleType == SampleTypes.active) {
                CurrentSampleType = SampleTypes.random;
            }
            else {
                CurrentSampleType = SampleTypes.active;
            }
            loggingManager.UpdateLogColumn(sampleTypeLogColumn, CurrentSampleType.ToString());
        }
        #endregion
    }
}
