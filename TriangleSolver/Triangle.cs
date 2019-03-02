using System.Collections.Generic;

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
        /// <param name="rows">Triangle rows count.</param>
        /// <param name="path">Triangle path of elements which sum is maximum.</param>
        /// <returns>Returns maximum path sum in a triangle.</returns>
        public static bool FindMaximumPathByOddEvenRule(List<List<int>> matrix, int rows, out List<int> path)
        {
            byte[,] invalidPositions = new byte[rows, rows];
            List<int>[,] paths = new List<int>[rows, rows];

            int candidate, child1, child2, parent;
            bool isCandidateExists, isChild1Candidate;
            List<int> row;
            for (int i = rows - 1; i > 0; i--)
            {
                row = matrix[i];
                for (int j = 0; j < i; j++)
                {
                    child1 = row[j];
                    child2 = row[j + 1];
                    parent = matrix[i - 1][j];

                    isCandidateExists = GetCandidateByOddEvenRule(child1, child2, parent, i, j, invalidPositions, out candidate, out isChild1Candidate);
                    if (!isCandidateExists)
                    {
                        //set parent to invalid state
                        invalidPositions[i - 1, j] = 1;
                        continue; //skip if candidate not found
                    }
                    var childIndex = isChild1Candidate ? j : j + 1;
                    List<int> childrenList = paths[i, childIndex];

                    if (childrenList == null)
                    {
                        childrenList = new List<int>() { candidate };
                        paths[i, childIndex] = childrenList;
                    }

                    childrenList.Add(parent);

                    List<int> parentChildrenTree = paths[i - 1, j];
                    if (parentChildrenTree == null)
                    {
                        paths[i - 1, j] = new List<int>(childrenList);
                    }
                    else
                    {
                        parentChildrenTree.AddRange(childrenList);
                    }
                }
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
        /// <param name="rows">Triangle rows count.</param>
        /// <returns>Returns maximum path sum in a triangle.</returns>
        public static int FindMaximumSum(List<List<int>> matrix, int rows)
        {
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
