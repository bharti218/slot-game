using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotgame
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip reelStopSound;
        [SerializeField] private AudioSource audioSource;
        public static AudioManager Instance;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
        }

        public void PlayReelStopSound()
        {
            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(reelStopSound);
        }

    }
}