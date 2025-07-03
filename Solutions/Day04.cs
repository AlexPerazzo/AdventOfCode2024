namespace AdventOfCode2024.Solutions
{
    public class Day04Solver
    {
        private string[] _lines;
        public List<List<char>> grid;

        public Day04Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            grid = BuildGrid();
        }

        public string SolvePart1()
        {
            int total = 0;

            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[0].Count; x++)
                {
                    if (grid[y][x] == 'X')
                    {
                        total += CheckM(y, x);
                    }
                }
            }

            return total.ToString();
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

        private int CheckM(int y, int x)
        {
            int count = 0;

            if (y <= 136 && grid[y + 1][x] == 'M')
                if (CheckAS(y + 1, x, "down"))
                    count++;

            if (x <= 136 && grid[y][x + 1] == 'M')
                if (CheckAS(y, x + 1, "right"))
                    count++;

            if (x >= 3 && grid[y][x - 1] == 'M')
                if (CheckAS(y, x - 1, "left"))
                    count++;

            if (y >= 3 && grid[y - 1][x] == 'M')
                if (CheckAS(y - 1, x, "up"))
                    count++;

            if (x <= 136 && y <= 136 && grid[y + 1][x + 1] == 'M')
                if (CheckAS(y + 1, x + 1, "downright"))
                    count++;

            if (y <= 136 && x >= 3 && grid[y + 1][x - 1] == 'M')
                if (CheckAS(y + 1, x - 1, "downleft"))
                    count++;

            if (y >= 3 && x >= 3 && grid[y-1][x-1] == 'M')
                if (CheckAS(y - 1, x - 1, "upleft"))
                    count++;

            if (y >= 3 && x <= 136 && grid[y - 1][x + 1] == 'M')
                if (CheckAS(y - 1, x + 1, "upright"))
                    count++;

            //if (count != 0)
            //    Console.WriteLine(count);
            return count; 
        }

        private bool CheckAS(int y, int x, string direction) 
        { 
            if (direction == "down")
                if (grid[y + 1][x] == 'A')
                    if (grid[y + 2][x] == 'S')
                        return true;

            if (direction == "up")
                if (grid[y - 1][x] == 'A')
                    if (grid[y - 2][x] == 'S')
                        return true;

            if (direction == "right")
                if (grid[y][x + 1] == 'A')
                    if (grid[y][x + 2] == 'S')
                        return true;

            if (direction == "left")
                if (grid[y][x - 1] == 'A')
                    if (grid[y][x - 2] == 'S')
                        return true;

            if (direction == "downright")
                if (grid[y + 1][x + 1] == 'A')
                    if (grid[y + 2][x + 2] == 'S')
                        return true;

            if (direction == "upleft")
                if (grid[y - 1][x - 1] == 'A')
                    if (grid[y - 2][x - 2] == 'S')
                        return true;

            if (direction == "downleft")
                if (grid[y + 1][x - 1] == 'A')
                    if (grid[y + 2][x - 2] == 'S')
                        return true;

            if (direction == "upright")
                if (grid[y - 1][x + 1] == 'A')
                    if (grid[y - 2][x + 2] == 'S')
                        return true;
            
            return false;
        }


        public string SolvePart2()
        {
            int total = 0;

            for (int y = 1; y < grid.Count - 1; y++)
            {
                for (int x = 1; x < grid[0].Count - 1; x++)
                {
                    if (grid[y][x] == 'A' && CheckMS(y, x))
                        total++;
                }
            }

            return total.ToString();
        }

        public bool CheckMS(int y, int x) 
        {
            if (grid[y - 1][x - 1] == 'M' && grid[y - 1][x + 1] == 'M')
                if (grid[y + 1][x - 1] == 'S' && grid[y + 1][x + 1] == 'S')
                    return true;

            if (grid[y - 1][x - 1] == 'S' && grid[y - 1][x + 1] == 'M')
                if (grid[y + 1][x - 1] == 'S' && grid[y + 1][x + 1] == 'M')
                    return true;

            if (grid[y - 1][x - 1] == 'S' && grid[y - 1][x + 1] == 'S')
                if (grid[y + 1][x - 1] == 'M' && grid[y + 1][x + 1] == 'M')
                    return true;

            if (grid[y - 1][x - 1] == 'M' && grid[y - 1][x + 1] == 'S')
                if (grid[y + 1][x - 1] == 'M' && grid[y + 1][x + 1] == 'S')
                    return true;

            return false;
        }
    }
}
