using NUnit.Framework;
using ScrambleKit;
using System.Collections.Generic;
using UnityEngine;

public class ListExtensionsTests
{
    [Test]
    public void TestDrawFromAtRandomEmptyListReturnsNull()
    {
		var testList = TestList(0);
		Assert.IsNull(testList.DrawFromAtRandom());
	}

	[Test]
	public void TestDrawFromAtRandomEmptyListOfIntReturnsZero()
	{
		List<int> intList = new List<int>();
		int draw = intList.DrawFromAtRandom();
		Assert.NotNull(draw);
		Assert.AreEqual(draw, 0);
	}

	[Test]
	public void TestDrawFromAtRandomEmptyListOfStringReturnsNull()
	{
		List<string> stringList = new List<string>();
		string draw = stringList.DrawFromAtRandom();
		Assert.IsNull(draw);
		Assert.IsTrue(string.IsNullOrEmpty(draw));
	}

	[Test]
	public void TestDrawFromAtRandomSingleItem()
	{
		var testList = TestList(1);
		TestObject draw = testList.DrawFromAtRandom();
		Assert.NotNull(draw);
		Assert.AreEqual(draw.value, 0);
		Assert.IsTrue(testList.Contains(draw));
	}

	[Test]
	public void TestDrawFromAtRandomThreeItemsAllChosen()
	{
		var testList = TestList(3);

		Random.InitState(42);  // forces RNG to return: 1, 0, 2 for this list size
		Assert.AreEqual(testList.DrawFromAtRandom().value, 1);
		Assert.AreEqual(testList.DrawFromAtRandom().value, 0);
		Assert.AreEqual(testList.DrawFromAtRandom().value, 2);
	}

	[Test]
	public void TestRemoveFromAtRandomSingleItemLeavesEmptyList()
	{
		var testList = TestList(1);
		TestObject draw = testList.RemoveFromAtRandom();
		Assert.NotNull(draw);
		Assert.AreEqual(draw.value, 0);
		Assert.IsFalse(testList.Contains(draw));
		Assert.AreEqual(testList.Count, 0);
	}

	[Test]
	public void TestRemoveFromAtRandomRemovesItem()
	{
		var testList = TestList(3);

		Random.InitState(42);  // forces RNG to return: 1, 0, 2 for this list size
		TestObject draw = testList.RemoveFromAtRandom();
		Assert.NotNull(draw);
		Assert.AreEqual(draw.value, 1);
		Assert.IsFalse(testList.Contains(draw));
		Assert.AreEqual(testList.Count, 2);
	}

	[Test]
	public void TestDrawFromAtRandomByWeight()
	{
		var testList = TestList(3);

		Random.InitState(42); // forces the RNG as desicrbed in comment after each line

		// expected results:
		// item 0 has 0 weight, never chosen
		// item 1 has 1 weight, chosen for RNG values (0-1)
		// item 2 has 2 weight, chosen for RNG values (1-3)
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 2); // RNG 2.997427 
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 2); // RNG 1.746737
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 1); // RNG 0.9709699
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 2); // RNG 1.725897
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 2); // RNG 2.122947
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => o.value).value, 2); // RNG 1.07259
	}

	[Test]
	public void TestDrawFromAtRandomByWeightZeroWeightsAlwaysReturnsFirstItem()
	{
		var testList = TestList(3);
		Random.InitState(42);

		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
		Assert.AreEqual(testList.DrawFromAtRandomByWeight(o => 0f).value, 0);
	}

	[Test]
	public void TestDrawFromAtRandomByWeightEmptyList()
	{
		var testList = TestList(0);
		Assert.IsNull(testList.DrawFromAtRandomByWeight(o => o.value));
	}

	private class TestObject
	{
		public int value;
	}

	private List<TestObject> TestList(int size)
	{
		var testList = new List<TestObject>();
		for(int i=0; i<size; i++)
		{
			testList.Add(new TestObject { value = i });
		}
		return testList;
	}
}
