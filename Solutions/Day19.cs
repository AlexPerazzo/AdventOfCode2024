namespace AdventOfCode2024.Solutions
{
    public class Day19Solver
    {
        private string[] _lines;
        private List<string> _patterns = new();
        private List<string> _towelArrangements = new();

        public Day19Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            ProcessText();
        }

        public string SolvePart1()
        {
            var totalPossibleDesigns = 0;

            foreach (var towelArrangement in _towelArrangements) 
            {
                Console.WriteLine(totalPossibleDesigns);
                if (IsThereTowelCombo(towelArrangement))
                    totalPossibleDesigns++;
            }
            return totalPossibleDesigns.ToString();
        }

        private bool IsThereTowelCombo(string towelArrangement)
        {
            HashSet<int> canBeMade = new HashSet<int>();
            canBeMade.Add(towelArrangement.Length);
            int jGoal = towelArrangement.Length;

            for (var i = towelArrangement.Length; i > 0; i--)
            {
                jGoal -= 1;
                for (var j = jGoal; j >= 0; j--)
                {
                    var patternToCheck = towelArrangement[j..i];
                    if (_patterns.Contains(towelArrangement[j..i]) && canBeMade.Contains(i))
                    {
                        canBeMade.Add(j);
                    }
                }
            }
            return canBeMade.Contains(0);
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
                    _towelArrangements.Add(line);
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
