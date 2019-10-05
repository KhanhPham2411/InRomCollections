using InRomCollections.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
	public class InRomQueue<T> : InRomList<T>
	{
		public InRomQueue(string address, bool useaAutoId = true) : base(address, useaAutoId)
		{
		}

		public void Enqueue(T item)
		{
			Add(item);
		}
		public T Dequeue()
		{
			var peek = Peek();
			Remove(First());

			return peek;
		}

		public T Peek()
		{
			return First();
		}
	}
}
