using System;
using System.Collections.Generic;
using MyLibrary;

namespace ex8
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("Введите номер пункта, который хотите выполнить:");
            Console.WriteLine("1. Ввести граф с клавиатуры");
            Console.WriteLine("2. Создать граф с помощью ДСЧ");
            Console.WriteLine("3. Выход");
        }

        static void Run()
        {
            int checkRun = 0, left = 1, right = 3;
            do
            {
                PrintMenu();
                checkRun = AskData.ReadIntNumber("Введите номер пункта, который хотите выполнить", left, right);
                switch (checkRun)
                {
                    case 1: // ввод графа с клавиатуры
                        {
                            Graph.FormGraph(out int[,] graph);                              // формирование графа
                            Graph.ShowGraph(graph);                                         // печать графа
                            int countVert = graph.GetLength(0);                             // кол-во вершин в графе
                            int startVert = AskData.ReadIntNumber("Введите номер вершины, от которой требуется найти эйлеров цикл:", 1, countVert) - 1;
                            Stack<int> res = new Stack<int>();                              // эйлеров цикл
                            Graph.EulerCycle(startVert, ref res, countVert, ref graph);     // поиск эйлерового цикла
                            Graph.ShowEulerCycle(res);                                      // печать цикла
                            break;
                        }

                    case 2: // формирование графа с помощью ДСЧ
                        {
                            Graph.FormRandomGraph(out int[,] graph);                        // формирование графа
                            Graph.ShowGraph(graph);                                         // печать графа
                            int countVert = graph.GetLength(0);                             // кол-во вершин в графе
                            int startVert = AskData.ReadIntNumber("Введите номер вершины, от которой требуется найти эйлеров цикл:", 1, countVert) - 1;
                            Stack<int> res = new Stack<int>();                              // эйлеров цикл
                            Graph.EulerCycle(startVert, ref res, countVert, ref graph);     // поиск эйлерового цикла
                            Graph.ShowEulerCycle(res);                                      // печать цикла
                            break;
                        }

                }
            } while (checkRun != right);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Поиск Эйлерова цикла в графе");
            Run();
        }
    }
}
