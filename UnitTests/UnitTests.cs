using System;
using System.Collections.Generic;
using System.Linq;
using TriangleSolver;
using Xunit;

namespace UnitTests
{
    public class UnitTests
    {
        [Theory]
        [InlineData(
            "215;"+
            "192 124;" +
            "117 269 442;" +
            "218 836 347 235;" +
            "320 805 522 417 345;" +
            "229 601 728 835 133 124;" +
            "248 202 277 433 207 263 257;" +
            "359 464 504 528 516 716 871 182;" +
            "461 441 426 656 863 560 380 171 923;" +
            "381 348 573 533 448 632 387 176 975 449;" +
            "223 711 445 645 245 543 931 532 937 541 444;" +
            "330 131 333 928 376 733 017 778 839 168 197 197;" +
            "131 171 522 137 217 224 291 413 528 520 227 229 928;" +
            "223 626 034 683 839 052 627 310 713 999 629 817 410 121;" +
            "924 622 911 233 325 139 721 218 253 223 107 233 230 124 233",
            "215->192->269->117->836->218->805->728->433->277->528->863->632->931->778->413->291->310->253", 
            9089
            )]
        [InlineData(
            "1;" +
            "8 9;" +
            "1 5 9;" +
            "4 5 2 3",
            "1->8->5->2", 
            16
            )]
        [InlineData(
            "3;" +
            "7 4;" +
            "2 4 7;" +
            "8 5 9 4",
            "3->4->7->4",
            18
            )]
        [InlineData(
            "8;" +
            "-3 4;" +
            "4 2 6;" +
            "1 1 3 1",
            "8->-3->4->1",
            10
            )]
        [InlineData(
            "-5;" +
            "-3 -1;" +
            "4 2 6;" +
            "1 1 3 1",
            null,
            0
            )]
        [InlineData(
            "1;" +
            "2 4;" +
            "4 2 6;" +
            "1 2 3 2",
            null,
            0
            )]
        [InlineData(
            "1;" +
            "2 2;" +
            "3 3 3;" +
            "3 5 7 9",
            null,
            0
            )]
        [InlineData(
            "0;" +
            "-3 -5;" +
            "-4 0 -6;" +
            "1 -1 3 1",
            "0->-3->0->3",
            0
            )]
        public void Test_TriangleMaximumPathAndSum_ByOddEvenRule_Is_Valid(string input, string expectedPath, int expectedSum)
        {
            List<List<int>> matrix = input.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(lvl1 => lvl1.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val.Trim())).ToList()).ToList();
            List<int> path;
            Triangle.FindMaximumPathByOddEvenRule(matrix, matrix.Count, out path);

            string pathString = null;
            int maxSum = 0;
            if (path != null)
            {
                pathString = string.Join("->", path);
                maxSum = path.Sum();
            }

            Assert.Equal(expectedPath, pathString);
            Assert.Equal(expectedSum, maxSum);
        }

        [Theory]
        [InlineData(
            "1;" +
            "8 9;" +
            "1 5 9;" +
            "4 5 2 3",
            22
            )]
        [InlineData(
            "3;" +
            "7 4;" +
            "2 4 6;" +
            "8 5 9 3",
            23
            )]
        [InlineData(
            "8;" +
            "-4 4;" +
            "2 2 6;" +
            "1 1 1 1",
            19
            )]
        public void Test_TriangleMaximumSum_Is_Valid(string input, int expectedSum)
        {
            List<List<int>> matrix = input.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(lvl1 => lvl1.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(val => int.Parse(val.Trim())).ToList()).ToList();
            int maximumSum = Triangle.FindMaximumSum(matrix, matrix.Count);

            Assert.Equal(expectedSum, maximumSum);
        }
    }
}
