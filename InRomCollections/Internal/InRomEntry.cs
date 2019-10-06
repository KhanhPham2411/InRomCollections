using InRomCollections.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections.Internal
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

		protected void SetProperty(string propertyName, string value)
		{
			var model = Load();
			model[propertyName] = value;
			Save(model);
		}
		protected void SetProperty(string propertyName, object value)
		{
			var model = Load();
			model[propertyName] = JObject.FromObject(value);
			Save(model);
		}

		protected void Save(object model)
		{
			var jsonContent = JsonConvert.SerializeObject(model, Formatting.Indented);
			if (Address == null) return;

			//new FileInfo(Address).Directory.Create();
			var dir = Path.GetDirectoryName(Address);
			if (!string.IsNullOrEmpty(dir))
			{
				Directory.CreateDirectory(dir);
			}

			FileHelper.WriteAllText(Address, jsonContent);
		}

		protected void Delete()
		{
			File.Delete(Address);
		}

		protected JObject Load()
		{
			return Load(Address);
		}
		protected static JObject Load(string address)
		{
			if (!File.Exists(address))
			{
				return null;
			}

			var jsonContent = FileHelper.ReadAllText(address);
			return JObject.Parse(jsonContent);
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
			var jsonContent = FileHelper.ReadAllText(address);
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
