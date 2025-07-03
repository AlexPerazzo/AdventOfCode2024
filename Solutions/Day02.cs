namespace AdventOfCode2024.Solutions;
public class Day02Solver
{
    private string[] _lines;

    public Day02Solver(string inputPath)
    {
        _lines = File.ReadAllLines(inputPath);
    }

    public string SolvePart1()
    {
        int totalSafe = 0;

        foreach (var line in _lines)
        {
            List<string> levels = line.Split(" ").ToList();

            bool ascending = true;
            bool descending = true;
            bool safe = true;

            for (int i = 1; i < levels.Count; i++)
            {
                string previousLevel = levels[i - 1];

                var difference = int.Parse(levels[i]) - int.Parse(previousLevel);

                if (difference >= 1 && difference <= 3 && ascending)
                    descending = false;
                else if (difference <= -1 && difference >= -3 && descending)
                    ascending = false;
                else
                {
                    safe = false;
                    break;
                }

            }

            if (safe)
                totalSafe++;
        }

        return totalSafe.ToString();
    }

    public string SolvePart2()
    {
        int totalSafe = 0;
        using (StreamWriter writer = new StreamWriter("C:\\Users\\alexa\\OneDrive\\Desktop\\AdventOfCode2024\\Inputs\\mytest.txt"))
        {
            
        
        foreach (var line in _lines)
        {
            bool safe = false;
            List<string> levels = line.Split(" ").ToList();

            if (IsItSafe(levels, 0))
                safe = true;
            
            //for (var i = 0; i < levels.Count; i++)
            //{
            //    List<string> theCombo = new List<string>();
            //    foreach (var level in levels)
            //    {
            //        theCombo.Add(level);
            //    }

            //    theCombo.RemoveAt(i);

            //    if (IsItSafeOld(theCombo, 0))
            //        safe = true;
            //}

            if (safe)
                {
                    writer.WriteLine("True");
                totalSafe++;
                }
                else
                {
                    writer.WriteLine("False");
                }
        }

        }
        return totalSafe.ToString();
    }

    public bool IsItSafe(List<string> levels, int recursionCount)
    {
        bool ascending = true;
        bool descending = true;
        bool safe = true;

        for (int i = 1; i < levels.Count; i++)
        {
            string currentLevel = levels[i];
            string previousLevel = levels[i - 1];

            var difference = int.Parse(currentLevel) - int.Parse(previousLevel);

            if (difference >= 1 && difference <= 3 && ascending)
                descending = false;

            else if (difference <= -1 && difference >= -3 && descending)
                ascending = false;

            else
            {
                if (recursionCount == 0)
                {
                    List<string> otherVersion = new List<string>();

                    foreach (var level in levels)
                    {
                        otherVersion.Add(level);
                    }

                    levels.RemoveAt(i);
                    otherVersion.RemoveAt(i - 1);

                    if (IsItSafe(levels, 1) || IsItSafe(otherVersion, 1))
                    {
                        safe = true;
                        break;
                    }

                    safe = false;
                    break;
                }
                else
                {
                    safe = false;
                    break;
                }
            }

        }
        return safe;
    }

    public bool IsItSafeOld(List<string> levels, int recursionCount)
    {
        bool ascending = true;
        bool descending = true;
        bool safe = true;

        for (int i = 1; i < levels.Count; i++)
        {
            string currentLevel = levels[i];
            string previousLevel = levels[i - 1];

            var difference = int.Parse(currentLevel) - int.Parse(previousLevel);

            if (difference >= 1 && difference <= 3 && ascending)
                descending = false;

            else if (difference <= -1 && difference >= -3 && descending)
                ascending = false;

            else
            {
                    safe = false;
                    break;
            }

        }
        return safe;
    }


    string[] _bruteLines;
    string[] _myLines;
    public string IWantToKnowThis()
    {
        var brutePath = "C:\\Users\\alexa\\OneDrive\\Desktop\\AdventOfCode2024\\Inputs\\brutetest.txt";
        _bruteLines = File.ReadAllLines(brutePath);

        var myPath = "C:\\Users\\alexa\\OneDrive\\Desktop\\AdventOfCode2024\\Inputs\\mytest.txt";
        _myLines = File.ReadAllLines(myPath);

        var numOfLines = _myLines.Length;

        for (int i = 0; i < numOfLines; i++)
        {
            if (_myLines[i] != _bruteLines[i])
                return i.ToString();
        }
        return "negatory";
    }
}

