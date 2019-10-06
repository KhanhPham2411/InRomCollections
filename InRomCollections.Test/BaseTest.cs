using InRomCollections.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class BaseTest
	{
		protected string _testFolder = "test";

		[SetUp]
		public void SetUp()
		{
			TryCleanFolder();
			Directory.CreateDirectory(_testFolder);
		}
		[TearDown]
		public void TearDown()
		{
			TryCleanFolder();
		}

		public void TryCleanFolder()
		{
			try
			{
				Directory.Delete(_testFolder, true);
			}
			catch { }
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
	}
}
