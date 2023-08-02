using _Code;
using _Code.Sound;
using UnityEngine;

namespace Code.Interactive
{
    public class Door : InteractiveObject
    {
        public override void Interact()
        {
            Open();
        }

        void Open()
        {
            SoundManager.Instance.Play(sound);
            gameObject.SetActive(false);
            Debug.Log("Door open");
        }
    }
}