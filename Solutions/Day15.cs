namespace AdventOfCode2024.Solutions
{
    public class Day15Solver
    {
        private string[] _lines;
        private string _instructions = "";
        private List<List<char>> _warehouseMap = new();
        private Dictionary<char, (int y, int x)> _directions = new() { 
            {'^', (-1,0)},
            {'>', (0,1)},
            {'v', (1,0)},
            {'<', (0,-1)},
        };

        public Day15Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);

            ProcessText();
        }

        public string SolvePart1()
        {
            (int y, int x) currentCords = FindCordsOfRobot();

            foreach (var instruction in _instructions) 
            { 
                (int, int) direction = _directions[instruction];
                if (MoveThing(currentCords, direction))
                    currentCords = (currentCords.y + _directions[instruction].y, currentCords.x + _directions[instruction].x);
            }

            long totalScore = CalculateScore();
            //PrintMap();
            return totalScore.ToString();
        }

        public bool MoveThing((int y, int x) cords, (int y, int x) direction)
        {
            bool move = true;
            (int y, int x) newCords = ((cords.y + direction.y), (cords.x + direction.x));

            char currentItem = _warehouseMap[cords.y][cords.x];
            char itemInTheWay = _warehouseMap[newCords.y][newCords.x];

            if (itemInTheWay == 'O')
            {
                move = MoveThing(newCords, direction);
                itemInTheWay = _warehouseMap[newCords.y][newCords.x];
            }

            if (itemInTheWay != '#' && move)
            {
                _warehouseMap[newCords.y][newCords.x] = currentItem;
                _warehouseMap[cords.y][cords.x] = itemInTheWay;
                //PrintMap();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool MoveThingPart2((int y, int x) cords, (int y, int x) direction)
        {
            bool move = true;
            (int y, int x) newCords = ((cords.y + direction.y), (cords.x + direction.x));

            char currentItem = _warehouseMap[cords.y][cords.x];
            char itemInTheWay = _warehouseMap[newCords.y][newCords.x];

            if (itemInTheWay == '[')
            {
                (int y, int x) otherHalfCords = ((newCords.y), (newCords.x + 1));

                bool move1 = MoveThingPart2(newCords, direction);
                bool move2 = MoveThingPart2(otherHalfCords, direction);
                move = move1 && move2;
                itemInTheWay = _warehouseMap[newCords.y][newCords.x];
            }

            if (itemInTheWay == ']')
            {
                (int y, int x) otherHalfCords = ((newCords.y), (newCords.x - 1));

                bool move1 = MoveThingPart2(newCords, direction);
                bool move2 = MoveThingPart2(otherHalfCords, direction);
                move = move1 && move2;
                itemInTheWay = _warehouseMap[newCords.y][newCords.x];
            }



            if (itemInTheWay != '#' && move)
            {
                _warehouseMap[newCords.y][newCords.x] = currentItem;
                _warehouseMap[cords.y][cords.x] = itemInTheWay;
                //PrintMap();
                return true;
            }
            else
            {
                return false;
            }
        }



        private (int, int) FindCordsOfRobot()
        {
            for (int i = 0; i < _warehouseMap.Count; i++)
            {
                for (int j = 0; j < _warehouseMap[0].Count; j++)
                {
                    if (_warehouseMap[i][j] == '@')
                        return (i, j);
                }
            }
            return (-1, -1);
        }

        private void PrintMap()
        {
            foreach (var row in _warehouseMap)
            {
                if (row.Contains('@'))
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(string.Concat(row));
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(string.Concat(row));

                }


            }
        }

        private long CalculateScore()
        {
            long total = 0;

            for (int i = 0; i < _warehouseMap.Count - 1; i++)
            {
                for (int j = 0; j < _warehouseMap[0].Count - 1; j++)
                {
                    if (_warehouseMap[i][j] == 'O')
                        total += i * 100 + j;
                }
            }

            return total;
        }
        public void ProcessText()
        {

            bool isMap = true;

            foreach (var line in _lines) 
            {
                if (string.IsNullOrEmpty(line))
                {
                    isMap = false;
                }

                else if (isMap)
                {
                    _warehouseMap.Add([..line]);
                }

                else
                {
                    _instructions += line.TrimEnd('\r', '\n');
                }

            }
        }

        private List<List<char>> UpdateMapForPart2()
        {
            List<List<char>> _updatedWarehouseMap = new();

            foreach (var line in _warehouseMap)
            {
                string newLine = "";

                foreach (var character in line)
                {
                    if (character == '#')
                    {
                        newLine += '#';
                        newLine += '#';
                    }
                    else if (character == 'O')
                    {
                        newLine += '[';
                        newLine += ']';
                    }
                    else if (character == '.')
                    {
                        newLine += '.';
                        newLine += '.';
                    }
                    else if (character == '@')
                    {
                        newLine += '@';
                        newLine += '.';
                    }
                }

                _updatedWarehouseMap.Add([.. newLine]);
            }

            return _updatedWarehouseMap;
        }
        public string SolvePart2()
        {
            //_warehouseMap.Clear();
            //_instructions = "";

            //ProcessText();
            //_warehouseMap = UpdateMapForPart2();

            //Console.WriteLine("pt2");
            //(int y, int x) currentCords = FindCordsOfRobot();
            //PrintMap();
            //var count = 0;
            //foreach (var instruction in _instructions)
            //{
            //    (int, int) direction = _directions[instruction];
            //    Console.WriteLine(_directions[instruction].y);
            //    Console.WriteLine(_directions[instruction].x);

            //    if (MoveThingPart2(currentCords, direction))
            //        currentCords = (currentCords.y + _directions[instruction].y, currentCords.x + _directions[instruction].x);
            //    PrintMap();
            //    if (count > 10)
            //        break;
            //    count++;
            //}

            //long totalScore = CalculateScore();
            return "WIP";
        }
    }
}
