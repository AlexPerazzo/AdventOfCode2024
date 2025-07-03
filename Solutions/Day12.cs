using System.Diagnostics.Metrics;
using System.Linq;

namespace AdventOfCode2024.Solutions
{
    public class Day12Solver
    {
        private string[] _lines;
        private List<List<char>> _map;
        public HashSet<(int, int)> _checkedSpots = new HashSet<(int, int)>();
        public HashSet<(int, int)> _region = new HashSet<(int, int)>();
        public List<(int, int)> directions = [(0, 1), (0, -1), (1, 0), (-1, 0)];

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

            for (var col = 0; col < _map.Count; col++)
            {
                for (var row = 0; row < _map[0].Count; row++)
                {
                    if (!_checkedSpots.Contains((col, row)))
                    {
                        var letter = _map[col][row];
                        CheckNeighbors(letter, col, row);

                        total += ProcessRegionPart2(letter);
                        _region.Clear();
                    }
                }
            }


            return total.ToString();
        }

        public void CheckNeighbors(char letter, int col, int row)
        {
            _region.Add((col, row));
            _checkedSpots.Add((col, row));

            foreach ((int moveCol, int moveRow) in directions)
            {
                int newCol = col + moveCol;
                int newRow = row + moveRow;

                if (newCol >= 0 && newCol < _map.Count && newRow >= 0 && newRow < _map[newCol].Count)
                {
                    if (!_region.Contains((newCol, newRow)) && _map[newCol][newRow] == letter)
                    {
                        CheckNeighbors(letter, newCol, newRow);
                    }
                }
            }
        }

        public int ProcessRegion(char letter)
        {
            int regionSize = _region.Count;
            int perimeter = 0;

            foreach((int col, int row) in _region)
            {
                foreach ((int moveCol, int moveRow) in directions)
                {
                    int newCol = col + moveCol;
                    int newRow = row + moveRow;

                    if (newCol >= 0 && newCol < _map.Count && newRow >= 0 && newRow < _map[newCol].Count)
                    {
                        if (_map[newCol][newRow] != letter)
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
            int regionSize = _region.Count;
            int perimeter = 0;
            List<(int currentRow, int neighborRow)> perimeterRows = new List<(int, int)>();
            List<(int currentCol, int neighborCol)> perimeterCols = new List<(int, int)>();

            foreach ((int col, int row) in _region)
            {
                foreach ((int moveCol, int moveRow) in directions)
                {
                    int newCol = col + moveCol;
                    int newRow = row + moveRow;

                    if (newCol >= 0 && newCol < _map.Count && newRow >= 0 && newRow < _map[newCol].Count)
                    {
                        if (_map[newCol][newRow] != letter)
                        {
                            if (newCol != col && !perimeterCols.Contains((col, newCol)))
                            {
                                perimeter++;
                                perimeterCols.Add((col, newCol));
                            }
                            else if (newRow != row && !perimeterRows.Contains((row, newRow)))
                            {
                                perimeter++;
                                perimeterRows.Add((row, newRow));
                            }
                        }
                    }
                    else
                    {
                        if (newCol != col && !perimeterCols.Contains((col, newCol)))
                        {
                            perimeter++;
                            perimeterCols.Add((col, newCol));
                        }
                        else if (newRow != row && !perimeterRows.Contains((row, newRow)))
                        {
                            perimeter++;
                            perimeterRows.Add((row, newRow));
                        }
                    }
                }
            }

            return regionSize * perimeter;
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
