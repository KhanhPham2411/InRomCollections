using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomListBaseModel : InRomEntryModel
	{
		public string FirstNodeAddress;
		public string LastNodeAddress;
	}

	public class InRomListBase : InRomEntry
	{
		public InRomListBase(string address) : base(address)
		{

		}

		public string FirstNodeAddress
		{
			get
			{
				return Load<InRomListBaseModel>().FirstNodeAddress;
			}
			set
			{
				SetProperty(nameof(FirstNodeAddress), value);
			}
		}
		public string LastNodeAddress
		{
			get
			{
				return Load<InRomListBaseModel>().LastNodeAddress;
			}
			set
			{
				SetProperty(nameof(LastNodeAddress), value);
			}
		}
	}


}
