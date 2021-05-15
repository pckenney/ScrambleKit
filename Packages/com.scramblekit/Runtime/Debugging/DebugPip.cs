using UnityEngine;

namespace ScrambleKit
{
    public class DebugPip : MonoBehaviour
    {
        public KeyCode clearKey = KeyCode.X;

        void Update()
        {
            if (Input.GetKeyDown(clearKey))
                Destroy(gameObject);
        }
    }
}
