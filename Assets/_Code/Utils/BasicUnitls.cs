using UnityEngine;

namespace Code.Utils
{
    public class BasicUnitls
    {
        public static void ClampRotation(ref Vector3 deltaRotation)
        {
            if (deltaRotation.x > 180) deltaRotation.x = deltaRotation.x - 360;
            if (deltaRotation.x < -180) deltaRotation.x = 360 - deltaRotation.x;
            if (deltaRotation.y > 180) deltaRotation.y = deltaRotation.y - 360;
            if (deltaRotation.y < -180) deltaRotation.y = 360 - deltaRotation.y;
            if (deltaRotation.z > 180) deltaRotation.z = deltaRotation.z - 360;
            if (deltaRotation.z < -180) deltaRotation.z = 360 - deltaRotation.z;
            Quaternion.Euler(20,30,0);
        }
        static void EncapsulateBounds (ref Bounds bounds, Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<Collider>(out var collider))
                    bounds.Encapsulate(collider.bounds);
                EncapsulateBounds(ref bounds, child);
            }
        }
        
        public static Bounds GetBoundsTransform(Transform targetTransform)
        {
            Bounds bounds = new Bounds();
            EncapsulateBounds(ref bounds,  targetTransform);
            return bounds;
        }

        public static void UnlockFPS()
        {
            if (Application.isMobilePlatform)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 120;
            }
        }
    }
}