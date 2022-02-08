using System.Collections;
using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class DelayAudio : MonoBehaviour
    {
        private int startingDelay = 5;
        private AudioSource audioSource;
        private ParticleSystem soundWaves;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            soundWaves = FindObjectOfType<ParticleSystem>();
            StartCoroutine(playSoundWithDelay());
        }

        IEnumerator playSoundWithDelay()
        {
            yield return new WaitForSeconds(startingDelay);
            soundWaves.Play();
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            soundWaves.Stop();
        }
    }
}