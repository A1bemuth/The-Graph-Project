using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDataLayer
{
    public abstract class NamedGraph : Graph
    {
        protected NamedGraph()
        {
            verticeNames = new Dictionary<int, string>();
        }

        public virtual bool HasVertice(string name)
        {
            return verticeNames.ContainsValue(name);
        }

        public virtual bool HasArrow(string from, string to)
        {
            var fromIndex = this[from];
            var toIndex = this[to];

            if (fromIndex == null || toIndex == null)
                return false;
            return HasArrow((int) fromIndex, (int) toIndex);
        }

        public virtual string this[int index]
        {
            get
            {
                if (index < 0 || index > VerticesCount)
                    throw new ArgumentOutOfRangeException($"Value of {nameof(index)} must be positive and should not exceed vertice count.");
                return verticeNames.ContainsKey(index) ? verticeNames[index] : null;
            }
            set
            {
                if (index < 0 || index > VerticesCount)
                    throw new ArgumentOutOfRangeException($"Value of {nameof(index)} must be positive and should not exceed vertice count.");
                verticeNames[index] = value;
            }
        }

        public virtual int? this[string name]
        {
            get { return verticeNames.ContainsValue(name) 
                    ? (int?) verticeNames.First(v => v.Value.Equals(name)).Key
                    : null; }
        }

        public string Name { get; set; }

        private readonly Dictionary<int, string> verticeNames;
    }
}
