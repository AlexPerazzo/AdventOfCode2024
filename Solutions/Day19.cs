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
            //Recursive
            //Check if pattern of first letter matches
            //If so, call again with shorter string
            //If not, check first two letters
            //So on and so forth
            //If it passed at any point, good to go
            //If not, bubble up false

            //Base Case
            if (_patterns.Contains(towelArrangement))
                return true;

            for (var i = 1; i <= towelArrangement.Length; i++)
            {
                if (_patterns.Contains(towelArrangement[..i]))
                {
                    if (IsThereTowelCombo(towelArrangement[i..]))
                        return true;
                }
            }

            return false;
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
