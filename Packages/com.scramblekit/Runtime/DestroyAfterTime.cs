using UnityEngine;

namespace ScrambleKit.Lifecycle
{
    public class DestroyTimer : MonoBehaviour
    {
        public float lifetime;

        private float startTime;

        void Update()
		{
			if (TimeAlive() >= lifetime)
				Destroy(gameObject);
		}

		void Start()
		{
			startTime = Time.time;
		}

		private float TimeAlive()
		{
			return Time.time - startTime;
		}
	}
}
