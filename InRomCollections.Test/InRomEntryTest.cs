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
	public class InRomEntryTest
	{
		private string _testFile = "folder1/folder2/folder3/test.json";

		[Test]
		public void SetAddress_ShouldCreateFile()
		{
			Assert.IsFalse(File.Exists(_testFile));

			InRomEntry entry = new InRomEntry(_testFile);

			Assert.IsTrue(File.Exists(_testFile));
		}

		[TearDown]
		public virtual void TearDown()
		{
			File.Delete(_testFile);
		}
	}
}
