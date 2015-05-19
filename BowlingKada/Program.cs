using System;
using System.IO;


namespace BowlingKada
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var score = BowlingScorer.ScoreLine(Console.ReadLine());
            
            Console.WriteLine(score);

            Console.ReadLine();
        }
       
    }
}