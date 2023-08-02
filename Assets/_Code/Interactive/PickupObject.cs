using System;
using _Code;
using _Code.Sound;
using UnityEngine;

namespace Code.Interactive
{
    public class PickupObject : MonoBehaviour
    {
        public event System.Action OnPickuped;

        [SerializeField] private SoundData sound;
        [SerializeField] private ParticleSystem fadeEffect;

        private void Awake()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            SoundManager.Instance.Play(sound);
            Instantiate(fadeEffect, transform.position, Quaternion.identity);
            OnPickuped?.Invoke();
            Destroy(gameObject);
        }
    }
}