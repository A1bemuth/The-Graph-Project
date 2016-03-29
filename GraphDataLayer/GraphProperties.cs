using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphDataLayer
{
    public static class GraphProperties
    {
        public static int GetSizeOfGraph(this IGraph graph)
        {
            return graph.VerticesCount-1;
        }

        /// <summary>
        ///     Возвращает плотность графа
        /// </summary>
        public static double GetDensity(this IGraph graph)
        {
            double density = (double)graph.ArrowsCount / (graph.VerticesCount * (graph.VerticesCount - 1));
            return Math.Round(density,2);
        }

        /// <summary>
        ///     Определяет является ли граф направленным
        /// </summary>
        public static bool IsDirected(this IGraph graph)
        {
           return true;
        }
    }
}
