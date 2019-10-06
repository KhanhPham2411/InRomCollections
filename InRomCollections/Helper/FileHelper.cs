using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Helper
{
	public class FileHelper
	{
		private static Dictionary<string, object> _lockDictionary = new Dictionary<string, object>();

		public static string ReadAllText(string path)
		{
			CheckLockDictionary(path);

			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}

			lock (_lockDictionary[path])
			{
				return InternalReadAllText(path, Encoding.UTF8);
			}
		}

		private static string InternalReadAllText(string path, Encoding encoding)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(path, encoding, true, 1024))
			{
				result = streamReader.ReadToEnd();
				streamReader.Close();
			}
			return result;
		}

		public static void WriteAllText(string path, string contents)
		{
			CheckLockDictionary(path);

			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}

			lock (_lockDictionary[path])
			{
				InternalWriteAllText(path, contents, Encoding.UTF8);
			}
		}

		private static object _lockCheck = new object();
		public static void CheckLockDictionary(string path)
		{
			lock(_lockCheck)
			{
				if (!_lockDictionary.ContainsKey(path))
				{
					_lockDictionary.Add(path, new object());
				}
			}
		}

		private static void InternalWriteAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024))
			{
				streamWriter.Write(contents);
				streamWriter.Close();
			}
		}
	}
}
