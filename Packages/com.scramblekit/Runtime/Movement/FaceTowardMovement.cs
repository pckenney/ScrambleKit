using UnityEngine;

namespace ScrambleKit
{
    public class FaceTowardMovement : MonoBehaviour
    {
        private Rigidbody2D rb;

        void FixedUpdate()
        {
            AlignFacingWithVelocity();
        }

        private void AlignFacingWithVelocity()
        {
            transform.eulerAngles = new Vector3(0, 0, rb.velocity.AngleDirectionFaces());
        }

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}
	}
}
