namespace AdventOfCode2024.Solutions
{
    public class Day07Solver
    {
        private string[] _lines;

        public Day07Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
        }

        public string SolvePart1()
        {
            var readyLines = ProcessFile();
            long count = 0;

            foreach ((long total, List<long> numbers) in readyLines) 
            {
                if (CanItBeSolved(total, numbers))
                    count += total;
            }


            return count.ToString();
        }
        public string SolvePart2()
        {
            var readyLines = ProcessFile();
            long count = 0;

            foreach ((long total, List<long> numbers) in readyLines)
            {
                if (CanItBeSolvedWithConcat(total, numbers))
                    count += total;
            }


            return count.ToString();
        }

        public bool CanItBeSolved(long total, List<long> numbers)
        {
            var number = numbers[0];
            if (numbers.Count == 1)
                if (total == number)
                    return true;
                else
                    return false;

            if (total % number == 0)
            {
                if (CanItBeSolved(total / number, numbers[1..numbers.Count()]))
                    return true;
                else
                    return (CanItBeSolved(total - number, numbers[1..numbers.Count()]));
            }
            else
                return (CanItBeSolved(total - number, numbers[1..numbers.Count()]));
        }

        public bool CanItBeSolvedWithConcat(long total, List<long> numbers)
        {
            var number = numbers[0];
            if (total < 0)
                return false;

            if (numbers.Count == 1)
                if (total == number)
                    return true;
                else
                    return false;

            if (total.ToString().EndsWith(number.ToString()))
            {
                if (number != total)
                {
                    if (CanItBeSolvedWithConcat(long.Parse(total.ToString().Substring(0, total.ToString().Length - number.ToString().Length)), numbers[1..numbers.Count()]))
                        return true;
                }
            }

            if (total % number == 0)
            {
                if (CanItBeSolvedWithConcat(total / number, numbers[1..numbers.Count()]))
                    return true;
                else
                    return (CanItBeSolvedWithConcat(total - number, numbers[1..numbers.Count()]));
            }
            else
                return (CanItBeSolvedWithConcat(total - number, numbers[1..numbers.Count()]));
        }


        public List<(long, List<long>)> ProcessFile()
        {
            List<(long, List<long>)> readyLines = new List<(long, List<long>)>();

            foreach (var line in _lines) 
            {
                var pieces = line.Split(": ");
                long total = long.Parse(pieces[0]);
                List<long> reverseNumbers = pieces[1].Split(" ").Select(long.Parse).Reverse().ToList();

                readyLines.Add((total, reverseNumbers));
            }

            return readyLines;
        }
    }
}
