using System.Linq;

namespace GraphDataLayer
{
    public class AdjacencyListGraph
    {
        public AdjacencyListGraph() : this(2)
        {
        }

        public AdjacencyListGraph(long size)
        {
            count = 0;
            arrowCount = new long[size];
            vertices = new long[size][];
        }

        public void AddArrow(long from, long to)
        {
            var fromArrowsCount = arrowCount[from];
            if (vertices[from].LongLength == fromArrowsCount)
            {
                vertices[from].AddWithGrowth(to);
            }
            else
            {
                vertices[from][fromArrowsCount] = to;
            }
            arrowCount[from]++;
        }

        //TODO(albemuth): нормально сделать
        public void AddVertices(long verticesCount)
        {
            for (int i = 0; i < verticesCount; i++)
            {
                AddVertice();
            }
        }

        public void AddVertice()
        {
            if (vertices.GetLongLength(0) == count)
            {
                vertices.AddWithGrowth(new long[1]);
                arrowCount.AddWithGrowth(0);
                count++;
            }
            else
            {
                vertices[count] = new long[1];
                arrowCount[count++] = 0;
            }
        }

        public long[] GetNeighbours(long vertice)
        {
            return vertices[vertice];
        }

        public bool[][] GetIncidenceMatrix()
        {
            var matrix = new bool[count][];

            for (int i = 0; i < count; i++)
            {
                matrix[i] = new bool[count];
                var arrows = vertices[i];
                for (int j = 0; j < arrowCount[i]; j++)
                {
                    matrix[i][arrows[j]] = true;
                }
            }
            return matrix;
        }

        public bool HasArrow(long from, long to)
        {
            return vertices[from].Contains(to);
        }

        public bool AreReciprocal(long verticeOne, long verticeTwo)
        {
            return HasArrow(verticeOne, verticeTwo) 
                && HasArrow(verticeTwo, verticeOne);
        }

        public long VerticesCount => count;

        private long count;
        private readonly long[] arrowCount;
        private readonly long[][] vertices;
    }
}
