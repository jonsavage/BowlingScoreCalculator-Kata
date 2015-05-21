using System;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Versioning;
using System.Threading;

namespace BowlingKada
{
    public class BowlingScorer
    {
        private static int score = 0;
        private static int frame = 0;
        private static bool secondNumericalRollFlag = false;

        //resets static vars, we need these since they take up too many lines in ScoreLine
        public static void reset()
        {
            score = 0;
            frame = 0;
            secondNumericalRollFlag = false;
        }

        // Scores the entire bowling line
        public static int ScoreLine(string line)
        {
            for(int linePosition = 0; linePosition <= line.Length && frame <= 10; linePosition++)
            {
                score += ScoreChar(line, linePosition, false);
            }
            return score;
        }

        // Scores each individual roll by trying to score the roll as '[0-9]', '/' && 'X'
        private static int ScoreChar(string line, int linePosition, bool incrementFrame)
        {
            if (secondNumericalRollFlag && IsNumerical(line[linePosition], false))
            {
                frame++;
                secondNumericalRollFlag = false;
            }
            return ScoreNumerical(line, linePosition) + ScoreSpare(line, linePosition) + ScoreStrike(line, linePosition);
        }

        // Checks to see if the char at linePoition is a '/'
        // Returns score if it is, 0 otherwise.
        private static int ScoreSpare(string line, int linePosition)
        {

            if (IsSpare(line[linePosition]))
            {
                secondNumericalRollFlag = false;

                return 10 - (int)Char.GetNumericValue(line[linePosition - 1]) + ScoreChar(line, linePosition + 1, false);
            }
            return 0;
        }

        private static bool IsSpare(Char c)
        {
            if (c == '/')
            {
                secondNumericalRollFlag = false;
            }
            return c == '/';
        }

        private static bool IsStrike(Char c)
        {
            if (c == 'X')
            {
                secondNumericalRollFlag = false;
            }
            return c == 'X';
        }

        // Returns true if the passed Char is NOT '/' || 'X'
        private static bool IsNumerical(Char c, bool flag)
        {
            if (c != 'X' && c != '/' && flag)
            {
                handleNumerical();
            }
            return (c != 'X' && c != '/');
        }

        private static void handleNumerical()
        {
            if (secondNumericalRollFlag)
            {
                frame++;
            }
            secondNumericalRollFlag = !secondNumericalRollFlag;
        }

        // Checks to see if the char at linePoition is a '[0-9]'
        // Returns score if it is, 0 otherwise.
        private static int ScoreNumerical(string line, int linePosition)
        {
            if (IsNumerical(line[linePosition], true))
            {
                return (int) Char.GetNumericValue(line[linePosition]);
            }
            return 0;
        }

        // Checks to see if the char at linePoition is a 'X'
        // Returns score if it is, 0 otherwise.
        private static int ScoreStrike(string line, int linePosition)
        {
            if (IsStrike(line[linePosition]))
            {
                return 10 + ScoreChar(line, linePosition + 1, false) + ScoreChar(line, linePosition + 2, false);
            }
            return 0;
        }

        private static bool IsSecondNumericalRoll(string line, int linePosition)
        {
            if (linePosition > 0)
            {
                return IsNumerical(line[linePosition - 1], false) && IsNumerical(line[linePosition], false);
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