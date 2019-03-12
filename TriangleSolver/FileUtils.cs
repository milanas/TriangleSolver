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
        /// <returns>Returns triangle data in nested list.</returns>
        public static List<List<int>> ReadFileToNestedList(string filePath, string separator, out int rows)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentOutOfRangeException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            if (separator == null)
            {
                throw new ArgumentNullException(nameof(separator));
            }

            char[] separatorChars = separator.ToCharArray();
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
                            parts = line.Split(separatorChars, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();
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

        /// <summary>
        /// Reads triangle data string to nested list.
        /// </summary>
        /// <param name="input">Triangle data inline.</param>
        /// <param name="rowsSeparator">Rows separator.</param>
        /// <param name="cellsSeparator">Cells separator.</param>
        /// <returns>Returns triangle data in nested list.</returns>
        public static List<List<int>> ParseTriangleValuesStringToMatrix(string input, string rowsSeparator, string cellsSeparator)
        {
            if (input == null)
            {
                return null;
            }
            return input.Split(new[] { rowsSeparator }, StringSplitOptions.RemoveEmptyEntries).Select(lvl1 => lvl1.Split(new[] { cellsSeparator }, StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val.Trim())).ToList()).ToList();
        }
    }
}
