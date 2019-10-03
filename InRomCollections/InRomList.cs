using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	//public class InRomList : InRomListBase
	//{
	//	public InRomNode<T> First()
	//	{
	//		if (FirstNodeAddress == null) return null;

	//		return InRomNode<T>.Load(FirstNodeAddress);
	//	}
	//	public InRomNode Last()
	//	{
	//		if (LastNodeAdress == null) return null;

	//		return InRomNode.Load(LastNodeAdress);
	//	}

	//	public void Add(InRomNode node)
	//	{
	//		if (FirstNodeAddress == null)
	//		{
	//			setFirstNodeAddress(node.Address);
	//			setLastNodeAddress(node.Address);
	//			return;
	//		}
	//		Last().InsertNext(node);
	//		LastNodeAdress = node.Address;
	//	}

	//}
}
