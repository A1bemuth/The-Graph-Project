using System;
using System.Collections.Generic;

namespace GraphDataLayer
{
    public abstract class NamedGraph : Graph
    {
        protected NamedGraph()
        {
            names = new List<string>();
        }

        public virtual bool HasVertice(string name)
        {
            return names.Contains(name);
        }

        public virtual string this[int index]
        {
            get
            {
                if (index < 0 || index > names.Count)
                    throw new ArgumentOutOfRangeException($"Value of {nameof(index)} must be positive and should not exceed vertice count.");
                return names[index];
            }
            set
            {
                if (index < 0 || index > names.Count)
                    throw new ArgumentOutOfRangeException($"Value of {nameof(index)} must be positive and should not exceed vertice count.");
                names[index] = value;
            }
        }

        private readonly List<string> names;
    }
}
