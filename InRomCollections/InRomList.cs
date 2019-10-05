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
		public InRomList(string address) : base(address)
		{

		}


	}
}
