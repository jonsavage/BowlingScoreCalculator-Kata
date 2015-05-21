using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;

namespace BowlingKada
{
    public class BowlingScorer
    {
        private static int score = 0;
        private static int frame = 0;
        private static bool secondNumericalRollFlag = false;


        public static void reset()
        {
            score = 0;
            frame = 0;
            secondNumericalRollFlag = false;
        }


        public static int ScoreLine(string line)
        {
            for(int linePosition = 0; linePosition < line.Length && frame < 10; linePosition++)
            {
                score += ScoreChar(line, linePosition, false);
            }
            return score;
        }
        


        private static int ScoreChar(string line, int linePosition, bool incrementFrame)
        {
            if (incrementFrame || IsSecondNumbericalRoll(line, linePosition))
            {
                frame++;
            }
            return ScoreNumerical(line, linePosition) + ScoreSpare(line, linePosition) + ScoreStrike(line, linePosition);
        }

        private static int ScoreSpare(string line, int linePosition)
        {
            if (line[linePosition] == '/')
            {
                return 10 - (int)Char.GetNumericValue(line[linePosition - 1]) + ScoreChar(line, linePosition + 1, false);
            }
            return 0;
        }

        private static int ScoreNumerical(string line, int linePosition)
        {
            if (line[linePosition] != '/' && line[linePosition] != 'X')
            {
                return (int)Char.GetNumericValue(line[linePosition]);
            }
            return 0;
        }

        private static int ScoreStrike(string line, int linePosition)
        {
            if (line[linePosition] == 'X')
            {
                return 10 + ScoreChar(line, linePosition + 1, false) + ScoreChar(line, linePosition + 2, false);
            }
            return 0;
        }

        private static bool CharIsNumerical(Char c)
        {
            return c != 'X' && c != '/';
        }

        private static bool IsSecondNumbericalRoll(string line, int linePosition)
        {
            if (linePosition > 0)
            {
                return CharIsNumerical(line[linePosition - 1]) && CharIsNumerical(line[linePosition]);
            }
            return false;
        }


        
        public static int ScoreLineOld(string line)
        {
            var score = 0;
            var frame = 0;
            var secondNumericalRollFlag = false;

            for (var i = 0; i < line.Length && frame < 10; i++)
            {
                if (line[i] == '/')
                {
                    score = score - (int) Char.GetNumericValue(line[i - 1]) + 10;

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
                    else // X[0-9]/
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