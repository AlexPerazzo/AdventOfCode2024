namespace AdventOfCode2024.Solutions
{
    public class Day08Solver
    {
        private string[] _lines;
        public Dictionary<char, List<(int y, int x)>> charPlacements = new Dictionary<char, List<(int y, int x)>>();


        public Day08Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            MakeCateogries();
        }

        public string SolvePart1()
        {
            var allAntinodes = new HashSet<(int y, int x)>();

            foreach (var coordinateList in charPlacements.Values)
            {
                foreach ((int y, int x) in coordinateList)
                {
                    
                    foreach ((int y2, int x2) in coordinateList)
                    {
                        if ((y, x) != (y2, x2))
                        {
                            var diffY = y - y2;
                            var diffX = x - x2;

                            var NewY1 = y + diffY;
                            var NewX1 = x + diffX;
                            var NewY2 = y2 - diffY;
                            var NewX2 = x2 - diffX;

                            if (NewY1 >= 0 && NewY1 <= 49 && NewX1 >= 0 && NewX1 <= 49)
                                allAntinodes.Add((NewY1, NewX1));
                            if (NewY2 >= 0 && NewY2 <= 49 && NewX2 >= 0 && NewX2 <= 49)
                                allAntinodes.Add((NewY2, NewX2));
                        }

                        
                    }
                }
            }
            return allAntinodes.Count.ToString();
        }

        public void MakeCateogries()
        {
            var y = -1;
            foreach (var line in _lines) 
            {
                var x = -1;
                y += 1;

                foreach(var character in line) 
                {
                    x += 1;

                    if (character != '.')
                    {
                        if (!charPlacements.ContainsKey(character))
                            charPlacements[character] = new List<(int x, int y)>();

                        charPlacements[character].Add((x, y));
                    }
                }
            }
        }

        public string SolvePart2()
        {
            var allAntinodes = new HashSet<(int y, int x)>();

            foreach (var coordinateList in charPlacements.Values)
            {
                foreach ((int y, int x) in coordinateList)
                {
                    foreach ((int y2, int x2) in coordinateList)
                    {
                        if ((y, x) != (y2, x2))
                        {
                            var diffY = y - y2;
                            var diffX = x - x2;

                            var counter = 0;


                            while (y + diffY * counter >= 0 && y + diffY * counter <= 49 && x + diffX * counter >= 0 && x + diffX * counter <= 49)
                            {
                                allAntinodes.Add((y + diffY * counter, x + diffX * counter));
                                counter += 1;
                            }

                            counter = 0;

                            while (y2 - diffY * counter >= 0 && y2 - diffY * counter <= 49 && x2 - diffX * counter >= 0 && x2 - diffX * counter <= 49)
                            {
                                allAntinodes.Add((y2 - diffY * counter, x2 - diffX * counter));
                                counter += 1;
                            }
                        }
                    }
                }
            }
            return allAntinodes.Count.ToString();
        }
    }
}
