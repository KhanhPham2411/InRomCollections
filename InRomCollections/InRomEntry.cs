using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomEntry
	{
		private string _address;

		public string Address
		{
			get => _address;
			set
			{
				_address = value;
				Save();
			}
		}

		protected void Save()
		{
			var jsonContent = JsonConvert.SerializeObject(this, Formatting.Indented);
			File.WriteAllText(Address, jsonContent);
		}
	}
}
