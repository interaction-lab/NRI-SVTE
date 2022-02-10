using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    public class ResourcePathConstants : MonoBehaviour {
        //Prefabs
        public static string PrefabFolder = "Prefabs/";

        public static string PictureFrame = PrefabFolder + "PictureFrame";
        public static string PictureObject = PrefabFolder + "PictureObject";

        // Materials
        public static string MaterialFolder = "Materials/";
        //public static string OutlineSnapColliderMaterial = MaterialFolder + "OutlineSnapCollider";

        // Audio
        public static string AudioFolder = "Audio/";
        public static string CorrectSound = AudioFolder + "correct";
        public static string IncorrectSound = AudioFolder + "incorrect";
        public static string PoofSound = AudioFolder + "poof";
        public static string PopSound = AudioFolder + "pop";
        public static string SnapSound = AudioFolder + "snap";
        public static string SpawnSound = AudioFolder + "spawn";
        public static string ComputerNoises = AudioFolder + "ComputerNoises";

        public static string SpeechFolder = AudioFolder + "PreloadSpeech/";
        public static string SpeechCacheFolder = AudioFolder + "CacheSpeech/";

        public static string CongratulationPhrases = SpeechFolder + "preloadCongratulation";
        public static string EncouragementPhrases = SpeechFolder + "preloadEncouragement";
        public static string CachePhrases = SpeechCacheFolder + "cachePhrase";

        public static string KuriSoundFolder = AudioFolder + "KuriAudio/";
        public static string KuriPositiveSoundFolder = KuriSoundFolder + "Positive/";
        public static string KuriNegativeSoundFolder = KuriSoundFolder + "Negative/";

        public static string KuriILoveYouSound = KuriPositiveSoundFolder + "i_love_you";
        public static string KuriGreetingSound = KuriPositiveSoundFolder + "Greeting";
        public static string KuriYippeSound = KuriPositiveSoundFolder + "S81_YIPPEEE_2";

        public static string KuriBangDownSound = KuriNegativeSoundFolder + "BANG_GOINGDOWN";
        public static string KuriFart = KuriNegativeSoundFolder + "Fart";
        public static string KuriPonderSad = KuriNegativeSoundFolder + "PONDER_SAD";

        // Navigation
        public static string NavigationFolder = PrefabFolder + "Navigation/";
        public static string NavGoalSphere = NavigationFolder + "NavGoalSphere";
    }
}
