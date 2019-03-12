using System;
using System.Collections.Generic;
using System.Linq;

namespace TriangleSolver
{
    /// <summary>
    /// Helper to solve Triangle tasks.
    /// </summary>
    public static class Triangle
    {
        #region Public methods

        /// <summary>
        /// Finds maximum path sum in a triangle by walking over the numbers as evens and odds subsequently.
        /// </summary>
        /// <param name="matrix">Triangle data represented in nested list.</param>
        /// <param name="path">Triangle path of elements which sum is maximum.</param>
        /// <returns>Returns maximum path sum in a triangle.</returns>
        public static bool FindMaximumPathByOddEvenRule(List<List<int>> matrix, out List<int> path)
        {
            if(matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if(!matrix.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(matrix));
            }

            int rows = matrix.Count;
            byte[,] invalidPositions = new byte[rows, rows];
            List<int>[,] paths = new List<int>[rows, rows];

            int candidate, child1, child2, parent;
            int child2ColumnIndex, parentRowIndex;
            bool isCandidateExists, isChild1Candidate;
            List<int> row;
            bool firstRowFromBottom = true;
            List<int> pathToCurrentElement = null;

            for (int i = rows - 1; i > 0; i--)
            {
                row = matrix[i];
                parentRowIndex = i - 1;

                for (int j = 0; j < i; j++)
                {
                    child2ColumnIndex = j + 1;

                    child1 = row[j];
                    child2 = row[child2ColumnIndex];
                    parent = matrix[parentRowIndex][j];

                    isCandidateExists = GetCandidateByOddEvenRule(child1, child2, parent, i, j, invalidPositions, out candidate, out isChild1Candidate);
                    if (!isCandidateExists)
                    {
                        //set parent to invalid state
                        invalidPositions[parentRowIndex, j] = 1;
                        continue; //skip if candidate not found
                    }

                    var childIndex = isChild1Candidate ? j : child2ColumnIndex;

                    if (firstRowFromBottom)
                    {
                        pathToCurrentElement = new List<int>() { candidate };
                        paths[i, childIndex] = pathToCurrentElement;
                    }
                    else
                    {
                        pathToCurrentElement = paths[i, childIndex];
                    }

                    List<int> parentChildrenTree = paths[parentRowIndex, j];
                    if (parentChildrenTree == null)
                    {
                        parentChildrenTree = new List<int>(pathToCurrentElement);
                        parentChildrenTree.Add(parent);
                        paths[parentRowIndex, j] = parentChildrenTree;
                    }
                }

                firstRowFromBottom = false;
            }

            path = paths[0, 0];
            var found = path != null;
            if (found)
            {
                path.Reverse(); //because we walked from bottom to top
            }

            return found;
        }

        /// <summary>
        /// Finds maximum path sum in a triangle.
        /// </summary>
        /// <param name="matrix">Triangle data represented in nested list.</param>
        /// <returns>Returns maximum path sum in a triangle.</returns>
        public static int FindMaximumSum(List<List<int>> matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            if (!matrix.Any())
            {
                throw new ArgumentOutOfRangeException(nameof(matrix));
            }

            int rows = matrix.Count;
            int child1, child2, candidate;
            List<int> row;
            for (int i = rows - 1; i > 0; i--)
            {
                row = matrix[i];
                for (int j = 0; j < i; j++)
                {
                    child1 = row[j];
                    child2 = row[j + 1];
                    candidate = (child1 > child2) ? child1 : child2;

                    matrix[i - 1][j] += candidate; //sum elements from bottom to top
                }
            }

            return matrix[0][0]; //return top element
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Get candidate by odd and even number rule if it exists. Also it check which one is selected first or second.
        /// </summary>
        /// <param name="child1">Candidate 1 element.</param>
        /// <param name="child2">Candidate 2 element.</param>
        /// <param name="parent">Parent element.</param>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        /// <param name="invalidPositions">Array of invalid elements.</param>
        /// <param name="candidate">Found candidate.</param>
        /// <param name="isChild1Candidate">Flag which provides is it first or second element.</param>
        /// <returns>Returns true if candidate found, false - otherwise.</returns>
        private static bool GetCandidateByOddEvenRule(int child1, int child2, int parent, int i, int j, byte[,] invalidPositions, out int candidate, out bool isChild1Candidate)
        {
            candidate = 0;
            isChild1Candidate = false; //default
            bool isParentOdd;

            byte child1State = invalidPositions[i, j];
            byte child2State = invalidPositions[i, j + 1];

            //first is invalid
            if (child1State == 1)
            {
                if (child2State == 1)
                {
                    return false;
                }
                else
                {
                    candidate = child2;
                }
            }
            //second is invalid
            else if (child2State == 1)
            {
                isChild1Candidate = true;
                candidate = child1;
            }
            //both have valid state
            else
            {
                isParentOdd = IsOdd(parent);


                bool isChild1Odd = IsOdd(child1);
                bool isChild2Odd = IsOdd(child2);

                if (isParentOdd != isChild1Odd)
                {
                    if (isParentOdd != isChild2Odd)
                    {
                        isChild1Candidate = child1 > child2;
                        candidate = isChild1Candidate ? child1 : child2;
                        return true;
                    }
                    else
                    {
                        isChild1Candidate = true;
                        candidate = child1;
                        return true;
                    }
                }
                else if (isParentOdd != isChild2Odd)
                {
                    candidate = child2;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            isParentOdd = IsOdd(parent);
            bool isCandidateOdd = IsOdd(candidate);

            return isParentOdd != isCandidateOdd;
        }

        /// <summary>
        /// Function which checks if number is odd.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>Returns 'true' if numbers is odd, 'false' - otherwise.</returns>
        private static bool IsOdd(int number)
        {
            var remainder = number % 2;
            return (number > 0 && remainder == 1) || (number < 0 && remainder == -1);
        }

        #endregion Private methods
    }
}
