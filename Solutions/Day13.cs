using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions
{
    public class Day13Solver
    {
        private string[] _lines;
        public List<((int, int), (int, int), (int, int))> _allInfo = new();

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

        public int ProcessGame(((int, int) buttonA, (int, int) buttonB, (int, int) prizeLocation) game)
        {
            List<(int, int)> allOptions = new();

            int AbuttonX = game.buttonA.Item1;
            int BbuttonX = game.buttonB.Item1;
            int prizeDistanceX = game.prizeLocation.Item1;

            int AbuttonY = game.buttonA.Item2;
            int BbuttonY = game.buttonB.Item2;
            int prizeDistanceY = game.prizeLocation.Item2;


            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100; j++)
                {
                    if (AbuttonX * i + BbuttonX * j == prizeDistanceX && AbuttonY * i + BbuttonY * j == prizeDistanceY)
                    {
                        allOptions.Add((i, j));
                    }
                }
            }

            int cheapestCost = 999999999;
            foreach ((int howManyA, int howManyB) options in allOptions)
            {
                int tokenCost = options.howManyA * 3 + options.howManyB;
                if (tokenCost < cheapestCost)
                {
                    cheapestCost = tokenCost;
                }
            }

            return cheapestCost;
        }

        //public int ProcessGame(((int, int) buttonA, (int, int) buttonB, (int, int) prizeLocation) game)
        //{
        //    int howManyTokens = 0;

        //    (bool aIsMoreEfficient, bool xIsBigger) = IsAMoreEfficient(game);
        //    int buttonAdistance;
        //    int buttonBdistance;
        //    int prizeDistance;
        //    //focus on the furthest movement you need to go with the most efficient movement possible
        //    if (xIsBigger) 
        //    {
        //        buttonAdistance = game.buttonA.Item1;
        //        buttonBdistance = game.buttonB.Item1;
        //        prizeDistance = game.prizeLocation.Item1;
        //    }
        //    else
        //    {
        //        buttonAdistance = game.buttonA.Item2;
        //        buttonBdistance = game.buttonB.Item2;
        //        prizeDistance = game.prizeLocation.Item2;
        //    }


        //    if (aIsMoreEfficient) 
        //    {
        //        //Take prize's spots mod A/B's movement
        //        int howManyA = prizeDistance / buttonAdistance;
        //        int AmountLeftoverForB = prizeDistance % buttonAdistance;

        //        int counter = 0;

        //        //Check if remainder is divisible by B / A
        //        while (AmountLeftoverForB % buttonBdistance != 0 && counter <= 100)
        //        {
        //            AmountLeftoverForB += buttonAdistance;
        //            counter++;
        //        }

        //        if (AmountLeftoverForB % buttonBdistance == 0) 
        //        { 
        //            //If so, check if these totals work for the other coordinate (X/Y).
        //            int howManyB = AmountLeftoverForB / buttonBdistance;

        //            if (xIsBigger)
        //            {
        //                if ((howManyA * game.buttonA.Item2) + (howManyB * game.buttonB.Item2) == game.prizeLocation.Item2)
        //                {
        //                    howManyTokens = howManyA * 3 + howManyB;
        //                    return howManyTokens;
        //                }
        //            }
        //        }
        //    }

        //    return 0;
        //}

        //public (bool, bool) IsAMoreEfficient(((int, int) buttonA, (int, int) buttonB, (int, int) prizeLocation) game)
        //{
        //    /* 
        //     Find the best token to movement efficiency:
        //        Find which coordinate you need to move further.
        //        Divide the A movement of associated coordinate by 3 (movement per token)
        //        Compare to B's movement per token
        //    */

        //    bool xIsBigger = false;
        //    bool aIsMoreEfficient = false;

        //    if (game.prizeLocation.Item1 > game.prizeLocation.Item2)
        //        xIsBigger = true;

        //    if (xIsBigger)
        //    {
        //        int aEfficency = game.buttonA.Item1 / 3;
        //        if (aEfficency > game.buttonB.Item1)
        //            aIsMoreEfficient = true;
        //    }
        //    else
        //    {
        //        int aEfficency = game.buttonA.Item2 / 3;
        //        if (aEfficency > game.buttonB.Item2)
        //            aIsMoreEfficient = true;
        //    }

        //    return (aIsMoreEfficient, xIsBigger);
        //}

        public void ProcessText()
        {
            var piece = 0;

            (int, int) buttonA = (0, 0);
            (int, int) buttonB = (0, 0);
            (int, int) prize = (0, 0);


            foreach (var line in _lines) 
            {
                var match = Regex.Match(line, @"X[+=](\d+),\s*Y[+=](\d+)");

                int x = 0;
                int y = 0;

                if (string.IsNullOrEmpty(line))
                    continue;

                x = int.Parse(match.Groups[1].Value);
                y = int.Parse(match.Groups[2].Value);
                
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
            int totalCost = 0;
            foreach (var game in _allInfo)
            {
                int tokenCost = ProcessGame(game);
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
