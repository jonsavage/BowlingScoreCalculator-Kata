﻿using System;
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
        private static int frame = -1;
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
            for (int linePosition = 0; linePosition <= line.Length && frame < 10; linePosition++)
            {
                score += ScoreChar(line, linePosition, false);
            }
            return score;
        }

        // Scores each individual roll by trying to score the roll as '[0-9]', '/' && 'X'
        private static int ScoreChar(string line, int linePosition, bool incrementFrame)
        {
            if (secondNumericalRollFlag && IsNumerical(line[linePosition]))
            {
                //                frame++;
            }
            return ScoreNumericalWrapper(line, linePosition) + ScoreSpareWrapper(line, linePosition) + ScoreStrikeWrapper(line, linePosition);
        }

        // Checks to see if the char at linePoition is a '/'
        // Returns score if it is, 0 otherwise.
        private static int ScoreSpareWrapper(string line, int linePosition)
        {

            if (IsSpare(line[linePosition]))
            {
                return ScoreSpareInner(line, linePosition);
            }
            return 0;
        }

        private static int ScoreSpareInner(string line, int linePosition)
        {
            frame++;
            secondNumericalRollFlag = false;
            return 10 - (int)Char.GetNumericValue(line[linePosition - 1]) + ScoreLookaheadChar(line, linePosition + 1);
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
        private static bool IsNumerical(Char c)
        {
            return c != 'X' && c != '/';
        }



        // Checks to see if the char at linePoition is a '[0-9]'
        // Returns score if it is, 0 otherwise.
        private static int ScoreNumericalWrapper(string line, int linePosition)
        {
            if (IsNumerical(line[linePosition]))
            {
                return ScoreNumericalInner(line, linePosition);
            }
            return 0;
        }

        private static int ScoreNumericalInner(string line, int linePosition)
        {
            trackDoubleNumerical();
            return (int)Char.GetNumericValue(line[linePosition] == '-' ? '0' : line[linePosition]);
        }

        private static void trackDoubleNumerical()
        {
            if (secondNumericalRollFlag)
            {
                frame++;
            }
            secondNumericalRollFlag = !secondNumericalRollFlag;
        }



        // Checks to see if the char at linePoition is a 'X'
        // Returns score if it is, 0 otherwise.
        private static int ScoreStrikeWrapper(string line, int linePosition)
        {
            if (IsStrike(line[linePosition]))
            {
                return ScoreStrikeInner(line, linePosition);
            }
            return 0;
        }

        private static int ScoreStrikeInner(string line, int linePosition)
        {
            frame++;
            secondNumericalRollFlag = false;
            return 10 + ScoreLookaheadChar(line, linePosition + 1) + ScoreLookaheadChar(line, linePosition + 2);

        }

        private static bool IsSecondNumericalRoll(string line, int linePosition)
        {
            if (linePosition > 0)
            {
                return IsNumerical(line[linePosition - 1]) && IsNumerical(line[linePosition]);
            }
            return false;
        }

        private static int ScoreLookaheadChar(string line, int linePosition)
        {
            return ScoreLookaheadNumerical(line, linePosition) + ScoreLookaheadSpare(line, linePosition) + ScoreLookaheadStrike(line, linePosition);
        }

        private static int ScoreLookaheadSpare(string line, int linePosition)
        {
            if (IsSpare(line[linePosition]))
            {

                return 10 - (int)Char.GetNumericValue(line[linePosition - 1]);
            }
            return 0;
        }

        private static int ScoreLookaheadStrike(string line, int linePosition)
        {
            if (IsStrike(line[linePosition]))
            {
                return 10;
            }
            return 0;
        }

        private static int ScoreLookaheadNumerical(string line, int linePosition)
        {
            if (IsNumerical(line[linePosition]))
            {
                return (int)Char.GetNumericValue(line[linePosition]);
            }
            return 0;
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
                    score = score - (int)Char.GetNumericValue(line[i - 1]) + 10;

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
                            score += (int)Char.GetNumericValue(line[i + 2]);
                        }
                    }
                    else if (line[i + 2] != '/') // X[0-9][0-9]
                    {
                        score += (int)Char.GetNumericValue(line[i + 1]);
                        score += (int)Char.GetNumericValue(line[i + 2]);
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
                    score += (int)Char.GetNumericValue(line[i]);

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