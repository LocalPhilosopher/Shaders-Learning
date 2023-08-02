using UnityEngine;

namespace _Code.Sound
{

    [CreateAssetMenu(fileName = "SoundData", menuName = "SoundData")]
    public class SoundData : ScriptableObject
    {
        [SerializeField] AudioClip[] clips;
        public float Distance;
        public float Volume = 1;
        
        public AudioClip Clip => clips[Random.Range(0, clips.Length)];
    }
}