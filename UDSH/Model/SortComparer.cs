// Copyright (C) 2025 Mohammed Kenawy
using System.Text.RegularExpressions;

namespace UDSH.Model
{
    public class SortComparer : IComparer<string>
    {
        private readonly Regex regex = new Regex(@"(\d+)|(\D+)", RegexOptions.Compiled);

        public int Compare(string x, string y)
        {
            var partsX = regex.Matches(x);
            var partsY = regex.Matches(y);

            for (int i = 0; i < Math.Min(partsX.Count, partsY.Count); ++i)
            {
                string partX = partsX[i].Value;
                string partY = partsY[i].Value;

                if (int.TryParse(partX, out int numX) && int.TryParse(partY, out int numY))
                {
                    int comparing = numX.CompareTo(numY);
                    if (comparing != 0)
                        return comparing;
                }
                else
                {
                    int comparing = string.Compare(partX, partY, StringComparison.OrdinalIgnoreCase);
                    if (comparing != 0)
                        return comparing;
                }
            }

            return partsX.Count.CompareTo(partsY.Count);
        }
    }
}
