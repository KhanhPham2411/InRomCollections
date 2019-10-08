using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Helper
{
	public class FileHelper
	{
		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}


			return InternalReadAllText(path, Encoding.UTF8);
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
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}

			InternalWriteAllText(path, contents, Encoding.UTF8);
		}
		private static void InternalWriteAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024))
			{
				streamWriter.Write(contents);
				streamWriter.Close();
			}
		}

		public static string[] ReadAllLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}
			return InternalReadAllLines(path, Encoding.UTF8);
		}
		private static string[] InternalReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				string item;
				while ((item = streamReader.ReadLine()) != null)
				{
					list.Add(item);
				}
				streamReader.Close();
			}
			return list.ToArray();
		}

		public static void WriteAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Argument_EmptyPath");
			}
			InternalWriteAllLines(new StreamWriter(path, false), contents);
		}
		private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
		{
			try
			{
				foreach (string value in contents)
				{
					writer.WriteLine(value);
				}
				writer.Close();
			}
			finally
			{
				if (writer != null)
				{
					((IDisposable)writer).Dispose();
				}
			}
		}

		public static string GetAbsolutePath(string relativePath)
		{
			return Path.Combine(GetCurrentFolder(), relativePath);
		}
		public static string GetCurrentFolder()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
		}
	}
}
