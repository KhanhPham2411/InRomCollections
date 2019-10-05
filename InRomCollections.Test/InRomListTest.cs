using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomListTest : BaseTest
	{
		[Test]
		public void Add_ShouldRunCorrectly()
		{
			InRomList list = new InRomList(_testFolder + "/list");

			var node1 = CreateNodeTest("node1");
			var node2 = CreateNodeTest("node2");
			list.Add(node1);
			list.Add(node2);

			Assert.AreEqual(list.FirstNodeAddress, node1.Address);
			Assert.AreEqual(list.LastNodeAddress, node2.Address);
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
			Assert.AreEqual(list.LastNodeAddress, node2.Address);
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
			Assert.AreEqual(list.LastNodeAddress, node2.Address);
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
			Assert.AreEqual(list.LastNodeAddress, node2.Address);
			Assert.AreEqual(node1.NextNodeAdress, node2.Address);
			Assert.AreEqual(node2.PreviousNodeAddress, node1.Address);
			Assert.IsFalse(File.Exists(node3.Address));
		}
	}
}
