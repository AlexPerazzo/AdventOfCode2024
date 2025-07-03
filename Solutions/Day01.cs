namespace AdventOfCode2024.Solutions;

public class Day01Solver
{
    private string[] _lines;

    public Day01Solver(string inputPath)
    {
        _lines = File.ReadAllLines(inputPath);
    }

    public string SolvePart1()
    {
        List<string> leftColumn = new List<string>();
        List<string> rightColumn = new List<string>();

        foreach (var line in _lines)
        {
            string[] parts = line.Split("   ", 2);
            leftColumn.Add(parts[0]);
            rightColumn.Add(parts[1]);
        }

        Console.WriteLine(leftColumn.Count);

        leftColumn.Sort();
        rightColumn.Sort();

        int total = 0;

        Console.WriteLine(leftColumn.Count);

        for (int i = 0; i < leftColumn.Count; i++)
        {
            var distance = int.Parse(leftColumn[i]) - int.Parse(rightColumn[i]);
            var distance2 = Math.Abs(distance);
            total += distance2;
        }

        return total.ToString();
    }
    public string SolvePart2()
    {
        List<string> leftColumn = new List<string>();
        List<string> rightColumn = new List<string>();

        foreach (var line in _lines)
        {
            string[] parts = line.Split("   ", 2);
            leftColumn.Add(parts[0]);
            rightColumn.Add(parts[1]);
        }

        Console.WriteLine(leftColumn.Count);

        leftColumn.Sort();
        rightColumn.Sort();

        int total = 0;

        Console.WriteLine(leftColumn.Count);

        for (int i = 0; i < leftColumn.Count; i++)
        {
            var totalRepeats = rightColumn.Count(number => number == leftColumn[i]);
            var similarity = int.Parse(leftColumn[i]) * totalRepeats;
            total += similarity;
        }

        return total.ToString();
    }
}

