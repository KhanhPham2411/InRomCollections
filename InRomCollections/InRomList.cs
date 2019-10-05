using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomList : InRomListBase
	{
		public InRomList(string address) : base(address)
		{
			
		}

		public InRomNode FirstNode()
		{
			if (FirstNodeAddress == null) return null;

			return InRomNode.Load(FirstNodeAddress);
		}
		public InRomNode LastNode()
		{
			if (LastNodeAdress == null) return null;

			return InRomNode.Load(LastNodeAdress);
		}

		public void Add(InRomNode node)
		{
			if (FirstNodeAddress == null)
			{
				FirstNodeAddress = node.Address;
				LastNodeAdress = node.Address;
				return;
			}

			LastNode().InsertNext(node);
			LastNodeAdress = node.Address;
		}
		public void Remove(InRomNode node)
		{
			if (FirstNodeAddress == null)
			{
				return;
			}
			if (node.Address == FirstNodeAddress)
			{
				FirstNodeAddress = node.NextNodeAdress;
			}
			if (node.Address == LastNodeAdress)
			{
				LastNodeAdress = node.PreviousNodeAddress;
			}

			if (node.NextNodeAdress != null)
			{
				var nextNode = InRomNode.Load(node.NextNodeAdress);
				nextNode.RemovePrevious();
				return;
			}
			if (node.PreviousNodeAddress != null)
			{
				var previousNode = InRomNode.Load(node.PreviousNodeAddress);
				previousNode.RemoveNext();
				return;
			}
		}
	}

	public class InRomList<T> : InRomList
	{
		private string _baseFolder;
		private string _name;
		private int _splitIdLength = 2;

		public InRomList(string address) : base(address)
		{
			_baseFolder = Path.GetDirectoryName(address) + "/Nodes";
			_name = Path.GetFileNameWithoutExtension(address);
		}

		public void Add(T item)
		{
			string id = Guid.NewGuid().ToString();
			Add(id, item);
		}
		public void Add(string id, T item)
		{
			var address = GetAddress(id);
			var node = new InRomNode<T>(address);
			node.Value = item;

			Add(node);
		}

		public void Remove(string id)
		{
			var address = $"{_baseFolder}/{id}";
			Remove(InRomNode.Load(address));
		}

		public string GetAddress(string id)
		{
			var address = $"{_baseFolder}/{GetIdFolder(id)}/{_name}_{id}.json";
			return address;
		}
		public string GetIdFolder(string id)
		{
			List<string> splitId = new List<string>();
			var startIndex = 0;
			while (true)
			{
				splitId.Add(id.Substring(startIndex, _splitIdLength));
				startIndex = startIndex + _splitIdLength;

				if (startIndex + _splitIdLength > id.Length && id.Length > startIndex)
				{
					_splitIdLength = id.Length - startIndex;
				}

				if (startIndex == id.Length) break;
			}

			return string.Join("/", splitId);
		}
	}
}
