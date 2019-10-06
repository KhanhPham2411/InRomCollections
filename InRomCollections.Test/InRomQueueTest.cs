using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomQueueTest : BaseTest
	{

		[Test]
		public void EnqueueDequeue_ShoudlRunCorrectly()
		{
			var queue = new InRomQueue<TestClass>(_testFolder + "/list.json");

			var item1 = new TestClass()
			{
				Id = "item1",
				TestField1 = 111,
				TestField2 = "item1"
			};
			var item2 = new TestClass()
			{
				Id = "item2",
				TestField1 = 222,
				TestField2 = "item2"
			};
			var item3 = new TestClass()
			{
				Id = "item3",
				TestField1 = 333,
				TestField2 = "item3"
			};
			queue.Enqueue(item1);
			queue.Enqueue(item2);
			queue.Enqueue(item3);

			var item = queue.Dequeue();

			Assert.AreEqual(queue.Peek().TestField1, item2.TestField1);
			Assert.AreEqual(item.TestField1, item1.TestField1);
		}

		[Test]
		public void EnqueueRange_ShoudlRunCorrectly()
		{
			var queue = new InRomQueue<TestClass>(_testFolder + "/list.json");

			List<TestClass> list = new List<TestClass>();
			var item1 = new TestClass()
			{
				Id = "item1",
				TestField1 = 111,
				TestField2 = "item1"
			};
			var item2 = new TestClass()
			{
				Id = "item2",
				TestField1 = 222,
				TestField2 = "item2"
			};
			var item3 = new TestClass()
			{
				Id = "item3",
				TestField1 = 333,
				TestField2 = "item3"
			};
			list.Add(item1);
			list.Add(item2);
			list.Add(item3);
			list.Add(item1);

			queue.EnqueueRange(list);
			var item = queue.Dequeue();

			Assert.AreEqual(queue.Peek().TestField1, item2.TestField1);
			Assert.AreEqual(item.TestField1, item1.TestField1);
			Assert.AreEqual(queue.Last().TestField1, item3.TestField1);
		}

		[Test]
		public void EnqueueAsync_ShoudlRunCorrectly()
		{
			var queue = new InRomQueue<TestClass>(_testFolder + "/list.json");

			var item1 = new TestClass()
			{
				Id = "item1",
				TestField1 = 111,
				TestField2 = "item1"
			};
			var item2 = new TestClass()
			{
				Id = "item2",
				TestField1 = 222,
				TestField2 = "item2"
			};
			var item3 = new TestClass()
			{
				Id = "item3",
				TestField1 = 333,
				TestField2 = "item3"
			};
			queue.Enqueue(item1);
			queue.EnqueueAsync(item2);
			queue.EnqueueAsync(item3);

			Thread.Sleep(500);
			Assert.AreEqual(queue.Peek().TestField1, item1.TestField1);
		}
	}
}
