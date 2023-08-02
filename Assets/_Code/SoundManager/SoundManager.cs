using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

namespace _Code.Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        private List<SoundObject> soundObjects = new List<SoundObject>();

        public void Play(SoundData data, Transform parent = null)
        {
            if (data == null)
                return;
            AudioClip clip = data.Clip;
            float volume = data.Volume;
            float distance = data.Distance;
            
            foreach (var soundObject in soundObjects)
            {
                if (soundObject.IsFinished())
                {
                    soundObject.Play(clip, parent, volume, distance);
                    return;
                }
            }
            var go = new GameObject();
            go.name = "_Sound " + clip.name;
            var sound = go.AddComponent<SoundObject>();
            soundObjects.Add(sound);
            sound.Play(clip,parent, volume, distance);
        }
    }
}