using UnityEngine;

namespace ScrambleKit
{
    public class PlatformerMovement : MonoBehaviour
    {
        public FXTrigger JumpFX;

        public float JumpVelocity = 9f;
        public float JumpFloatForce = 5f;
        public float JumpSquashMax = 3f;

        public float JumpFloatTime = 0.4f;
        public float JumpAirControl = 1f;
        public float WalkingSpeed = 6f;
        public float WalkingAccel = 40f;

        public float CoyoteTime = 0.115f;
        public float PreJumpTime = 0.2f;

        public LayerMask WalkOnLayers = ~0; // "Everything"
        public float groundProbeWidth = 0.25f;

        // INPUTS, to be assigned by player input, or AI
        public Vector2Int MoveInput { get; set; }
        public bool JumpHeld;

        public void TriggerJump()
        {
            lastJumpTriggerTime = Time.fixedTime;
        }

        public string DEBUG;

        public enum MoveState
        {
            Idle,
            Walk,
            Jumping,
            Falling
        }
        public MoveState state = MoveState.Idle;
        private float lastSwitchTime;
        private float jumpUntilHeight;
        private float lastJumpTime;
        private float coyoteSince;
        private float lastJumpTriggerTime;

        private Rigidbody2D r2;
        private Animator anim;
        private SpriteRenderer rend;

        void Start()
        {
            r2 = gameObject.GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            rend = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            // YOU MAY NEED TO CUT INPUTS IN CERTAIN GAMESTATES HERE
			// eg I often disable player movement during a gameover display

            if (MoveInput.x > 0)
                rend.flipX = false;
            else if (MoveInput.x < 0)
                rend.flipX = true;

            bool grounded = IsGrounded();
            if (grounded)
            {
                coyoteSince = Time.fixedTime;
            }

            switch (state)
            {
                case MoveState.Idle:
                    if (!grounded)
                        EnterState(MoveState.Falling);
                    else if (MoveInput.x != 0)
                    {
                        EnterState(MoveState.Walk);
                    }
                    else if (JumpTriggeredRecently() && DidntJustJump())
                    {
                        EnterState(MoveState.Jumping);
                    }
                    else
                    {
                        StopWalking();
                    }
                    break;

                case MoveState.Jumping:
                    if (!JumpHeld || JumpFloatTimeOver() || r2.velocity.y < -0.1f)
                    {
                        EnterState(MoveState.Falling);
                    }
                    else
                    {
                        ApplyJumpFloat();
                        ApplyAirControl();
                    }
                    break;

                case MoveState.Falling:
                    if (grounded)
                    {
                        EnterState(MoveState.Idle);
                    }
                    else if (StillInCoyoteTime() && JumpTriggeredRecently() && DidntJustJump())  // Coyote jump
                    {
                        EnterState(MoveState.Jumping);
                    }
                    else
                    {
                        ApplyAirControl();
                    }
                    break;

                case MoveState.Walk:
                    if (!grounded)
                        EnterState(MoveState.Falling);
                    else if (JumpTriggeredRecently())
                        EnterState(MoveState.Jumping);
                    else if (MoveInput.x == 0)
                        EnterState(MoveState.Idle);
                    else
                    {
                        Walk();
                    }
                    break;
            }
        }

        private void EnterState(MoveState newState)
        {
            lastSwitchTime = Time.fixedTime;
            MoveState oldState = state;
            state = newState;

            switch (state)
            {
                case MoveState.Idle:
                    if (anim)
                    {
                        anim.SetTrigger("Idle");
                    }
                    break;
                case MoveState.Jumping:
                    if (anim)
                    {
                        anim.SetTrigger("Jump");
                    }
                    JumpFX.Trigger(transform);
                    ResetJumpTriggerAndCoyoteTime();
                    DoJump();
                    break;
                case MoveState.Falling:
                    if (anim)
                    {
                        anim.SetTrigger("Fall");
                    }
                    SquashJump();
                    break;
                case MoveState.Walk:
                    if (anim)
                    {
                        anim.SetTrigger("Run");
                    }
                    break;
            }
        }

        private void DoJump()
        {
            float cancelNegative = Mathf.Max(0, -r2.velocity.y);  // So Coyote time gives a full jump when already falling

            Vector2 accel = new Vector2(0, JumpVelocity + cancelNegative);
            Vector2 force = r2.mass * accel / Time.fixedDeltaTime;
            r2.AddForce(force);
            lastJumpTime = Time.fixedTime;
        }

        private void ApplyJumpFloat()
        {
            Vector2 accel = new Vector2(0, JumpFloatForce);
            Vector2 force = r2.mass * accel;
            r2.AddForce(force);
        }

        private void SquashJump()
        {
            float squash = Mathf.Min(0, JumpSquashMax - r2.velocity.y);

            Vector2 accel = new Vector2(0, squash);
            Vector2 force = r2.mass * accel / Time.fixedDeltaTime;
            r2.AddForce(force);
        }

        private void ResetJumpTriggerAndCoyoteTime()
        {
            lastJumpTriggerTime = -999f;
            coyoteSince = -999f;
        }

        private bool JumpTriggeredRecently()
        {
            return (Time.fixedTime - lastJumpTriggerTime) < PreJumpTime;
        }

        private bool DidntJustJump()
        {
            return (Time.fixedTime - lastJumpTime) > (CoyoteTime + 0.02f);
        }

        private bool StillInCoyoteTime()
        {
            return (Time.fixedTime - coyoteSince) < CoyoteTime;
        }

        private bool JumpFloatTimeOver()
        {
            return (Time.fixedTime - lastJumpTime) > JumpFloatTime;
        }

        private void ApplyAirControl()
        {
            int dir = MoveInput.x;
            float stopFactor = (dir == 0 || Mathf.Sign(dir) != Mathf.Sign(r2.velocity.x)) ? 1.25f : 1;
            Vector2 targetVel = new Vector2(WalkingSpeed * dir, r2.velocity.y);
            r2.AccelTowardVelocityByAtMost(targetVel, WalkingAccel * JumpAirControl * stopFactor);
        }

        private void Walk()
        {
            int dir = MoveInput.x;
            float stopFactor = (dir == 0 || Mathf.Sign(dir) != Mathf.Sign(r2.velocity.x)) ? 2 : 1;
            Vector2 targetVel = new Vector2(WalkingSpeed * dir, r2.velocity.y);
            r2.AccelTowardVelocityByAtMost(targetVel, WalkingAccel * JumpAirControl * stopFactor);
        }

        private void StopWalking()
        {
            Vector2 accel = new Vector2(-r2.velocity.x, 0);
            Vector2 force = r2.mass * accel / Time.fixedDeltaTime;
            r2.AddForce(force);
        }

        private bool IsGrounded()
        {
            float len = 0.8f;

            Vector2 ctr = transform.position;
            Vector2 left = ctr + new Vector2(-groundProbeWidth, 0);
            Vector2 right = ctr + new Vector2(groundProbeWidth, 0);

            RaycastHit2D res1 = Physics2D.Raycast(left, -Vector2.up, len, WalkOnLayers);
            RaycastHit2D res2 = Physics2D.Raycast(ctr, -Vector2.up, len, WalkOnLayers);
            RaycastHit2D res3 = Physics2D.Raycast(right, -Vector2.up, len, WalkOnLayers);

            Debug.DrawLine(left, left - Vector2.up * len);
            Debug.DrawLine(ctr, ctr - Vector2.up * len);
            Debug.DrawLine(right, right - Vector2.up * len);

            return (res1.collider != null) || (res2.collider != null) || (res3.collider != null);
        }

    }
}
