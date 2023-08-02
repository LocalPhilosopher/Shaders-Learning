using System;
using _Code.Sound;
using UnityEngine;

namespace _Code
{
    public class SoundObject : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.rolloffMode = AudioRolloffMode.Custom;
        }
        
        public bool IsFinished() 
        {
            if (audioSource.isPlaying)
                return false;
            return true;
        }
        
        public void Play(AudioClip clip, Transform parent, float volume, float distance)
        {
            gameObject.name = "_Sound " + clip.name;
            audioSource.spatialBlend = parent == null ? 0 : 1; 
            transform.parent = parent == null ? SoundManager.Instance.transform : parent;
            transform.localPosition = Vector3.zero;
            audioSource.clip = clip;
            audioSource.maxDistance = distance;
            audioSource.volume = volume;
            audioSource.Play();
        }
        
        
    }
}