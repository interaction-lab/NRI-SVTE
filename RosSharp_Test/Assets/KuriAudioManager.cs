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

        public bool IsPlaying {
            get {
                return audioSRC.isPlaying;
            }
        }
        #endregion
        #region unity
        #endregion
        #region public
        public float PlayAudioClip(string acName) {
            // lowercase and remove spaces from audio clip name
            acName = acName.ToLower().Replace(" ", "");
            AudioClip ac = ObjDialogueFileManager.GetAudioClip(acName);
            if (ac != null) {
                audioSRC.clip = ac;
                audioSRC.Play();
                return ac.length;
            }
            return 0;
        }

        public void StopAudio() {
            audioSRC.Stop();
        }
        #endregion
        #region private
        #endregion
    }
}
