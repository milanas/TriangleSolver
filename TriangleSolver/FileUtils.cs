using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TriangleSolver
{
    /// <summary>
    /// Helper to work with files.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Reads file to nested list.
        /// </summary>
        /// <param name="filePath">Full input file path.</param>
        /// <param name="separator">File content elements separator.</param>
        /// <param name="rows">Number of rows.</param>
        /// <returns>Return triangle data in nested list.</returns>
        public static List<List<int>> ReadFileToNestedList(string filePath, char[] separator, out int rows)
        {
            string line;
            List <int> parts = null;
            rows = 0;
            List<List<int>> numbersList = new List<List<int>>();
            using (StreamReader file = new StreamReader(filePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)) // skip empty lines 
                    {
                        try
                        {
                            parts = line.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(string.Format("Failed to parse line '{0}' to integer numbers. File: '{1}'.", line, filePath), ex);
                        }
                        rows++;
                        if(parts.Count != rows)
                        {
                            throw new Exception(string.Format("Input data is not valid. Each line of the triangle file must have the same number of items as the row number. Invalid line number: {0}. File: '{1}'.", rows, filePath));
                        }
                        numbersList.Add(parts);
                    }
                }
            }
            return numbersList;
        }
    }
}
