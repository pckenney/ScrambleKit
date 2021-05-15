using ScrambleKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class FaceTowardMovementTests
{
	private GameObject go;
	private Transform transform;
	private Rigidbody2D rb;

	[UnityTest]
	public IEnumerator TestFacingTowardMovement()
	{
		setupFacer();

		rb.velocity = Vector2.up;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 0f);

		rb.velocity = (-1f * Vector2.one);
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 135f);

		rb.velocity = Vector2.left;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 90f);

		// Although the Vector extensions in ScrambleKit return negative rotations,
		// reads on the transform euler are positive, so I'm adding 360 explicity
		rb.velocity = Vector2.one;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 360-45f);

		rb.velocity = Vector2.right;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 360-90f);

		rb.velocity = Vector2.down;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 360-180f);
	}

	private void setupFacer()
	{
		go = new GameObject();
		transform = go.transform;
		rb = go.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		rb.drag = 0f;
		go.AddComponent<FaceTowardMovement>();
	}
}
