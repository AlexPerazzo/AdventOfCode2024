using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions
{
    public class Day14Solver
    {
        private string[] _lines;
        private List<((int, int), (int, int))> _allRobots = new();
        private List<((int, int), (int, int))> _allRobotsSecond = new();


        public Day14Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            ProcessLines();

        }

        private void ProcessLines()
        {
            foreach (var line in _lines)
            {
                var match = Regex.Match(line, @"p=(-?\d+),(-?\d+)\s+v=(-?\d+),(-?\d+)");
                if (match.Success)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    int xSpeed = int.Parse(match.Groups[3].Value);
                    int ySpeed = int.Parse(match.Groups[4].Value);

                    _allRobots.Add(((x, y), (xSpeed, ySpeed)));
                }
            }
        }

        private ((int, int), (int, int)) ProcessRobot(((int x, int y), (int xSpeed, int ySpeed)) robot)
        {
            int x = robot.Item1.x; int y = robot.Item1.y;
            int xSpeed = robot.Item2.xSpeed; int ySpeed = robot.Item2.ySpeed;

            //101 wide 103 tall
            x += xSpeed;
            x %= 101;
            y += ySpeed;
            y %= 103;
            if (x < 0)
            {
                x += 101;
            }
            if (y < 0) 
            { 
                y += 103;
            }

            return ((x, y), (xSpeed, ySpeed));
        }

        public string SolvePart1()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    foreach (var robot in _allRobots) 
                    { 
                        var newRobot = ProcessRobot(robot);

                        _allRobotsSecond.Add(newRobot);
                    }
                    _allRobots.Clear();
                }
                else
                {
                    foreach (var robot in _allRobotsSecond)
                    {
                        var newRobot = ProcessRobot(robot);

                        _allRobots.Add(newRobot);
                    }
                    _allRobotsSecond.Clear();
                }
            }


            return CheckQuads().ToString();
        }

        public int CheckQuads()
        {
            //0-100: 0-49, 50, 51-100

            //0-102: 0-50, 51, 52-102
            int quad1 = 0;
            int quad2 = 0;
            int quad3 = 0;
            int quad4 = 0;

            foreach(((int x, int y), (int xSpeed, int ySpeed)) robot in _allRobots)
            {
                int x = robot.Item1.x; int y = robot.Item1.y;

                if (x <= 49 && y <= 50)
                    quad1++;
                if (x <= 49 && y >= 52)
                    quad2++;
                if (x >= 51 && y <= 50)
                    quad3++;
                if (x >= 51 && y >= 52)
                    quad4++;
            }
            return quad1 * quad2 * quad3 * quad4;
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
