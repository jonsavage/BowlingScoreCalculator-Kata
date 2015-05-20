using System;
using System.Threading;

namespace BowlingKada
{
    public class BowlingScorer
    {
        public static int ScoreLine(string line)
        {
            var score = 0;
            var frame = 0;
            var secondNumericalRollFlag = false;

            for (var i = 0; i < line.Length && frame < 10; i++)
            {
                if (line[i] == '/')
                {
                    score -= (int) Char.GetNumericValue(line[i - 1]);
                    score += 10;

                    if (line[i + 1] == 'X')
                    {
                        score += 10;
                    }
                    else // Must be numerical
                    {
                        score += (int)Char.GetNumericValue(line[i + 1]);
                    }

                    secondNumericalRollFlag = false;
                    frame++;
                }
                else if (line[i] == 'X') // X??
                {
                    score += 10;
                    if (line[i + 1] == 'X') // XX?
                    {
                        score += 10;
                        if (line[i + 2] == 'X') // XXX
                        {
                            score += 10;
                        }
                        else // XX[0-9]
                        {
                            score += (int) Char.GetNumericValue(line[i + 2]);
                        }
                    }
                    else if (line[i + 2] != '/') // X[0-9][0-9]
                    {
                        score += (int) Char.GetNumericValue(line[i + 1]);
                        score += (int) Char.GetNumericValue(line[i + 2]);
                    }
                    else
                    {
                        score += 10;
                    }

                    secondNumericalRollFlag = false;
                    frame++;
                }
                else if (line[i] != '-')
                {
                    score += (int) Char.GetNumericValue(line[i]);

                    if (secondNumericalRollFlag)
                    {
                        frame++;
                        secondNumericalRollFlag = false;
                    }
                    else
                    {
                        secondNumericalRollFlag = true;
                    }
                }
            }
            return score;
        }
    }
}