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
	//	public InRomNode FirstNode()
	//	{
	//		if (FirstNodeAddress == null) return null;

	//		return InRomNode.Load(FirstNodeAddress);
	//	}
	//	public InRomNode LastNode()
	//	{
	//		if (LastNodeAdress == null) return null;

	//		return InRomNode.Load(LastNodeAdress);
	//	}

	//	public void Add(InRomNode node)
	//	{
	//		if (FirstNodeAddress == null)
	//		{
	//			FirstNodeAddress = node.Address;
	//			LastNodeAdress = node.Address;
	//			return;
	//		}

	//		LastNodeAdress = node.Address;
	//		LastNode().InsertNext(node);
	//	}
	//	public void Remove(InRomNode node)
	//	{
	//		if (FirstNodeAddress == null)
	//		{
	//			return;
	//		}
	//		if(node.NextNodeAdress == null)
	//		{

	//		}
	//	}
	//}
}
