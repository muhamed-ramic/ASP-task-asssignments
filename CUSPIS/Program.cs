using System;
using System.Collections.Generic;


namespace CUSPIS
{

    /*
     To use the application change the current start vertice to either one of this vertices 1, 2, 3, 4,5.
     Maybe it is not finished assignment but I have realised it like this ,and It is my own work
     not someone elses, as I can guarantee that and explain if needed anything in code.

     */
    public class Graph
    {
        private int vertices;
        private List<int>[] adjacencyList;
        private List<int>[] VisitedEachVerticeNumber;
        private List<int> path = new List<int>();

        public Graph(int v)
        {
            vertices = v;
            adjacencyList = new List<int>[v + 1];
            VisitedEachVerticeNumber = new List<int>[v + 1];

            for (int i = 1; i <= v; i++)
            {
                adjacencyList[i] = new List<int>();
                VisitedEachVerticeNumber[i] = new List<int>();
            }
            adjacencyList[0] = null;
        }
        public Graph(Graph graph)
        {
            vertices = graph.vertices;
            adjacencyList = graph.adjacencyList;
            VisitedEachVerticeNumber = new List<int>[vertices + 1];
            path = new List<int>();

            for (int i = 1; i <=vertices; i++)
            {
                VisitedEachVerticeNumber[i] = new List<int>();
            }
        }
        public void AddEdge(int v, int w)
        {
            adjacencyList[v].Add(w);
        }
        public void DFS(int startVertice)
        {
            bool[] isVisited = new bool[vertices + 1];

            for (int i = 1; i <= vertices; i++)
            {
                //Default every vertice on beginning is unvisited.
                isVisited[i] = false;
            }
            DFSRecursive(startVertice, isVisited);
        }
        public void DFSRecursive(int vertice, bool[] isVisited)
        {
            // Mark the current node as visited and
            // print it 
            isVisited[vertice] = true;
            path.Add(vertice);

            //Path that we are travelling
            if (path.Count % 9==0)
            {
                bool isCorrectPath =  isPathCorrect(path);
                if (isCorrectPath)
                {
                    foreach (var item in path)
                    {
                        Console.Write(item + " ");
                    }
                    isVisited[vertice] = false;
                    Console.WriteLine("\n----------------\n");
                }
            }

            //Traverse all adjacent vertices
            for (int i = 0; i < adjacencyList[vertice].Count; i++)
            {
                int v = adjacencyList[vertice][i];

                if (!isVisited[v])
                {
                    VisitedEachVerticeNumber[vertice].Add(v);
                    VisitedEachVerticeNumber[v].Add(vertice);
                    DFSRecursive(v, isVisited);

                    VisitedEachVerticeNumber[v].Remove(VisitedEachVerticeNumber[v].Count - 1);
                    VisitedEachVerticeNumber[vertice].Remove(VisitedEachVerticeNumber[vertice].Count - 1);
                }
                //Traverse all not visited adjacent lines 
                if (!VisitedEachVerticeNumber[vertice].Contains(v)  || !VisitedEachVerticeNumber[v].Contains(vertice)&& v!=path[path.Count-2] && v!=path[path.Count-1])
                {
                    bool didFoundDuplicates = FindDuplicateLines(path, v, vertice);

                    //To not end in same line as beginning and middle, especially for 5. vertice.
                    if (path.Count == 4)
                    {
                        if (v == path[0] && v==5)
                        {
                            int tempI = i + 1;

                            if (tempI == adjacencyList[vertice].Count)
                            {
                                return;
                            }

                            int verticeUnVisited = adjacencyList[vertice][tempI];
                            do
                            {
                                didFoundDuplicates = FindDuplicateLines(path, verticeUnVisited, vertice);
                                tempI++;
                            } while (didFoundDuplicates && tempI < adjacencyList[vertice].Count);

                            if (didFoundDuplicates == false)
                            {
                                VisitedEachVerticeNumber[vertice].Add(verticeUnVisited);
                                VisitedEachVerticeNumber[verticeUnVisited].Add(vertice);
                                DFSRecursive(verticeUnVisited, isVisited);
                            }
                        }
                    }

                    if (didFoundDuplicates==false)
                    {
                        VisitedEachVerticeNumber[vertice].Add(v);
                        VisitedEachVerticeNumber[v].Add(vertice);
                        DFSRecursive(v, isVisited);
                    }
                    else 
                    {
                        //Eliminate duplicate lines.
                        int tempI = i+1;

                        if (tempI == adjacencyList[vertice].Count)
                        {
                            return;
                        }

                        int verticeUnVisited = adjacencyList[vertice][tempI];
                        do
                        {
                            didFoundDuplicates = FindDuplicateLines(path, verticeUnVisited, vertice);
                            tempI++;
                        } while (didFoundDuplicates && tempI<adjacencyList[vertice].Count);

                        if (didFoundDuplicates == false)
                        {
                            VisitedEachVerticeNumber[vertice].Add(verticeUnVisited);
                            VisitedEachVerticeNumber[verticeUnVisited].Add(vertice);
                            DFSRecursive(verticeUnVisited, isVisited);
                        }
                    }
                }
            }
            //After return of recursion
            path.RemoveAt(path.Count - 1);
            isVisited[vertice] = false;
        }
        public bool FindDuplicateLines(List<int> path, int start, int destination)
        {
            int firstCounter = 0, secondCounter = 0;
            foreach (var item in path)
            {
                if (item == start)
                {
                    firstCounter++;
                }
                else if (item == destination)
                {
                    secondCounter++;
                }
            }
            if (firstCounter == 2 && secondCounter == 2)
                return true;
            return false;
        }
        public bool isPathCorrect(List<int>path)
        {
            List<int> listOf8Numbers = new List<int>();
            for(int i = 0; i < path.Count; i++)
            {
                for(int j = i + 1; j < path.Count; j++)
                {
                    listOf8Numbers.Add(path[i] * 10 + path[j] * 1);
                    break;
                }
            }
            int counter = 0;
            for(int i = 0; i < listOf8Numbers.Count; i++)
            {
                for(int j = i + 1; j < listOf8Numbers.Count; j++)
                {
                    if (listOf8Numbers[i] == GetReverseNumber(listOf8Numbers[j]) || listOf8Numbers[i]==listOf8Numbers[j])
                        counter++;
                }
            }
            if (counter != 0)
                return false;
            return true;
        }
        public int GetReverseNumber(int number)
        {
            int result = 0;
            int decade = 10;
            do
            {
                result += number % 10 * decade;
                decade = 1;
                number /= 10;
            } while (number != 0);
            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Graph graph = new Graph(5);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);

            graph.AddEdge(2, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 1);


            graph.AddEdge(3, 5);
            graph.AddEdge(3,1);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 4);

            graph.AddEdge(4, 5);
            graph.AddEdge(4, 2);
            graph.AddEdge(4, 1);
            graph.AddEdge(4, 3);

            graph.AddEdge(5, 4);
            graph.AddEdge(5, 3);

            Graph graphVerticeTwo = new Graph(graph);
            Graph graphVerticeThree = new Graph(graph);
            Graph graphVerticeFour = new Graph(graph);
            Graph graphVerticeFive = new Graph(graph);

            //graph.DFS(1);
            Console.WriteLine("---Start from vertice 2.----\n");
            graph.DFS(2);
            Console.WriteLine("---------Start from vertice 3 .-----------\n");
            //graph.DFS(3);
            Console.WriteLine("--------Start from vertice 4.--------------\n");
            //graph.DFS(4);
            Console.WriteLine("-------Start from vertice 5. --------------\n");
            //graph.DFS(5);
        }
    }
}
