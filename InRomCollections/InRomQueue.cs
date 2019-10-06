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


		private static object _lockEnqueue = new object();
		public void Enqueue(T item)
		{
			lock (_lockEnqueue)
			{
				Add(item);
			}
		}

		public async void EnqueueAsync(T item)
		{
			var task = new Task(() =>
			{
				Enqueue(item);
			});

			task.Start();
			await task;
		}
		public void EnqueueRange(IEnumerable<T> collections)
		{
			AddRange(collections);
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
