using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions
{
    public class Day13Solver
    {
        private string[] _lines;
        private List<((long, long), (long, long), (long, long))> _allInfo = new();

        public Day13Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            ProcessText();

            

            /* 
             Find the best token to movement efficiency:
                Find which coordinate you need to move further.
                Divide the A movement of associated coordinate by 3 (movement per token)
                Compare to B's movement per token

            Whichever is cheaper:
                Take full coordinate spots mod A/B's movement
                Check if remainder is divisible by B/A
                If so, check if these totals work for the other coordinate (X/Y).
                If so, good to go
                Otherwise, subtract A/B's movement and go back to checking if remainder is divisble by B/A
             
Button A: X+33, Y+93

             */
        }

        private long GCD(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            if (a == 0)
                return b;
            else
                return a;
        }
        private long ProcessGame(((long, long) buttonA, (long, long) buttonB, (long, long) prizeLocation) game)
        {
            List<(long, long)> allOptions = new();

            long AbuttonX = game.buttonA.Item1;
            long BbuttonX = game.buttonB.Item1;
            long prizeDistanceX = game.prizeLocation.Item1;

            long AbuttonY = game.buttonA.Item2;
            long BbuttonY = game.buttonB.Item2;
            long prizeDistanceY = game.prizeLocation.Item2;

            long gcdX = GCD(AbuttonX, BbuttonX);
            long gcdY = GCD(AbuttonY, BbuttonY);

            if (prizeDistanceX % gcdX != 0 || prizeDistanceY % gcdY != 0)
                return 0;

            for (long i = 0; i <= 100; i++)
            {
                for (long j = 0; j <= 100; j++)
                {
                    if (AbuttonX * i + BbuttonX * j == prizeDistanceX && AbuttonY * i + BbuttonY * j == prizeDistanceY)
                    {
                        allOptions.Add((i, j));
                    }
                }
            }

            long cheapestCost = 999999999;
            foreach ((long howManyA, long howManyB) options in allOptions)
            {
                long tokenCost = options.howManyA * 3 + options.howManyB;
                if (tokenCost < cheapestCost)
                {
                    cheapestCost = tokenCost;
                }
            }

            return cheapestCost;
        }

        private long ProcessGamePart2(((long, long) buttonA, (long, long) buttonB, (long, long) prizeLocation) game)
        {

            List<(long, long)> allOptions = new();

            long AbuttonX = game.buttonA.Item1;
            long BbuttonX = game.buttonB.Item1;
            long prizeDistanceX = game.prizeLocation.Item1;

            long AbuttonY = game.buttonA.Item2;
            long BbuttonY = game.buttonB.Item2;
            long prizeDistanceY = game.prizeLocation.Item2;

            long gcdX = GCD(AbuttonX, BbuttonX);
            long gcdY = GCD(AbuttonY, BbuttonY);

            if (prizeDistanceX % gcdX != 0 || prizeDistanceY % gcdY != 0)
                return 0;


            long howManyTokens = 0;

            (bool aIsMoreEfficient, bool xIsBigger) = IsAMoreEfficient(game);
            ////focus on the furthest movement you need to go with the most efficient movement possible

            long buttonAdistance;
            long buttonBdistance;
            long prizeDistance;
            if (xIsBigger)
            {
                buttonAdistance = game.buttonA.Item1;
                buttonBdistance = game.buttonB.Item1;
                prizeDistance = game.prizeLocation.Item1;
            }
            else
            {
                buttonAdistance = game.buttonA.Item2;
                buttonBdistance = game.buttonB.Item2;
                prizeDistance = game.prizeLocation.Item2;
            }


            if (aIsMoreEfficient)
            {
                //Take prize's spots mod A/B's movement
                long howManyA = prizeDistance / buttonAdistance;
                long AmountLeftoverForB = prizeDistance % buttonAdistance;

                //Check if remainder is divisible by B / A
                while (AmountLeftoverForB % buttonBdistance != 0)
                {
                    AmountLeftoverForB += buttonAdistance;
                }

                if (AmountLeftoverForB % buttonBdistance == 0)
                {
                    //If so, check if these totals work for the other coordinate (X/Y).
                    long howManyB = AmountLeftoverForB / buttonBdistance;

                    if (xIsBigger)
                    {
                        if ((howManyA * game.buttonA.Item2) + (howManyB * game.buttonB.Item2) == game.prizeLocation.Item2)
                        {
                            howManyTokens = howManyA * 3 + howManyB;
                            return howManyTokens;
                        }
                    }
                }
            }

            return 0;
        }

        private (bool, bool) IsAMoreEfficient(((long, long) buttonA, (long, long) buttonB, (long, long) prizeLocation) game)
        {
            /* 
             Find the best token to movement efficiency:
                Find which coordinate you need to move further.
                Divide the A movement of associated coordinate by 3 (movement per token)
                Compare to B's movement per token
            */

            bool xIsBigger = false;
            bool aIsMoreEfficient = false;

            if (game.prizeLocation.Item1 > game.prizeLocation.Item2)
                xIsBigger = true;

            if (xIsBigger)
            {
                long aEfficency = game.buttonA.Item1 / 3;
                if (aEfficency > game.buttonB.Item1)
                    aIsMoreEfficient = true;
            }
            else
            {
                long aEfficency = game.buttonA.Item2 / 3;
                if (aEfficency > game.buttonB.Item2)
                    aIsMoreEfficient = true;
            }

            return (aIsMoreEfficient, xIsBigger);
        }

        private void ProcessText()
        {
            var piece = 0;

            (long, long) buttonA = (0, 0);
            (long, long) buttonB = (0, 0);
            (long, long) prize = (0, 0);


            foreach (var line in _lines) 
            {
                var match = Regex.Match(line, @"X[+=](\d+),\s*Y[+=](\d+)");

                long x = 0;
                long y = 0;

                if (string.IsNullOrEmpty(line))
                    continue;

                x = long.Parse(match.Groups[1].Value);
                y = long.Parse(match.Groups[2].Value);
                
                if (piece == 0)
                {
                    buttonA = (x, y);
                    piece++;
                }
                else if (piece == 1) 
                { 
                    buttonB = (x, y);
                    piece++;
                }
                else if (piece == 2)
                {
                    prize = (x, y);

                    _allInfo.Add((buttonA, buttonB, prize));

                    buttonA = (0, 0);
                    buttonB = (0, 0);
                    prize = (0, 0);

                    piece = 0;
                }
            }
        }

       

        public string SolvePart1()
        {
            long totalCost = 0;
            foreach (var game in _allInfo)
            {
                long tokenCost = ProcessGame(game);
                if (tokenCost != 999999999)
                {
                    totalCost += tokenCost;
                }
            }

            return totalCost.ToString();
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
