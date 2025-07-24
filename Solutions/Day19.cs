namespace AdventOfCode2024.Solutions
{
    public class Day19Solver
    {
        private string[] _lines;
        private List<string> _patterns = new();
        private List<string> _towels = new();

        public Day19Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            ProcessText();
        }

        public string SolvePart1()
        {
            return "";
        }

        private void ProcessText()
        {
            bool isTowel = false;

            foreach (var line in _lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    isTowel = true;
                }

                else if (isTowel)
                {
                    _towels.Add(line);
                }

                else
                {
                    _patterns = line.Split(", ").ToList();
                }
            }
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
