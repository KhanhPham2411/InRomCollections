using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomListTest
	{
		protected string _testFolder = "test";

		[Test]
		public void Add_ShouldRunCorrectly()
		{
			InRomList list = new InRomList(_testFolder + "/list");

			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			list.Add(node1);
			list.Add(node2);

			Assert.AreEqual(list.FirstNodeAddress, node1.Address);
			Assert.AreEqual(list.LastNodeAdress, node2.Address);
			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
		}

		[Test]
		public void RemoveBetween_ShouldRunCorrectly()
		{
			InRomList list = new InRomList(_testFolder + "/list");

			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			var node3 = CreateNodeTest("node3");
			list.Add(node1);
			list.Add(node3);
			list.Add(node2);

			list.Remove(node3);

			Assert.AreEqual(list.FirstNodeAddress, node1.Address);
			Assert.AreEqual(list.LastNodeAdress, node2.Address);
			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
			Assert.IsFalse(File.Exists(node3.Address));
		}
		[Test]
		public void RemoveFirst_ShouldRunCorrectly()
		{
			InRomList list = new InRomList(_testFolder + "/list");
			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			var node3 = CreateNodeTest("node3");
			list.Add(node3);
			list.Add(node1);
			list.Add(node2);

			list.Remove(list.FirstNode());

			Assert.AreEqual(list.FirstNodeAddress, node1.Address);
			Assert.AreEqual(list.LastNodeAdress, node2.Address);
			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
			Assert.IsFalse(File.Exists(node3.Address));
		}
		[Test]
		public void RemoveLast_ShouldRunCorrectly()
		{
			InRomList list = new InRomList(_testFolder + "/list");
			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			var node3 = CreateNodeTest("node3");
			list.Add(node1);
			list.Add(node2);
			list.Add(node3);

			list.Remove(list.LastNode());

			Assert.AreEqual(list.FirstNodeAddress, node1.Address);
			Assert.AreEqual(list.LastNodeAdress, node2.Address);
			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
			Assert.IsFalse(File.Exists(node3.Address));
		}

		public InRomNode<TestClass> CreateNodeTest()
		{
			return CreateNodeTest(Guid.NewGuid().ToString());
		}
		public InRomNode<TestClass> CreateNodeTest(string name)
		{
			var address = _testFolder + "/" + name;
			var node = new InRomNode<TestClass>(address);
			node.Value = new TestClass()
			{
				TestField1 = 5,
				TestField2 = "okay baby"
			};

			return node;
		}

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(_testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(_testFolder, true);
		}
	}
}
