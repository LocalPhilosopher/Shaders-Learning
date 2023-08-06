using UnityEngine;

namespace _Code.ShadersLogic
{
    public class DissolveLogic : MonoBehaviour
    {
        [SerializeField] private Renderer dissolveRenderer;
        private Material material;

        void Start()
        {
            material = dissolveRenderer.material;
        }

        void Update()
        {
            material.SetVector("_PlaneOrigin", transform.position);
            material.SetVector("_PlaneNormal", transform.up);
        }
    }
}