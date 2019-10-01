using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomList<T>
	{
		private InRomNode _firstNode;
		private InRomNode _lastNode;

		public InRomNode First()
		{
			return _firstNode;
		}
		public InRomNode Last()
		{
			return _lastNode;
		}
	}
}
