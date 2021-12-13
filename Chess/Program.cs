using System;
using static Chess.Piece;
using System.Threading;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)    //[i,j] i est la colonne et j la ligne
        {
            Thread.Sleep(1000);
            Board b = new Board();
            //b.Print(false);
            int x = Bench(b, 4,false,team.white);
            Console.Write(x);
        }
    }
}