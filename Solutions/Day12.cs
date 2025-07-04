namespace AdventOfCode2024.Solutions
{
    public class Day12Solver
    {
        private string[] _lines;
        private List<List<char>> _map;
        public HashSet<(int, int)> _checkedSpots = new HashSet<(int, int)>();
        public HashSet<(int, int)> _region = new HashSet<(int, int)>();

        public List<(int, int)> directions = [(0, 1), (0, -1), (1, 0), (-1, 0)];
        public List<(int currentY, int currentX, (int, int) direction)> perimeterCords = new();
        public int perimeter = 0;


        public Day12Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            MakeMap();
        }

        public void MakeMap()
        {
            _map = new List<List<char>>();

            foreach (string line in _lines)
            {
                var foo = new List<char>();
                foreach (char c in line)
                {
                    foo.Add(c);
                }
                _map.Add(foo);
            }
        }

        public string SolvePart1()
        {
            int total = 0;

            for (var y = 0; y < _map.Count; y++)
            {
                for (var x = 0; x < _map[0].Count; x++)
                {
                    if (!_checkedSpots.Contains((y, x)))
                    {
                        var letter = _map[y][x];
                        CheckNeighbors(letter, y, x);

                        total += ProcessRegion(letter);
                        _region.Clear();
                        perimeterCords.Clear();
                    }
                }
            }


            return total.ToString();
        }

        public void CheckNeighbors(char letter, int y, int x)
        {
            _region.Add((y, x));
            _checkedSpots.Add((y, x));

            foreach ((int deltaY, int deltaX) in directions)
            {
                int newY = y + deltaY;
                int newX = x + deltaX;

                if (newY >= 0 && newY < _map.Count && newX >= 0 && newX < _map[newY].Count)
                {
                    if (!_region.Contains((newY, newX)) && _map[newY][newX] == letter)
                    {
                        CheckNeighbors(letter, newY, newX);
                    }
                }
            }
        }

        public int ProcessRegion(char letter)
        {
            int regionSize = _region.Count;
            int perimeter = 0;

            foreach((int Y, int X) in _region)
            {
                foreach ((int deltaY, int deltaX) in directions)
                {
                    int newY = Y + deltaY;
                    int newX = X + deltaX;

                    if (newY >= 0 && newY < _map.Count && newX >= 0 && newX < _map[newY].Count)
                    {
                        if (_map[newY][newX] != letter)
                        {
                            perimeter++;
                        }
                    }
                    else
                    {
                        perimeter++;
                    }
                }
            }

            return regionSize * perimeter;
        }

        public int ProcessRegionPart2(char letter)
        {
            perimeter = 0;
            int regionSize = _region.Count;

            foreach ((int y, int x) in _region)
            {
                foreach (var direction in directions)
                {
                    (int deltaY, int deltaX) = direction;
                    int newY = y + deltaY;
                    int newX = x + deltaX;

                    if (newY >= 0 && newY < _map.Count && newX >= 0 && newX < _map[newY].Count)
                    {
                        if (_map[newY][newX] != letter)
                        {
                            perimeterCords.Add((y, x, direction));
                        }
                    }
                    else
                    {
                        perimeterCords.Add((y, x, direction));
                    }
                }
            }
            ProcessPerimeters();

            return regionSize * perimeter;
        }

        public void ProcessPerimeters()
        {
            foreach (var (Y, X, direction) in perimeterCords.ToList())
            {
                if (perimeterCords.Contains((Y, X, direction)))
                {
                    perimeter++;
                    DeletePerimeters(Y, X, direction);
                }
            }
        }

        public void DeletePerimeters(int y, int x, (int deltaY, int deltaX) direction)
        {
            //If there's an adjacent perimeter, delete it from the main list
            //Thus only counting each wall of perimeters once

            perimeterCords.Remove((y, x, direction));
            //If you were checking to the left or right, delete up and down neighbors
            if (direction.deltaX != 0)
            {
                if (perimeterCords.Contains((y + 1, x, direction)))
                {
                    perimeterCords.Remove((y + 1, x, direction));
                    DeletePerimeters(y+1, x, direction);
                }
                if (perimeterCords.Contains((y - 1, x, direction)))
                {
                    perimeterCords.Remove((y - 1, x, direction));
                    DeletePerimeters(y - 1, x, direction);
                }
            }

            else
            {
                if (perimeterCords.Contains((y, x + 1, direction)))
                {
                    perimeterCords.Remove((y, x + 1, direction));
                    DeletePerimeters(y, x + 1, direction);
                }
                if (perimeterCords.Contains((y, x - 1, direction)))
                {
                    perimeterCords.Remove((y, x - 1, direction));
                    DeletePerimeters(y, x - 1, direction);
                }
            }
        }

        public string SolvePart2()
        {
            //Same Process to find regions, just process ths scoring of the region differently
            _checkedSpots.Clear();

            int total = 0;

            for (var y = 0; y < _map.Count; y++)
            {
                for (var x = 0; x < _map[0].Count; x++)
                {
                    if (!_checkedSpots.Contains((y, x)))
                    {
                        var letter = _map[y][x];
                        CheckNeighbors(letter, y, x);

                        total += ProcessRegionPart2(letter);
                        _region.Clear();
                        perimeterCords.Clear();
                    }
                }
            }


            return total.ToString();
        }
    }
}
