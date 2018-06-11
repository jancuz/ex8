using System;
using System.Collections.Generic;
using MyLibrary;

namespace ex8
{
    class Graph
    {
        protected static Random rnd = new Random();

        // формирование случайного графа
        public static void FormRandomGraph(out int[,] graph)
        {
            int countVertex = AskData.ReadIntNumber("Введите количество вершин графа (от 2 до 20): ", 2, 20); // кол-во вершин графа
            graph = new int[countVertex, countVertex];      // граф
            for (int i = 0; i < countVertex; i++)
                for (int j = 0; j < countVertex; j++)
                    graph[i, j] = 0;

            for (int i = 0; i < countVertex; i++)          // для каждой вершины в графе
            {
                int numConnection;                         // кол-во ребер, исходящих от данной вершины
                do
                {
                    numConnection = rnd.Next(0, countVertex);
                } while (numConnection >= countVertex);    // должно быть меньше, чем кол-во вершин графа

                int haveConnection = 0;                    // кол-во уже исходящих ребер из данной вершины
                for (int s = 0; s < countVertex; s++)
                    if (graph[i, s] == 1) haveConnection++;

                numConnection = numConnection - haveConnection;
                for (int j = 0; j < numConnection; j++)    // для каждой вершины
                {
                    var numTry = 20;                       // 20 попыток

                    while (numTry > 0)                     // пока соединение не найдено
                    {
                        var connectVert = RandomVertex(countVertex, i); // найти случайную вершину, кроме изначальной
                        if (graph[i, connectVert] == 1 || graph[connectVert, i] == 1) // если связь с этой вершиной уже есть, то
                            numTry--;                      // уменьшить кол-во попыток
                        else
                        {
                            graph[i, connectVert] = 1;
                            graph[connectVert, i] = 1;     // иначе добавить вершину
                            break;                         // выйти из цикла
                        }
                    }
                }
            }
        }

        // создание случайной вершины, не являющейся заданной
        private static int RandomVertex(int countVertex, int startVertex)
        {
            int ans = rnd.Next(countVertex);
            while (ans == startVertex)
                ans = rnd.Next(countVertex);
            return ans;
        }

        // ввод графа с клавиатуры
        public static void FormGraph(out int[,] graph)
        {
            int countVertex = AskData.ReadIntNumber("Введите количество вершин графа (от 2 до 20): ", 2, 20);
            graph = new int[countVertex, countVertex];
            for (int i = 0; i < countVertex; i++)
            {
                for (int j = i + 1; j < countVertex; j++)
                {
                    Console.WriteLine("Соединение вершин {0} и {1}", i + 1, j + 1);
                    graph[i, j] = AskData.ReadIntNumber("Введите 0 или 1:", 0, 1);
                    graph[j, i] = graph[i, j];
                }
            }
        }

        // печать графа
        public static void ShowGraph(int[,] graph)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                    Console.Write(graph[i, j] + " ");
                Console.WriteLine();
            }
        }

        // поиск эйлерового цикла
        private static void Search(int v, ref Stack<int> Stack, int n, ref int[,] graph)
        {
            for (int i = 0; i < n; i++)
                if (graph[v, i] != 0)                    // если есть инцидентные ребра
                {
                    graph[v, i] = 0;                     // удаляем пройденное ребро
                    graph[i, v] = 0;
                    Search(i, ref Stack, n, ref graph);  // переход к следующей вершине, рекурсивный поиск
                }
            Stack.Push(v + 1);                           // запись пройденной вершины в стек
        }

        // проверка графа и поиск эйлерового циикла
        public static void EulerCycle(int v, ref Stack<int> Stack, int n, ref int[,] graph)
        {
            if (!CheckEven(graph, out int strMistake))   // проверка, является ли граф эйлеровым
            {
                Console.WriteLine("Эйлерового цикла не существует!");
                Console.WriteLine("Вершина с нечетной степенью: {0}", strMistake + 1);
            }
            else
            {
                Search(v, ref Stack, n, ref graph);      // поиск эйлерового цикла
            }
        }

        // является ли граф Эйлеровым (проверка четности вершин)
        private static bool CheckEven(int[,] gr, out int numStrWithMistake)
        {
            var countOdd = 0;                           // кол-во заполненных ячеек (четность вершины)
            numStrWithMistake = -1;                     // строка с ошибкой (вершина с нечетной степенью)
            for (var i = 0; i < gr.GetLength(0); i++)
            {
                for (int j = 0; j < gr.GetLength(0); j++)
                {
                    if (gr[i, j] % 2 != 0)
                        countOdd++;
                }
                if (countOdd % 2 != 0)                 // если вершина не является четной
                {
                    numStrWithMistake = i;             // вершина, содержащая ошибку
                    return false;                      // граф не является эйлеровым
                }
                countOdd = 0;
            }
            return true;
        }

        // печать эйлерового цикла
        public static void ShowEulerCycle(Stack<int> cycle)
        {
            foreach (var c in cycle)
                Console.Write(c + " ");
            Console.WriteLine("\n");
        }
    }
}
