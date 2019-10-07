using InRomCollections.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRomCollections
{
    public class InRomDictionary<T> : InRomList<T>
    {
        public InRomDictionary(string address, bool useaAutoId = true) : base(address, useaAutoId)
        {
        }

        public override void Add(string id, T item)
        {
            var address = GetAddress(id);
            if (!File.Exists(address))
            {
                var node = new InRomNode<T>(address);
                node.Value = item;
            }
        }

        public bool Contains(string id)
        {
            var address = GetAddress(id);
            return File.Exists(address);
        }

        public override T Get(string id)
        {
            return base.Get(id);
        }

        public override void Remove(string id)
        {
            if (id == null) return;

            var address = GetAddress(id);
            if (File.Exists(address))
            {
                File.Delete(address);
            }
        }
    }
}
