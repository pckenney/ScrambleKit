using ScrambleKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class TopDownMovementTests
{
	private GameObject go;
	private Transform transform;
	private Rigidbody2D rb;
	private TopDownMovement move;

	[UnityTest]
	public IEnumerator TestTopDownMovement()
	{
		setupMover();

		reset();
		move.moveDir = Vector2.up;
		yield return new WaitForFixedUpdate();
		Assert.Greater(transform.position.y, 0f);
		Assert.AreEqual(transform.position.x, 0f);

		reset();
		move.moveDir = Vector2.one;
		yield return new WaitForFixedUpdate();
		Assert.Greater(transform.position.y, 0f);
		Assert.Greater(transform.position.x, 0f);

		reset();
		move.moveDir = -1f * Vector2.one;
		yield return new WaitForFixedUpdate();
		Assert.Less(transform.position.y, 0f);
		Assert.Less(transform.position.x, 0f);
	}

	private void setupMover()
	{
		go = new GameObject();
		transform = go.transform;
		rb = go.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		rb.drag = 0f;
		move = go.AddComponent<TopDownMovement>();
	}

	private void reset()
	{
		rb.velocity = Vector2.zero;
		transform.position = Vector3.zero;
	}
}
