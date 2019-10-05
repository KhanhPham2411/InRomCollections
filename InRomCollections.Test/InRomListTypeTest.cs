using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomListTypeTest : BaseTest
	{
		public InRomListTypeTest()
		{
			_testFolder = "G:\\temp";
		}

		[Test]
		public void Add_ShouldRunCorrectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

			var item1 = new TestClass()
			{
				TestField1 = 111,
				TestField2 = "item1"
			};
			var item2 = new TestClass()
			{
				TestField1 = 222,
				TestField2 = "item2"
			};
			list.Add("i1", item1);
			list.Add("i2", item2);

			Assert.AreEqual(list.First().TestField1, item1.TestField1);
			Assert.AreEqual(list.Last().TestField1, item2.TestField1);
		}
	}
}
