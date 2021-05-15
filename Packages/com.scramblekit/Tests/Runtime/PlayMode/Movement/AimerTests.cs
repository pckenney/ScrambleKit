using ScrambleKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class AimerTests
{
	private GameObject go;
	private Transform transform;
	private Aimer aimer;

	[UnityTest]
	public IEnumerator TestAiming()
	{
		setupAimer();
		transform.position = Vector3.zero;

		aimer.aimingAt = Vector2.up;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 0f);

		aimer.aimingAt = (-1f * Vector2.one);
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 135f);

		aimer.aimingAt = Vector2.one;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 360-45f);

		aimer.aimingAt = Vector2.down;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 360-180f);

		transform.position = new Vector3(100, 100, 0);
		aimer.aimingAt = Vector2.one;
		yield return new WaitForFixedUpdate();
		Assert.AreEqual(transform.eulerAngles.z, 135f);
	}

	private void setupAimer()
	{
		go = new GameObject();
		transform = go.transform;
		aimer = go.AddComponent<Aimer>();
	}
}
