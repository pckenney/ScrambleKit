using UnityEngine;

namespace ScrambleKit
{
    public static class Rigidbody2DExtensions
    {
        public static void AccelTowardVelocityByAtMost(this Rigidbody2D rb, Vector2 targetVelocity, float maxAccelPerSec)
        {
            Vector2 deltaVel = targetVelocity - rb.velocity;
            float changePossible = Mathf.Min(deltaVel.magnitude / Time.fixedDeltaTime, Mathf.Max(0,maxAccelPerSec));

            Vector2 accel = changePossible * deltaVel.normalized;
            Vector2 forceAccel = rb.mass * accel;
            rb.AddForce(forceAccel);
        }
    }
}
