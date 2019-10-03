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
	public class InRomNodeTest
	{
		public string testFile = "test.json";

		[Test]
		public void SetAddress_FileCreated()
		{
			Assert.IsFalse(File.Exists(testFile));

			InRomNode node = new InRomNode();
			node.Address = testFile;

			Assert.IsTrue(File.Exists(testFile));
		}

		[Test]
		public void SetAddressWithModel_FileCreated()
		{
			Assert.IsFalse(File.Exists(testFile));

			InRomNode<TestClass> node = new InRomNode<TestClass>();
			node.Address = testFile;
			node.Value = new TestClass() {
				TestField1 = 5,
				TestField2 = "okay cai cay"
			};

			Console.WriteLine(File.ReadAllText(testFile));
			Assert.IsTrue(File.Exists(testFile));
		}


		[SetUp]
		public void SetUp()
		{
			
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete(testFile);
		}
	}
}
