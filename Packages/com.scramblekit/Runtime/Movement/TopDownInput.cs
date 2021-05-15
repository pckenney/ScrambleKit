using UnityEngine;

namespace ScrambleKit
{
	[RequireComponent(typeof(TopDownMovement))]
	public class TopDownInput : MonoBehaviour
	{
		private TopDownMovement move;

		void Update()
		{
			Vector2 input = Vector2.zero;
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
				input += Vector2.up;
			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				input += Vector2.down;

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
				input += Vector2.left;
			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				input += Vector2.right;

			move.moveDir = input.normalized;
		}

		void Start()
		{
			move = GetComponent<TopDownMovement>();
		}
	}
}
