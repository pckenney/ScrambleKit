using UnityEngine;

namespace ScrambleKit
{
    public class HitStopFX : MonoBehaviour
    {
        private static float stopUntil;
        public static void HitStop(float time)
        {
            stopUntil = Time.realtimeSinceStartup + time;
        }

        void Update()
        {
            if (stopUntil > Time.realtimeSinceStartup)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1;
        }
    }
}
