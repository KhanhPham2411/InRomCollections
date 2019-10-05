using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomEntryModel
	{
		public string Address;
	}

	public class InRomEntry
	{
		public InRomEntry(string address) // Load or create
		{
			Address = address;

			if (!File.Exists(address))
			{
				var model = new InRomEntryModel() { Address = address };
				Save(model);
			}
		}

		public string Address { get; set; }

		protected void Save(object model)
		{
			var jsonContent = JsonConvert.SerializeObject(model, Formatting.Indented);
			if (Address == null) return;

			File.WriteAllText(Address, jsonContent);
		}
		protected void Delete()
		{
			File.Delete(Address);
		}

		protected T Load<T>()
		{
			return Load<T>(Address);
		}
		protected static T Load<T>(string address)
		{
			if (!File.Exists(address))
			{
				return default(T);
			}
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
