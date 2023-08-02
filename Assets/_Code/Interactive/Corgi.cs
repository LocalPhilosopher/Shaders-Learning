using System;
// using Code.Physics;
using Code.Player;
using UnityEngine;

namespace Code.Interactive
{
    [RequireComponent(typeof(AudioSource))]
    public class Corgi : InteractiveObject
    {
        [SerializeField] private AudioClip clip;
        
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            audioSource.PlayOneShot(clip);
        }
    }
}