using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomNodeBase : InRomEntry
	{
		private string _nextNodeAddress;
		private string _previousNodeAddress;
	
		public string NextNodeAdress { get => _nextNodeAddress;
			set
			{
				_nextNodeAddress = value;
				Save();
			}
		}
		public string PreviousNodeAddress{ get => _previousNodeAddress;
			set
			{
				_previousNodeAddress = value;
				Save();
			}
		}
	}
}