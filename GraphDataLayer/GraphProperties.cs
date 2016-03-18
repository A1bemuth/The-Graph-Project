using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataLayer
{
    public static class GraphProperties
    {

        public static int GetSizeOfGraph(this IGraph graph)
        {
            return 0;
        }

        /// <summary>
        ///     Возвращает плотность графа
        /// </summary>
        public static double GetDensity(this GraphStructure graph)
        {
            double density = (double)graph.EdgesCount / (graph.VerticesCount * (graph.VerticesCount - 1));
            if (!graph.IsDirected())
                density *= 2;
            return density;

        }
        /// <summary>
        ///     Определяет является ли граф направленным
        /// </summary>
        public static bool IsDirected(this IGraph graph)
        {
            bool isDirected = false;
            foreach (var i in graph.GetIncidenceMatrix())
            {
                if ((i.Count(j => j < 0) > 0))
                {
                    isDirected = true;
                    break;
                }
            }
            return isDirected;
        }
    }
}
