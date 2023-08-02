using _Code;
using _Code.Sound;
using UnityEngine;

namespace Code.Interactive
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected SoundData sound;
        [SerializeField] private Sprite interactiveIcon;
        public Sprite InteractiveIcon => interactiveIcon;
        public abstract void Interact();
    }
}