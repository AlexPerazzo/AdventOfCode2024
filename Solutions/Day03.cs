namespace AdventOfCode2024.Solutions
{
    public class Day03Solver
    {
        private string[] _lines;
        private string[] input;

        public Day03Solver(string inputPath)
        {
            input = File.ReadAllLines(inputPath);
        }

        public string SolvePart1()
        {
            string doOrDoNot = "";

            bool Do = true;

            string mulString = "";

            int total = 0;

            foreach (char c in input.SelectMany(s => s))
            {
                try 
                { 
                    if (mulString == "" && c == 'm')
                        mulString += c;
                    else if (mulString == "m" && c == 'u')
                        mulString += c;
                    else if (mulString == "mu" && c == 'l')
                        mulString += c;
                    else if (mulString == "mul" && c == '(')
                        mulString += c;
                    else if (mulString == "mul(" && char.IsDigit(c))
                        mulString += c;
                
                    else if (mulString.Length >= 4)
                        {
                            if ((mulString.Substring(0, 4) == "mul(" && char.IsDigit(mulString[4])) && (char.IsDigit(c) || c == ',') && !mulString.Contains(","))
                                mulString += c;
                            else if (mulString.Substring(0, 4) == "mul(" && char.IsDigit(mulString[4]) && mulString.Contains(",") && char.IsDigit(c))
                                mulString += c;
                            else if (mulString.Substring(0, 4) == "mul(" && char.IsDigit(mulString[4]) && mulString.Contains(",") && char.IsDigit(mulString[mulString.IndexOf(",") + 1]) && c == ')')
                                mulString += c;
                            else
                                mulString = "";

                            if (mulString.Substring(0, 4) == "mul(" && char.IsDigit(mulString[4]) && mulString.Contains(",") && char.IsDigit(mulString[mulString.IndexOf(",") + 1]) && c == ')')
                            {
                                if (Do)
                            {
                                Console.WriteLine(mulString);
                                        var nums = mulString.Substring(4,(mulString.Length-4)).Split(",");
                                        var number1 = int.Parse(nums[0]);
                                        var number2 = int.Parse(nums[1].Substring(0, nums[1].Length - 1));
                                        total += (number1 * number2);

                            }
                                else
                            {
                                Console.WriteLine($"Nope! {mulString}");
                            }
                                        mulString = "";
                            }
                        }
                    else
                    {
                            mulString = "";
                    }
                }
                catch 
                {
                    continue;
                }

                if (doOrDoNot == "" && c == 'd')
                    doOrDoNot += c;
                else if (doOrDoNot == "d" && c == 'o')
                    doOrDoNot += c;
                else if (doOrDoNot == "do" && (c == 'n' || c == '('))
                    doOrDoNot += c;
                else if (doOrDoNot == "don" && c == '\'')
                    doOrDoNot += c;
                else if (doOrDoNot == "don'" && c == 't')
                    doOrDoNot += c;
                else if (doOrDoNot == "don't" && c == '(')
                    doOrDoNot += c;
                else if (doOrDoNot == "don't(" && c == ')')
                {
                    Do = false;
                    doOrDoNot = "";
                }
                else if (doOrDoNot == "do(" && c ==  ')')
                {
                    Do = true;
                    doOrDoNot = "";
                }
                else
                    doOrDoNot = "";
            }

            return total.ToString();
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
