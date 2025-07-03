namespace AdventOfCode2024.Solutions
{
    public class Day06Solver
    {
        private string[] _lines;
        public List<List<char>> grid;
        public HashSet<(int, int)> spacesGoneTo = new HashSet<(int, int)>();


        public Day06Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            grid = BuildGrid();
            CreateSpacesGoneTo();
        }

        public string SolvePart1()
        {
            return spacesGoneTo.Count.ToString();
        }

        private void CreateSpacesGoneTo()
        {
            (int y, int x) = FindGuard(grid);
            string direction = "up";
            bool onTheMap = true;

            while (onTheMap)
            {
                while (direction == "up")
                {
                    if (y - 1 < 0)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y - 1][x] == '#')
                        direction = "right";
                    else
                    {
                        (y, x) = (y - 1, x);
                        spacesGoneTo.Add((y, x));
                    }
                }

                while (direction == "right")
                {
                    if (x + 1 >= grid[y].Count)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y][x + 1] == '#')
                        direction = "down";
                    else
                    {
                        (y, x) = (y, x + 1);
                        spacesGoneTo.Add((y, x));
                    }
                }

                while (direction == "down")
                {
                    if (y + 1 >= grid.Count)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y + 1][x] == '#')
                        direction = "left";
                    else
                    {
                        (y, x) = (y + 1, x);
                        spacesGoneTo.Add((y, x));
                    }
                }

                while (direction == "left")
                {
                    if (x - 1 < 0)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y][x - 1] == '#')
                        direction = "up";
                    else
                    {
                        (y, x) = (y, x - 1);
                        spacesGoneTo.Add((y, x));
                    }
                }
            }
        }

        private bool CreatedInfiniteLoop(int y, int x)
        {
            HashSet<(int, int, string)> spacesGoneToWithDirections = new HashSet<(int, int, string)>();

            string direction = "up";
            bool onTheMap = true;

            while (onTheMap)
            {
                while (direction == "up")
                {
                    if (y - 1 < 0)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y - 1][x] == '#')
                        direction = "right";
                    else
                    {
                        (y, x) = (y - 1, x);
                        if (spacesGoneToWithDirections.Contains((y, x, direction)))
                            return true;
                        
                        spacesGoneToWithDirections.Add((y, x, direction));
                    }
                }

                while (direction == "right")
                {
                    if (x + 1 >= grid[y].Count)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y][x + 1] == '#')
                        direction = "down";
                    else
                    {
                        (y, x) = (y, x + 1);
                        if (spacesGoneToWithDirections.Contains((y, x, direction)))
                            return true;

                        spacesGoneToWithDirections.Add((y, x, direction));
                    }
                }

                while (direction == "down")
                {
                    if (y + 1 >= grid.Count)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y + 1][x] == '#')
                        direction = "left";
                    else
                    {
                        (y, x) = (y + 1, x);
                        if (spacesGoneToWithDirections.Contains((y, x, direction)))
                            return true;

                        spacesGoneToWithDirections.Add((y, x, direction));
                    }
                }

                while (direction == "left")
                {
                    if (x - 1 < 0)
                    {
                        onTheMap = false;
                        break;
                    }

                    if (grid[y][x - 1] == '#')
                        direction = "up";
                    else
                    {
                        (y, x) = (y, x - 1);
                        if (spacesGoneToWithDirections.Contains((y, x, direction)))
                            return true;

                        spacesGoneToWithDirections.Add((y, x, direction));
                    }
                }
            }

            return false;
        }

        public string SolvePart2()
        {
            //Count set to -1 because origin space does not count
            int count = -1;
            (int guardY, int guardX) = FindGuard(grid);

            foreach ((int y, int x) in spacesGoneTo)
            {
                grid[y][x] = '#';

                if (CreatedInfiniteLoop(guardY, guardX))
                    count++;

                grid[y][x] = '.';
            }
            return count.ToString();
        }

        (int y, int x) FindGuard(List<List<char>> grid)
        {
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    if (grid[y][x] == '^')
                    {
                        return (y, x);
                    }
                }
            }

            throw new Exception($"Could not find guard. In function FindGuard");
        }


        private List<List<char>> BuildGrid()
        {
            var grid = new List<List<char>>();

            foreach (var line in _lines)
            {
                var row = new List<char>();
                foreach (char letter in line)
                {
                    row.Add(letter);
                }
                grid.Add(row);
            }

            return grid;
        }
    }
}
