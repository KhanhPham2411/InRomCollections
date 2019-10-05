using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
		public InRomNode(string address) : base(address)
		{
		}

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

		public void RemoveNext()
		{
			if (NextNodeAdress == null)
				return;

			var nextNode = Load(NextNodeAdress);
			NextNodeAdress = null;

			if (nextNode.NextNodeAdress != null)
			{
				var nodeAfterNext = Load(nextNode.NextNodeAdress);
				nodeAfterNext.PreviousNodeAddress = null;
				InsertNext(nodeAfterNext);	
			}

			nextNode.Delete();
		}

		public void RemovePrevious()
		{
			if (PreviousNodeAddress == null)
				return;

			var previousNode = Load(PreviousNodeAddress);
			PreviousNodeAddress = null;

			if (previousNode.PreviousNodeAddress != null)
			{
				var nodeBeforePrevious = Load(previousNode.PreviousNodeAddress);
				nodeBeforePrevious.NextNodeAdress = null;
				InsertPrevious(nodeBeforePrevious);
			}

			previousNode.Delete();
		}

		public static InRomNode Load(string address)
		{
			return new InRomNode(address);
		}
	}

	public class InRomNodeModel<T> : InRomNodeBaseModel
	{
		public T Value;
	}

	public class InRomNode<T> : InRomNode
	{
		public InRomNode(string address) : base(address)
		{
		}

		public T Value
		{
			get
			{
				return Load<InRomNodeModel<T>>().Value;
			}
			set
			{
				SetProperty(nameof(Value), value);
			}
		}

		public new static InRomNode<T> Load(string address)
		{
			return new InRomNode<T>(address);
		}
	}
}
