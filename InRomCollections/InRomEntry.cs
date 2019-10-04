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
			if (Address == null) return;

			File.WriteAllText(Address, jsonContent);
		}
		public static T Load<T>(string address)
		{
			var jsonContent = File.ReadAllText(address);
			return JsonConvert.DeserializeObject<T>(jsonContent);
		}

		public static bool operator ==(InRomEntry source, InRomEntry target)
		{
			var jsonSource = JsonConvert.SerializeObject(source);
			var jsonTarget = JsonConvert.SerializeObject(target);

			return jsonSource == jsonTarget;
		}
		public static bool operator !=(InRomEntry source, InRomEntry target)
		{
			var jsonSource = JsonConvert.SerializeObject(source);
			var jsonTarget = JsonConvert.SerializeObject(target);

			return jsonSource != jsonTarget;
		}
	}
}
