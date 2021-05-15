using NUnit.Framework;
using ScrambleKit;
using UnityEngine;

public class Vector2ExtensionsTests
{
	// It's janky that cases have multiple right answers, like -180 or 180
	// I have tried to have these tests document what you can expect as well as verify behavior

	[Test]
	public void TestAngleDirectionFaces()
	{
		// right-hand side of circle (x>=0) gives negative angles
		Assert.AreEqual(Vector2.up.AngleDirectionFaces(), 0f);
		Assert.AreEqual(Vector2.one.AngleDirectionFaces(), -45f);
		Assert.AreEqual(Vector2.right.AngleDirectionFaces(), -90f);
		Assert.AreEqual(Vector2.down.AngleDirectionFaces(), -180f);

		// left-hand side of circle (x<0) gives positive angles
		Assert.AreEqual((-1f*Vector2.one).AngleDirectionFaces(), 135);
		Assert.AreEqual(Vector2.left.AngleDirectionFaces(), 90f);
	}

	[Test]
	public void TestAngleTowardPosition()
	{
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.zero), 0f);
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.up), 0f);
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.one), -45f);
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.right), -90f);
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.down), -180f);
		Assert.AreEqual(Vector2.zero.AngleTowardPosition(Vector2.left), 90f);
		Assert.AreEqual(new Vector2(100, -100).AngleTowardPosition(new Vector2(150, -50)), -45f);
	}

	[Test]
	public void TestAngleBetweenDirections()
	{
		// Look at the smallest distance: if moving clockwise, angle is negative, CCW is positive
		Assert.AreEqual(Vector2.up.AngleBetweenDirections(Vector2.one), -45f);
		Assert.AreEqual(Vector2.one.AngleBetweenDirections(Vector2.up), 45f);

		Assert.AreEqual(Vector2.up.AngleBetweenDirections(Vector2.right), -90f);
		Assert.AreEqual(Vector2.right.AngleBetweenDirections(Vector2.up), 90f);

		Assert.AreEqual(Vector2.right.AngleBetweenDirections(Vector2.down), -90f);
		Assert.AreEqual(Vector2.down.AngleBetweenDirections(Vector2.right), 90f);

		Assert.AreEqual(Vector2.down.AngleBetweenDirections(Vector2.left), -90f);
		Assert.AreEqual(Vector2.left.AngleBetweenDirections(Vector2.down), 90f);

		Assert.AreEqual(Vector2.left.AngleBetweenDirections(Vector2.up), -90f);
		Assert.AreEqual(Vector2.up.AngleBetweenDirections(Vector2.left), 90f);

		// For angles exactly 180 away, there is no "shortest" and so we can't tell CW vs CCW
		// In this case, we default to CW and return -180, since Vector2.down.AngleFacing() = -180
		Assert.AreEqual(Vector2.one.AngleBetweenDirections((-1f * Vector2.one)), -180f);
		Assert.AreEqual((-1f * Vector2.one).AngleBetweenDirections(Vector2.one), -180f);

		Assert.AreEqual(Vector2.up.AngleBetweenDirections(Vector2.down), -180f);
		Assert.AreEqual(Vector2.down.AngleBetweenDirections(Vector2.up), -180f);

		// No movement is zero
		Assert.AreEqual(Vector2.one.AngleBetweenDirections(Vector2.one), 0f);
	}
}
