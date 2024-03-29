﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
	public class InRomListTypeTest : BaseTest
	{
		[Test]
		public void GetSetCurrent_ShouldRunCorectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

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

			Assert.AreEqual(list.Current.TestField1, item1.TestField1);

			list.Current = item3;
			Assert.AreNotEqual(list.Current.TestField1, item3.TestField1);

			list.Current = list.NextOf(list.Current);
			Assert.AreEqual(list.Current.TestField1, item2.TestField1);
		}

		[Test]
		public void Add_ShouldRunCorrectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

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

			Assert.AreEqual(list.First().TestField1, item1.TestField1);
			Assert.AreEqual(list.Last().TestField1, item3.TestField1);
		}

		[Test]
		public void Remove_ShouldRunCorrectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

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
			var item4 = new TestClass()
			{
				Id = "item4",
				TestField1 = 4444,
				TestField2 = "item4"
			};
			list.Add(item1);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			list.Remove(item4);
			list.Remove(item1);

			Assert.AreEqual(list.First().TestField1, item2.TestField1);
			Assert.AreEqual(list.Last().TestField1, item3.TestField1);
			Assert.AreEqual(list.NextOf(item2).TestField1, item3.TestField1);
			Assert.AreEqual(list.PreviousOf(item3).TestField1, item2.TestField1);
		}

		[Test]
		public void Get_ShouldRunCorrectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

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

			Assert.AreEqual(list.Get(item2.Id).TestField1, item2.TestField1);
		}

		[Test]
		public void Contains_ShouldRunCorrectly()
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

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
			list.Add(item3);

			Assert.IsTrue(list.Contains(item3));
			Assert.IsFalse(list.Contains(item2));
		}

		[TestCase("1234567", "12/34/56/7")]
		[TestCase("123456", "12/34/56")]
		[TestCase("12345", "12/34/5")]
		public void GetIdFolderTest(string input, string expected)
		{
			var list = new InRomList<TestClass>(_testFolder + "/list.json");

			var idFolder = list.GetIdFolder(input);
			var idFolder2 = list.GetIdFolder(input);

			Assert.AreEqual(expected, idFolder);
			Assert.AreEqual(expected, idFolder2);
		}
	}
}
