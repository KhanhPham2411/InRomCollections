using InRomCollections.Internal;
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
			if (LastNodeAddress == null) return null;

			return InRomNode.Load(LastNodeAddress);
		}

		public void Add(InRomNode node)
		{
			if (FirstNodeAddress == null)
			{
				FirstNodeAddress = node.Address;
				LastNodeAddress = node.Address;
				return;
			}

			LastNode().InsertNext(node);
			LastNodeAddress = node.Address;
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
			if (node.Address == LastNodeAddress)
			{
				LastNodeAddress = node.PreviousNodeAddress;
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
		private const int _splitIdLength = 2;
		private bool _useAutoId;

		public InRomList(string address, bool useaAutoId = true) : base(address)
		{
			_baseFolder = Path.Combine(Path.GetDirectoryName(address), "Nodes");
			_name = Path.GetFileNameWithoutExtension(address);
			_useAutoId = useaAutoId;
		}

		public void AddRange(IEnumerable<T> collections)
		{
			foreach (var item in collections)
			{
				Add(item);
			}
		}
		public void Add(T item)
		{
			if (Contains(item))
				return;

			string id = Guid.NewGuid().ToString();
			if (_useAutoId)
			{
				var autoId = GetAutoId(item);
				if (autoId != null)
				{
					id = autoId;
				}
			}
			
			Add(id, item);
		}
		public virtual void Add(string id, T item)
		{
			var address = GetAddress(id);
			var node = new InRomNode<T>(address);
			node.Value = item;

			Add(node);
		}

		public T NextOf(T item)
		{
			var address = GetAddress(item);
			if (address == null) return default(T);

			var nextNodeAddress = Load<InRomNodeModel<T>>(address).NextNodeAdress;
			if (nextNodeAddress == null) return default(T);

			return Load<InRomNodeModel<T>>(nextNodeAddress).Value;
		}
		public T PreviousOf(T item)
		{
			var address = GetAddress(item);
			if (address == null) return default(T);

			var previousNodeAddress = Load<InRomNodeModel<T>>(address).PreviousNodeAddress;
			if (previousNodeAddress == null) return default(T);

			return Load<InRomNodeModel<T>>(previousNodeAddress).Value;
		}

		public void Remove(T item)
		{
			if (item == null)
				return;

			Remove(GetAutoId(item));
		}
		public virtual void Remove(string id)
		{
			if (id == null) return;

			var address = GetAddress(id);
			Remove(InRomNode.Load(address));
		}

		public virtual T Get(string id)
		{
			var address = GetAddress(id);
			if (address == null) return default(T);

			var node = Load<InRomNodeModel<T>>(address);
			if (node == null)
				return default(T);

			return node.Value;
		}
		public bool Contains(T item)
		{
			var address = GetAddress(item);
			return File.Exists(address);
		}

		public string GetAddress(T item)
		{
			var id = GetAutoId(item);
			if (id == null) return null;

			return GetAddress(id);
		}
		public string GetAddress(string id)
		{
			var address = Path.Combine(_baseFolder, $"{GetIdFolder(id)}/{_name}_{id}.json");
			return address;
		}
		public string GetIdFolder(string id)
		{
			List<string> splitId = new List<string>();
			var startIndex = 0;
			var splitIdLength = _splitIdLength;

			while (true)
			{
				splitId.Add(id.Substring(startIndex, splitIdLength));
				startIndex = startIndex + splitIdLength;

				if (startIndex + splitIdLength > id.Length && id.Length > startIndex)
				{
					splitIdLength = id.Length - startIndex;
				}

				if (startIndex == id.Length) break;
			}

			return string.Join("/", splitId);
		}
		public string GetAutoId(T item)
		{
			if (_useAutoId)
			{
				dynamic dynamicItem = item;
				var id = dynamicItem.Id;
				if (id == null)
				{
					throw new Exception($"Please specify Id for the model or disable useAutoId when construct:\n{JsonConvert.SerializeObject(item)}");
				}
				return id;
			}

			return null;
		}

		public T First()
		{
			if (FirstNodeAddress == null) return default(T);

			return InRomNode<T>.Load(FirstNodeAddress).Value;
		}
		public T Last()
		{
			if (LastNodeAddress == null) return default(T);

			return InRomNode<T>.Load(LastNodeAddress).Value;
		}
		public T Current
		{
			get
			{
				if (CurrentNodeAddress == null)
				{
					CurrentNodeAddress = FirstNodeAddress;
					return First();
				}

				return InRomNode<T>.Load(CurrentNodeAddress).Value;
			}
			set
			{
				if (Contains(value))
				{
					CurrentNodeAddress = GetAddress(value);
				}
				else
				{
					CurrentNodeAddress = FirstNodeAddress;
				}
			}
		}
	}
}
