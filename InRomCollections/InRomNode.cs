using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomNode : InRomNodeBase
	{
		public void InsertNext(InRomNode node)
		{
			if (NextNodeAdress == null)
			{
				NextNodeAdress = node.Address;
				Save();
				return;
			}

			var nextNode = Load(NextNodeAdress);
			node.NextNodeAdress = nextNode.Address;
			NextNodeAdress = node.Address;
			Save();
		}
		public void InsertPrevious(InRomNode node)
		{
			if (PreviousNodeAddress == null)
			{
				PreviousNodeAddress = node.Address;
				Save();
				return;
			}

			var previousNode = Load(PreviousNodeAddress);
			node.PreviousNodeAddress = previousNode.Address;
			PreviousNodeAddress = node.Address;
			Save();
		}
		public static InRomNode Load(string address)
		{
			var jsonContent = File.ReadAllText(address);
			return JsonConvert.DeserializeObject<InRomNode>(jsonContent);
		}
	}

	public class InRomNode<T> : InRomNode
	{
		private T value;

		public T Value { get => value;
			set
			{
				this.value = value;
				Save();
			}
		}
	}
}
