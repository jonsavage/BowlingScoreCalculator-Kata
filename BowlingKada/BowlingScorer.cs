using System;
using System.Threading;

namespace BowlingKada
{
    public class BowlingScorer
    {
        public static int ScoreLine(string line)
        {
            var score = 0;
            var lookahead = 0;
            var frame = 0;
            

            for (var i = 0; i < line.Length; i++)
            {
                if (lookahead > 0)
                {
                    if (line[i].Equals('X'))
                    {
                        score += 10;
                    }
                    score += (int)(char.GetNumericValue(line[i]));
                    
                    lookahead--;

                    if (i == line.Length - 1)
                    {
                        break;
                    }
                }
                if (line[i].Equals('-')) // Miss '-'
                {
                    
                }
                else if (line[i].Equals('/')) // Spare '/'
                {
                    score += 10 - (int)(char.GetNumericValue(line[i-1]));
                    lookahead = 1;
                }
                else if (line[i].Equals('X')) // Strike 'X' 
                {
                    score += 10;
                    if (i <= line.Length - 1 && line[i + 1].Equals('X')) // X @ i + 1
                    {
                        score += 10; 
                        if (i <= line.Length - 2 && line[i + 2].Equals('X')) // X @ i + 2
                        {
                            score += 10; 
                        }
                        else // Add 
                        {
                            score += (int) (char.GetNumericValue(line[i + 1]));
                            score += (int)(char.GetNumericValue(line[i + 2]));
                        }
                    }
                    else if (i < line.Length - 2 && line[i + 2].Equals('/'))
                    {
                        score += 10;
                    }
                    else if(i < line.Length - 2)
                    {
                        score += (int)(char.GetNumericValue(line[i + 1]));
                        score += (int)(char.GetNumericValue(line[i + 2])); 
                    }
                    else if (i < line.Length - 1)
                    {
                        score += (int)(char.GetNumericValue(line[i + 1]));
                    }
//                    lookahead = 2;
                }
                else // numerical 0-9 
                {
                    score += (int) (char.GetNumericValue(line[i]));
                }
                Console.WriteLine(score);
            }

            return score;
        }
    }
}