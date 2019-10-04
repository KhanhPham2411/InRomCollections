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

				if (node.PreviousNodeAddress != Address)
					node.InsertPrevious(this);
			}
			else
			{
				var nextNode = Load<InRomNode>(NextNodeAdress);
				node.NextNodeAdress = nextNode.Address;
				NextNodeAdress = node.Address;

				if (nextNode.PreviousNodeAddress != node.Address)
					nextNode.InsertPrevious(node);
			}
		}
		public void InsertPrevious(InRomNode node)
		{
			if (PreviousNodeAddress == null)
			{
				PreviousNodeAddress = node.Address;

				if (node.NextNodeAdress != Address)
					node.InsertNext(this);
			}
			else
			{
				var previousNode = Load<InRomNode>(PreviousNodeAddress);
				node.PreviousNodeAddress = previousNode.Address;
				PreviousNodeAddress = node.Address;

				if (previousNode.NextNodeAdress != node.Address)
					previousNode.InsertNext(node);
			}
		}

		public static InRomNode Load(string address)
		{
			return Load<InRomNode>(address);
		}
	}

	public class InRomNode<T> : InRomNode
	{
		private T _value;

		public T Value { get => _value;
			set
			{
				_value = value;
				Save();
			}
		}

		public new static InRomNode<T> Load(string address)
		{
			return Load<InRomNode<T>>(address);
		}
	}
}
