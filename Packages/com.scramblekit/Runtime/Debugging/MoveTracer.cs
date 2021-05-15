using UnityEngine;

namespace ScrambleKit
{
    public class MoveTracer : MonoBehaviour
    {
        public Transform pipPrefab;
        public KeyCode toggleKey = KeyCode.C;
        public bool startActive = true;

        private Rigidbody2D rb;
        private bool on;

        void FixedUpdate()
        {
            if (on && IsMoving())
                PlacePipAtCurrentLocation();
        }

		void Update()
		{
			if (Input.GetKeyDown(toggleKey))
				on = !on;
		}

		private void PlacePipAtCurrentLocation()
        {
            if (pipPrefab == null)
                return;

            Transform pip = Instantiate(pipPrefab);
            pip.position = transform.position;
        }

        private bool IsMoving()
        {
            return (rb == null || rb.velocity.magnitude > 0.001f);
        }

        void Start()
        {
            on = startActive;
            rb = GetComponent<Rigidbody2D>();
        }
    }
}
