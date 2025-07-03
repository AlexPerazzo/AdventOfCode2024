using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2024.Solutions
{
    public class Day11Solver
    {
        private string[] _lines;
        private List<long> _holderOne = new([3028, 78, 973951, 5146801, 5, 0, 23533, 857]);
        private List<long> _holderTwo = new();
        private Dictionary<long, List<long>> _memoization = new();

        public Day11Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
        }

        public string SolvePart1()
        {
            for (var i = 0; i < 25; i++)
            {
                if (i % 2 == 0)
                    _holderTwo = TransformList(_holderOne);
                else
                    _holderOne = TransformList(_holderTwo);
            }

            return _holderTwo.Count.ToString();
        }

        private List<long> TransformList(List<long> source)
        {
            var result = new List<long>();

            foreach (var number in source)
            {
                var strNum = number.ToString();
                var length = strNum.Length;

                if (number == 0)
                {
                    result.Add(1);
                }
                else if (length % 2 == 0)
                {
                    var halfway = length / 2;

                    result.Add(long.Parse(strNum[..halfway]));
                    result.Add(long.Parse(strNum[halfway..]));
                }
                else
                {
                    result.Add(number * 2024);
                }
            }

            return result;
        }

        private List<long> TransformListWithMemo(List<long> source)
        {
            var result = new List<long>();

            foreach (var number in source)
            {
                if (_memoization.ContainsKey(number))
                {
                    foreach (var item in _memoization[number])
                    {
                        result.Add(item);
                    }
                }
                else
                {
                    var strNum = number.ToString();
                    var length = strNum.Length;

                    if (number == 0)
                    {
                        result.Add(1);

                        _memoization[number] = [1];
                    }
                    else if (length % 2 == 0)
                    {
                        var halfway = length / 2;

                        var first = long.Parse(strNum[..halfway]);
                        var second = long.Parse(strNum[halfway..]);

                        result.Add(first);
                        result.Add(second);

                        _memoization[number] = [first, second];
                    }
                    else
                    {
                        var newRock = number * 2024;
                        result.Add(newRock);

                        _memoization[number] = [newRock];
                    }

                }

            }

            return result;
        }


        public string SolvePart2()
        {
            for (var i = 0; i < 75; i++)
            {
                if (i % 2 == 0)
                    _holderTwo = TransformList(_holderOne);
                else
                    _holderOne = TransformList(_holderTwo);
                Console.WriteLine(i);
            }

            return _holderTwo.Count.ToString();
        }
    }
}
