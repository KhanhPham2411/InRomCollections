using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomNodeTest : InRomEntryTest
	{
		protected string _testFolder = "test";

		[Test]
		public void Load_ShouldReturnCorrectly()
		{
			var nodeExpected = CreateNodeTest();
			Console.WriteLine(File.ReadAllText(nodeExpected.Address));

			var nodeActual = InRomNode<TestClass>.Load(nodeExpected.Address);

			Assert.IsTrue(nodeActual == nodeExpected);
		}

		[Test]
		public void InsertNext_ShouldReturnCorrectly()
		{
			var node1 = CreateNodeTest();
			var node2 = CreateNodeTest();
			node1.InsertNext(node2);

			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
		}

		[Test]
		public void InsertNextBetween_ShouldReturnCorrectly()
		{
			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			var node3 = CreateNodeTest("node3");

			node1.InsertNext(node3);
			node1.InsertNext(node2);

			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.NextNodeAdress, node3.Address);

			node3 = InRomNode<TestClass>.Load(node3.Address); // refresh node 3
			Assert.AreEqual(node3.PreviousNodeAddress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
		}

		[Test]
		public void InsertPrevious_ShouldReturnCorrectly()
		{
			var node1 = CreateNodeTest();
			var node2 = CreateNodeTest();
			node2.InsertPrevious(node1);

			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
		}

		[Test]
		public void InsertPreviousBetween_ShouldReturnCorrectly()
		{
			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			var node3 = CreateNodeTest("node3");

			node3.InsertPrevious(node1);
			node3.InsertPrevious(node2);

			node1 = InRomNode<TestClass>.Load(node1.Address); // refresh node 1
			node2 = InRomNode<TestClass>.Load(node2.Address); // refresh node 3

			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.NextNodeAdress, node3.Address);
			Assert.AreEqual(node3.PreviousNodeAddress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
		}


		public InRomNode<TestClass> CreateNodeTest()
		{
			return CreateNodeTest(Guid.NewGuid().ToString());
		}
		public InRomNode<TestClass> CreateNodeTest(string name)
		{
			var node = new InRomNode<TestClass>();
			node.Address = _testFolder + "/" + name;
			node.Value = new TestClass()
			{
				TestField1 = 5,
				TestField2 = "okay cai cay"
			};

			return node;
		}

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(_testFolder);
		}

		[TearDown]
		public override void TearDown()
		{
			Directory.Delete(_testFolder, true);
		}
	}
}
