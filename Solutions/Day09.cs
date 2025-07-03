namespace AdventOfCode2024.Solutions
{
    public class Day09Solver
    {
        private string[] _lines;
        private string data = "";
        private string emptySpace = "";

        public Day09Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            GetDataAndSpace();
        }

        public string SolvePart1()
        {
            long total = 0;
            var dataIndex = 0;
            var totalSpotsFilled = 0;

            var foo = 0;

            while (data.Length > 0)
            {
                if (foo % 2 == 0)
                {
                    var test = int.Parse(data[0].ToString());

                    for (var i = 0; i < int.Parse(data[0].ToString()); i++)
                    {
                        total += (dataIndex) * totalSpotsFilled;
                        totalSpotsFilled++;
                    }

                    //total += int.Parse(data[0].ToString()) * dataIndex * totalSpotsFilled + additionalPointsNeeded;
                    data = data.Substring(1, data.Length - 1); //Remove first char

                    dataIndex++;
                    //totalSpotsFilled += int.Parse(data[0].ToString());
                }
                else
                {

                    bool keepGoing = true;

                    while (keepGoing && data.Length > 0)
                    {
                        
                        int lastData = int.Parse(data[data.Length - 1].ToString());
                        int howMuchSpace = int.Parse(emptySpace[0].ToString());
                        if (howMuchSpace < 0)
                        {
                            var what = 0;
                        }

                        int leftoverData = lastData - howMuchSpace;

                        if (leftoverData > 0)
                        {
                            data = data.Remove(data.Length - 1); // Remove last char
                            data += leftoverData.ToString();    // Append leftoverData

                            emptySpace = emptySpace.Substring(1, emptySpace.Length - 1);



                            // Data.Length - 1 gets us the value of the storage there.
                            // * it by how much we put in
                            // Value of container          // spaces spread across //Index
                            for (var i = 0; i < howMuchSpace; i++)
                            {
                                total += (data.Length - 1 + dataIndex) * totalSpotsFilled;
                                
                                totalSpotsFilled++;
                            }

                            //total += (data.Length - 1 + dataIndex) * howMuchSpace * totalSpotsFilled + additionalPointsNeeded;
                            keepGoing = false;

                            //totalSpotsFilled += howMuchSpace;

                        }

                        else if (leftoverData == 0)
                        {

                            for (var i = 0; i < lastData; i++)
                            {
                                total += (data.Length - 1 + dataIndex) * totalSpotsFilled;
                                totalSpotsFilled++;
                            }

                            emptySpace = emptySpace.Substring(1, emptySpace.Length - 1); //Remove first char
                            data = data.Remove(data.Length - 1); // Remove last char

                            keepGoing = false;

                            //totalSpotsFilled += lastData;

                        }

                        else if (leftoverData < 0)
                        {


                            for (var i = 0; i < lastData; i++)
                            {
                                total += (data.Length - 1 + dataIndex) * totalSpotsFilled;
                                totalSpotsFilled++;
                            }


                            leftoverData = Math.Abs(leftoverData);
                            emptySpace = leftoverData.ToString() + emptySpace.Substring(1);

                            data = data.Remove(data.Length - 1); // Remove last char

                            //totalSpotsFilled += lastData;

                        }
                    }                
                }
                foo++;
            }
            return total.ToString();
        }

        public void GetDataAndSpace()
        {

            var counter = 0;

            foreach (var line in _lines)
            {
                foreach (var digit in line)
                {
                    if (counter % 2 == 0)
                    {
                        data += digit;
                    }
                    else
                    {
                        emptySpace += digit;
                    }
                    counter++;
                }
            }
        }

        public string SolvePart2()
        {
            return "";
        }
    }
}
