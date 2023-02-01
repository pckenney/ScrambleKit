using UnityEngine;

namespace ScrambleKit
{
    public class PlatformerInput : MonoBehaviour
    {

        private PlatformerMovement move;

        void Start()
        {
            move = GetComponent<PlatformerMovement>();
        }

        void Update()
        {
            int x = 0;
            int y = 0;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            {
                y = 1;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                y = -1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                x = 1;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1;
            }

            move.MoveInput = new Vector2Int(x, y);

            move.JumpHeld = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow));
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
                move.TriggerJump();

        }

    }
}
