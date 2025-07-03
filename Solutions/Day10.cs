using System.Net.NetworkInformation;

namespace AdventOfCode2024.Solutions
{
    public class Day10Solver
    {
        private string[] _lines;
        private List<List<char>> _map;
        private int total = 0;
        private List<(int, int)> peaksGoneTo = new List<(int, int)>();
        

        public Day10Solver(string inputPath)
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
            for (var col = 0; col < _map.Count; col++) 
            { 
                for (var row = 0; row < _map[0].Count; row++)
                {
                    if (_map[col][row] == '0')
                    {
                        peaksGoneTo = new List<(int, int)>();
                        TestPath(col, row, 0);
                    }
                }
            }

            return total.ToString();
        }

        public void TestPath(int col, int row, int elevation)
        {
            int width = _map.Count;         // number of columns
            int height = _map[0].Count;     // number of rows

            if (elevation == 9 && !peaksGoneTo.Contains((col, row)))
            {
                peaksGoneTo.Add((col, row));
                total++;
            }
            else
            {
                if (col - 1 >= 0 && int.Parse(_map[col - 1][row].ToString()) == elevation + 1)       // up
                    TestPath(col - 1, row, elevation + 1);

                if (row + 1 < width && int.Parse(_map[col][row + 1].ToString()) == elevation + 1)   // right
                    TestPath(col, row + 1, elevation + 1);

                if (col + 1 < height && int.Parse(_map[col + 1][row].ToString()) == elevation + 1)    // down
                    TestPath(col + 1, row, elevation + 1);

                if (row - 1 >= 0 && int.Parse(_map[col][row - 1].ToString()) == elevation + 1)       // left
                    TestPath(col, row - 1, elevation + 1);
            }
        }

        public string SolvePart2()
        {
            total = 0;
            for (var col = 0; col < _map.Count; col++)
            {
                for (var row = 0; row < _map[0].Count; row++)
                {
                    if (_map[col][row] == '0')
                    {
                        TestPathPart2(col, row, 0);
                    }
                }
            }

            return total.ToString();
        }

        public void TestPathPart2(int col, int row, int elevation)
        {
            int width = _map.Count;         // number of columns
            int height = _map[0].Count;     // number of rows

            if (elevation == 9)
            {
                total++;
            }
            else
            {
                if (col - 1 >= 0 && int.Parse(_map[col - 1][row].ToString()) == elevation + 1)       // up
                    TestPathPart2(col - 1, row, elevation + 1);

                if (row + 1 < width && int.Parse(_map[col][row + 1].ToString()) == elevation + 1)   // right
                    TestPathPart2(col, row + 1, elevation + 1);

                if (col + 1 < height && int.Parse(_map[col + 1][row].ToString()) == elevation + 1)    // down
                    TestPathPart2(col + 1, row, elevation + 1);

                if (row - 1 >= 0 && int.Parse(_map[col][row - 1].ToString()) == elevation + 1)       // left
                    TestPathPart2(col, row - 1, elevation + 1);
            }
        }
    }
}
