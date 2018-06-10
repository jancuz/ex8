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
                    var numTry = 16;                       // 16 попыток

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

        // создание случайной вершшины, не являющейся заданной
        private static int RandomVertex(int countVertex, int startVertex)
        {
            int ans = rnd.Next(countVertex);
            while (ans == startVertex)
                ans = rnd.Next(countVertex);
            return ans;
        }
    }
}
