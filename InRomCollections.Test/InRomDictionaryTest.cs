using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InRomCollections.Test
{
    public class InRomDictionaryTest : BaseTest
    {
        [Test]
        public void Add_ShouldRunCorrectly()
        {
            InRomDictionary<TestClass> dict = new InRomDictionary<TestClass>(Path.Combine(_testFolder, "dict.json"));

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

            dict.Add(item1.Id, item1);
            dict.Add(item2.Id, item2);

            bool isExist = dict.Contains(item2.Id);
            Assert.IsTrue(isExist);

            Assert.AreEqual(item1.TestField1, dict.Get(item1.Id).TestField1);
        }

        [Test]
        public void MultithreadAdd_ShouldRunCorrectly()
        {
            InRomDictionary<TestClass> dict = new InRomDictionary<TestClass>(Path.Combine(_testFolder, "dict.json"));

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
            TestClass item = null;

            var t1 = new Thread(()=> dict.Add(item1.Id, item1));
            var t2 = new Thread(() => item = dict.Get(item1.Id));
            var t3 = new Thread(() => dict.Add(item1.Id, item1));
            var t4 = new Thread(() => dict.Add(item2.Id, item1));
            t1.Start();t3.Start(); t4.Start();
            Thread.Sleep(100);
            t2.Start();

            t1.Join();t3.Join();
            t2.Join();t4.Join();

            bool isExist = dict.Contains(item2.Id);
            Assert.IsTrue(isExist);

            Assert.AreEqual(item1.TestField1, item.TestField1);
        }
    }
}
