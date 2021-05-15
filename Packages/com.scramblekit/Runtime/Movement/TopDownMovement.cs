using UnityEngine;

namespace ScrambleKit
{
	public class TopDownMovement : MonoBehaviour
	{
		public float speedLimit = 7;
		public float moveAcceleration = 25;
		public float stopDeceleration = 40;
		public bool flipXScaleMovingLeft = true;

		[HideInInspector]
		public Vector2 moveDir;

		private Rigidbody2D rb;
		private Vector3 origScale;

		void FixedUpdate()
		{
			if (moveDir != Vector2.zero)
				Move();
			else
				Stop();
		}

		private void Move()
		{
			if (ReversingDirection())
			{
				Stop(); // moving opposite current velocity should skid to a halt using stop decel
			}
			else
			{
				rb.AccelTowardVelocityByAtMost(speedLimit * moveDir, moveAcceleration);
				UpdateFacing();
			}
		}

		private void Stop()
		{
			rb.AccelTowardVelocityByAtMost(Vector2.zero, stopDeceleration);
		}

		private bool ReversingDirection()
		{
			return rb.velocity.magnitude > 0.05f * speedLimit
				&& Vector2.Dot(moveDir.normalized, rb.velocity.normalized) < -0.8f;
		}

		private void UpdateFacing()
		{
			if (moveDir.x == 0 || !flipXScaleMovingLeft)
				return;

			int facing = moveDir.x < 0 ? -1 : 1;
			transform.localScale = new Vector3(origScale.x * facing, origScale.y, origScale.z);
		}

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			origScale = transform.localScale;
		}
	}
}