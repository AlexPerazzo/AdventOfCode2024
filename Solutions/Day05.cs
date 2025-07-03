namespace AdventOfCode2024.Solutions
{
    public class Day05Solver
    {
        private string[] _lines;
        private List<List<string>> allPages = new List<List<string>>();
        private List<List<string>> allInstructions = new List<List<string>>();
        public Day05Solver(string inputPath)
        {
            _lines = File.ReadAllLines(inputPath);
            ProcessPagesAndInstructions();
        }

        public string SolvePart1()
        {
            int count = 0;

            foreach (var pages in allPages)
            {
                bool countUp = true;
                int middlePage = 0;

                var duplicatedPages = DuplicateList(pages);

                foreach (var page in pages) 
                {
                    duplicatedPages.Remove(page);

                    List<string> thisPagesInstructions = allInstructions.Where(inner => inner[0] == page).Select(inner => inner[1]).ToList();

                    bool hasCommon = duplicatedPages.Any(item => thisPagesInstructions.Contains(item));

                    if (hasCommon) 
                    {
                        //var testing = pages.Count;
                        countUp = false;
                        break;
                    }
                    middlePage = int.Parse(pages[pages.Count / 2]);
                }
                
                if (countUp)
                    count += middlePage; 
            }
            return count.ToString();
        }

        public string SolvePart2()
        {
            int count = 0;

            foreach (var pages in allPages)
            {
                bool fixedOrder = false;
                int middlePage = 0;

                var duplicatedPages = DuplicateList(pages);
                var sortThisList = DuplicateList(pages);

                foreach (var page in pages)
                {
                    duplicatedPages.Remove(page);

                    List<string> thisPagesInstructions = allInstructions.Where(inner => inner[0] == page).Select(inner => inner[1]).ToList();

                    //duplicatedPages below should in reality be sortThisList -- except without the items at the beginning of the list
                    var subrange = sortThisList[sortThisList.IndexOf(page)..sortThisList.Count];
                    bool hasCommon = subrange.Any(item => thisPagesInstructions.Contains(item));

                    var lastDuplicatedItem = subrange.Where(item => thisPagesInstructions.Contains(item)).LastOrDefault();

                    var indexOfFurthestWrongItem = sortThisList.IndexOf(lastDuplicatedItem);

                    if (hasCommon)
                    {
                        //Remove page and add it directly after the furthest wrong item
                        sortThisList.Remove(page);
                        sortThisList.Insert(indexOfFurthestWrongItem, page);

                        fixedOrder = true;
                    }
                    middlePage = int.Parse(sortThisList[sortThisList.Count / 2]);
                }

                if (fixedOrder)
                    count += middlePage;
            }
            return count.ToString();
        }

        private List<string> DuplicateList(List<string> listToDuplicate)
        {
            var duplicatedList = new List<string>();
            foreach (var item in listToDuplicate)
            {
                duplicatedList.Add(item);
            }

            return duplicatedList;
        }
        private void ProcessPagesAndInstructions() 
        {
            bool swap = false;

            foreach (string line in _lines)
            {
                if (line == "break")
                { 
                    swap = true; 
                    continue; 
                }

                if (!swap)
                    allInstructions.Add(line.Split("|").ToList());
                else
                    allPages.Add(line.Split(",").Reverse().ToList());
            }
        }
        
    }
}
