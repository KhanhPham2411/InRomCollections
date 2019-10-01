using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomNode
	{
		public string address;
		public string nextNodeAdress;
		public string previousNodeAddress;

		public void InsertNext(InRomNode node)
		{
			if (nextNodeAdress == null)
			{
				nextNodeAdress = node.address;
				Save();
				return;
			}

			var nextNode = Load(nextNodeAdress);
			node.nextNodeAdress = nextNode.address;
			nextNodeAdress = node.address;
			Save();
		}

		public void InsertPrevious(InRomNode node)
		{
			if (previousNodeAddress == null)
			{
				previousNodeAddress = node.address;
				Save();
				return;
			}

			var previousNode = Load(previousNodeAddress);
			node.previousNodeAddress = previousNode.address;
			previousNodeAddress = node.address;
			Save();
		}
		public void Save()
		{
			var jsonContent = JsonConvert.SerializeObject(this);
			File.WriteAllText(address, jsonContent);
		}

		public static InRomNode Load(string address)
		{
			var jsonContent = File.ReadAllText(address);
			return JsonConvert.DeserializeObject<InRomNode>(jsonContent);
		}
	}
}
