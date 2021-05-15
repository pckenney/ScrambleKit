using ScrambleKit;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Rigidbody2DExtensionsTests
{
	private static int frameCount = 5;  // duration of test in physics frames

	private GameObject go;
	private Transform transform;
	private Rigidbody2D rb;

    [UnityTest]
    public IEnumerator TestZeroTargetSpeed()
    {
		setupBody(Vector2.zero, 9999f);

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity, Vector2.zero);
    }

	[UnityTest]
	public IEnumerator TestZeroAcceleration()
	{
		setupBody(Vector2.up * 9999f, 0f);

		for(int i=0;i<frameCount;i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity, Vector2.zero);
	}

	[UnityTest]
	public IEnumerator TestNegativeAccelerationIsZero()
	{
		setupBody(Vector2.up * 9999f, -10f);

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity, Vector2.zero);
	}

	[UnityTest]
	public IEnumerator TestLimitedVerticalAcceleration()
	{
		setupBody(Vector2.up * 9999f, 10f);

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();
		
		Assert.AreEqual(rb.velocity.magnitude, 10f * frameCount * Time.fixedDeltaTime, 0.001f);
	}

	[UnityTest]
	public IEnumerator TestLimitedDiagonalAcceleration()
	{
		setupBody(Vector2.one * 9999f, 10f);

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity.magnitude, 10f * frameCount * Time.fixedDeltaTime, 0.001f);
	}

	[UnityTest]
	public IEnumerator TestLimitedHorizontalAcceleration()
	{
		setupBody(Vector2.left * 9999f, 10f);

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity.magnitude, 10f * frameCount * Time.fixedDeltaTime, 0.001f);
	}

	[UnityTest]
	public IEnumerator TestAccelerationCappedAtTarget()
	{
		setupBody(Vector2.up * 10, 9999f / (frameCount * Time.fixedDeltaTime));

		for (int i = 0; i < frameCount; i++)
			yield return new WaitForFixedUpdate();

		Assert.AreEqual(rb.velocity.magnitude, 10f, 0.001f);
	}

	private void setupBody(Vector2 targetVelocity, float maxAccelPerSec)
	{
		go = new GameObject();
		transform = go.transform;
		rb = go.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		rb.drag = 0f;

		ConstantAcceleration accel = go.AddComponent<ConstantAcceleration>();
		accel.MaxAccelPerSec = maxAccelPerSec;
		accel.TargetVelocity = targetVelocity;
	}

	private class ConstantAcceleration : MonoBehaviour
	{
		public Vector2 TargetVelocity;
		public float MaxAccelPerSec;

		private Rigidbody2D rb;
		void Start()
		{
			rb = transform.GetComponent<Rigidbody2D>();	
		}

		void FixedUpdate()
		{
			rb.AccelTowardVelocityByAtMost(TargetVelocity, MaxAccelPerSec);
		}
	}
}
