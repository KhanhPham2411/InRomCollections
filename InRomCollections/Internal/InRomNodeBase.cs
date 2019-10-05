using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Internal
{
	public class InRomNodeBaseModel : InRomEntryModel
	{
		public string NextNodeAdress;
		public string PreviousNodeAddress;
	}

	public class InRomNodeBase : InRomEntry
	{
		public InRomNodeBase(string address) : base(address)
		{
		}

		public string NextNodeAdress {
			get
			{
				var model = Load<InRomNodeBaseModel>();
				return model?.NextNodeAdress;
			}
			set
			{
				SetProperty(nameof(NextNodeAdress), value);
			}
		}
		public string PreviousNodeAddress{
			get
			{
				var model = Load<InRomNodeBaseModel>();
				return model?.PreviousNodeAddress;
			}
			set
			{
				SetProperty(nameof(PreviousNodeAddress), value);
			}
		}
	}
}