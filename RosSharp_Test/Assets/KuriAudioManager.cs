using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class KuriAudioManager : Singleton<KuriAudioManager> {
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
        AudioSource _src;
        AudioSource audioSRC {
            get {
                if (_src == null) {
                    _src = GetComponent<AudioSource>();
                }
                return _src;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public float PlayAudioClip(string acName) {
            AudioClip ac = ObjDialogueFileManager.GetAudioClip(acName);
            audioSRC.clip = ac;
            audioSRC.Play();
            return ac.length;
        }
        #endregion
        #region private
        #endregion
    }
}
