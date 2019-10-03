using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomListBase : InRomEntry
	{
		private string _firstNodeAddress;
		private string _lastNodeAdress;

		public string FirstNodeAddress { get => _firstNodeAddress;
			set
			{
				_firstNodeAddress = value;
				Save();
			}
		}
		public string LastNodeAdress { get => _lastNodeAdress;
			set
			{
				_lastNodeAdress = value;
				Save();
			}
		}
	}
}
