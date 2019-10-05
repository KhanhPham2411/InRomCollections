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
		public string LastNodeAdress;
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
				return Load<InRomListBaseModel>(Address).FirstNodeAddress;
			}
			set
			{
				var model = Load<InRomListBaseModel>();
				model.FirstNodeAddress = value;
				Save(model);
			}
		}
		public string LastNodeAdress
		{
			get
			{
				return Load<InRomListBaseModel>(Address).LastNodeAdress;
			}
			set
			{
				var model = Load<InRomListBaseModel>();
				model.LastNodeAdress = value;
				Save(model);
			}
		}
	}


}
