using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TriangleSolver;

namespace Run
{
    class Program
    {
        /// <summary>
        /// File content separator.
        /// </summary>
        private static char[] _separator = new char[] { ' ', '\t' };

        static void Main(string[] args)
        {
            try
            {
                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string filePath = Path.Combine(assemblyPath, "Data", "input.txt");

                if (File.Exists(filePath))
                {
                    int rows;
                    var matrix = FileUtils.ReadFileToNestedList(filePath, _separator, out rows);
                    if(rows < 2)
                    {
                        Console.WriteLine("Input data is invalid. The triangle can be at least from 2 rows.");
                        return;
                    }

                    List<int> path;
                    bool pathExist = Triangle.FindMaximumPathByOddEvenRule(matrix, rows, out path);
                    if (pathExist)
                    {
                        Console.WriteLine("Maximum path and sum in triangle: ");
                        Console.WriteLine(string.Join("->", path));
                        Console.WriteLine("{0} = {1}", path.Sum(), string.Join(" + ", path.Select(i => FormatNegativeNumbers(i))));
                    }
                    else
                    {
                        Console.WriteLine("Sorry... Triangle path not exists.");
                    }
                }
                else
                {
                    Console.WriteLine("File '{0}' not exists or inaccessible.", filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.WriteLine("StackTrace: {0}", ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Helper to display negative integer in proper way.
        /// </summary>
        /// <param name="i">Integer number.</param>
        /// <returns>Number string.</returns>
        private static string FormatNegativeNumbers(int i)
        {
            return i < 0 ? string.Format("({0})", i) : i + "";
        }
    }
}
