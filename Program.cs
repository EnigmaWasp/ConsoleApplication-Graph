using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
// задание 1.
namespace ConsoleApplication1  // создание ребер графа.
{
    class Program
    {
        public struct Duga//Структура для слов
        {
            internal int v1;
            internal int v2;

            public Duga(int i1, int i2)// конструктор структуры
            {
                v1 = i1;
                v2 = i2;
            }
        }
        public static int cmp(Duga t1, Duga t2)// функция сравнения для структуры
        {
            int result = t1.v1.CompareTo(t2.v1);// вначале сравниваются слова
            if (result != 0) return result;
            return t1.v2.CompareTo(t2.v2);// затем номера файлов
        }
        public static int[] FindWay(List<Duga> Li, int[] ukaz,  int BonOr, int Suchi ,int Scan, int n )

        {
            int[] Vert = new int[n + 1];
            int[] fromVert = new int[n + 1];
            int[] VertList = new int[n + 1];
            int countList, countListTmp, Cvert, tmp = 0;
                    for (int j = 1; j < n + 1; j++)
                    {
                        fromVert[j] = 0;
                        fromVert[j] = 0;
                        Vert[j] = 0;
                    }
                    Console.WriteLine("Путь " + Scan);
                    Vert[Scan] = -1;
                    fromVert[Suchi] = Suchi;
                    countList = 1;
                    VertList[countList] = Suchi;
                    bool OutFlag = false;
                    countListTmp = countList;
                    while (true)
                    {
                        Cvert = VertList[countListTmp];
                        for (int j = ukaz[Cvert]; j < ukaz[Cvert + 1]; j++)
                        {
                            if (Vert[Li[j].v2] == -1) continue;
                            tmp = Li[j].v2;

                            if (Vert[tmp] == 0)
                            {
                                Vert[tmp] = 1;
                                fromVert[tmp] = Cvert;
                                countList++;
                                VertList[countList] = tmp;
                            }
                            if (tmp == BonOr)
                            {
                                OutFlag = true;
                                break;
                            }
                        }
                        countListTmp++;
                        if (countListTmp > countList)
                        {
                            Console.WriteLine("Пути нет");
                            break;
                        }
                        if (OutFlag)
                        {
                            while (true)
                            {
                                Console.WriteLine("Путь Вершина " + tmp + "  " + BonOr);
                                Console.ReadKey();
                                if (tmp == Suchi) break;
                                tmp = fromVert[tmp];
                            }
                            Console.WriteLine("Путь End ");
                            break;
                        }

                    }
            return fromVert;
                }
  

        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture =
            new System.Globalization.CultureInfo("en-US");  // без этого получаются запятые вместо точек.
            Console.Title = "Работа с файлами и сортировка"; // заголовок для консоли
            Console.BackgroundColor = ConsoleColor.DarkYellow;// цвет фона консоли
            Console.ForegroundColor = ConsoleColor.Red;// цвет символов консоли
            Console.Clear();// Очистка поля консоли
            Random gen = new Random();
            List<Duga> Li = new List<Duga>(); // создается объект Li типа List для формирования списка структур OutItem
            Duga ll, ll1; // одиночная структура
            int i1, i2, n, mr, ii;
            bool flag;
            Console.WriteLine("введите число вершин и число ребер графа");
            n = int.Parse(Console.ReadLine());
            mr = int.Parse(Console.ReadLine());
            for (int i = 0; i < mr; i++)
            {
                while (true)
                {
                    i1 = gen.Next(1, n + 1);
                    i2 = gen.Next(1, n + 1);
                    flag = true;
                    if (i1 == i2) flag = false;
                    for (ii = 0; ii < Li.Count; ii++)
                    {
                        if (i1 == Li[ii].v1 && i2 == Li[ii].v2)
                            flag = false;
                    }
                    if (flag) break;
                }

                ll = new Duga(i1, i2);
                ll1 = new Duga(i2, i1);
                Li.Add(ll);
                Li.Add(ll1);
            }
            ll = new Duga(n + 1, -1);
            Li.Add(ll);
            Li.Sort(cmp);
            for (int i = 0; i < Li.Count; i++)
                Console.WriteLine(" номер  {0:} ребро {1:} {2:} ", i, Li[i].v1, Li[i].v2);
            int[] ukaz = new int[n + 2];
            int step = 0, previ = 1;
            ukaz[1] = 0;
            while (step < Li.Count)
            {
                if (Li[step].v1 != previ)
                {
                    previ++;
                    ukaz[previ] = step;
                }
                else step++;
            }
            ukaz[n + 1] = Li.Count - 1;
            for (int i = 1; i <= n + 1; i++)
                Console.WriteLine(" указатель {0:} {1:} ", i, ukaz[i]);
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine();
                for (int j = ukaz[i]; j < ukaz[i + 1]; j++)
                    Console.WriteLine(" ребро {0:} {1:} {2:} ", j, Li[j].v1, Li[j].v2);
            }

            Console.WriteLine("введите бони клайд суши в 3 строки");
            int bon = int.Parse(Console.ReadLine());
            int klaid = int.Parse(Console.ReadLine());
            int Suchi = int.Parse(Console.ReadLine());

            for (int Scan = 1; Scan < n + 1; Scan++)
            {
                if (Scan != bon && Scan != klaid && Scan != Suchi)
                {
                    FindWay(Li, ukaz, bon, Suchi, Scan, n);

                    Console.WriteLine("Путь " + Scan);
                }
                else continue;
            }

            Console.WriteLine("Нажми любую клавишу.");
            Console.ReadKey();// приостановка закрытия окна консоли
        }
    }
}

