using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021aoc13
{
    class Program
    {
        class Fólia
        {
            HashSet<Pont> pontok;
            IEnumerable<(char, int)> utasítások;
            public Fólia(string path)
            {
                pontok = System.IO.File.ReadAllLines(path+"_points.txt").Select(s => s.Split(',')).Select(t => new Pont(t)).ToHashSet();
                utasítások = System.IO.File.ReadAllLines(path+"_instructions.txt").Select(s => s.Split('=')).Select(t=>(t[0].Last(), int.Parse(t[1])));
            }
            struct Pont
            {
                public int x, y;
                public Pont(string[] t) : this(int.Parse(t[0]), int.Parse(t[1])) { }
                public Pont(int x, int y) => (this.x, this.y) = (x, y);
                public Pont Behajtása(char ko, int poz) => ko == 'x' ? (poz < x ? new Pont(2 * poz - x, y) : this) : (poz < y ? new Pont(x, 2 * poz - y) : this);
            }
            public void Hajtogatás() { Diagnosztika(); foreach ((char, int) utasítás in utasítások) Végrehajt(utasítás); }
            private void Végrehajt((char, int) utasítás)
            {
                (char koordináta, int pozíció) = utasítás;
                HashSet<Pont> új_pontok = new HashSet<Pont>();
                foreach (Pont pont in pontok)
                    új_pontok.Add(pont.Behajtása(koordináta, pozíció));
                pontok = új_pontok;
                Diagnosztika($"fold along { koordináta}={ pozíció}");
            }
            public void Diagnosztika(string megj = "")
            {
                int N = pontok.Max(p => p.x)+1;
                int M = pontok.Max(p => p.y)+1;
                bool[,] rajzmátrix = new bool[N, M];
                foreach (Pont pont in pontok)
                    rajzmátrix[pont.x, pont.y] = true;
                Console.WriteLine("\n\n\n\n---- " + megj+" ----\n");
                if (N<128)
                {
                    for (int j = 0; j < M; j++)
                    {
                        for (int i = 0; i < N; i++)
                            Console.Write(rajzmátrix[i,j]?"#":".");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine($"\n---- pontok száma így: {pontok.Count} ----");
            }
        }
        static void Main(string[] args)
        {
            /** /
            Fólia fólia = new Fólia("teszt");
            /*/
            Fólia fólia = new Fólia("input");
            /**/

            fólia.Hajtogatás();
            Console.ReadKey();
        }
    }
}
