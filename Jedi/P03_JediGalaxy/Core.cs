using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P03_JediGalaxy
{
    public class Core
    {
        public long Run()
        {
            int x, y;
            GetDimensions(out x, out y);

            int[,] matrix = new int[x, y];

            int value = 0;
            value = CreateMatrix(x, y, matrix, value);

            long sum = PlayTheGame(matrix);
            return sum;
        }

        private static void GetDimensions(out int x, out int y)
        {
            int[] dimestions = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            x = dimestions[0];
            y = dimestions[1];
        }

        private static long PlayTheGame(int[,] matrix)
        {
            string command = Console.ReadLine();
            long sum = 0;
            while (command != "Let the Force be with you")
            {
                int[] ivoS = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                
                EvilTurn(matrix);

                int xI = ivoS[0];
                int yI = ivoS[1];
                IvoTurn(matrix, ref sum, ref xI, ref yI);

                command = Console.ReadLine();
            }

            return sum;
        }

        private static void IvoTurn(int[,] matrix, ref long sum, ref int xI, ref int yI)
        {
            while (xI >= 0 && yI < matrix.GetLength(1))
            {
                if (ItsInRange(matrix, xI, yI))
                {
                    sum += matrix[xI, yI];
                }

                yI++;
                xI--;
            }
        }

        private static void EvilTurn(int[,] matrix)
        {
            int[] evil = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int xE = evil[0];
            int yE = evil[1];

            while (xE >= 0 && yE >= 0)
            {
                if (ItsInRange(matrix, xE, yE))
                {
                    matrix[xE, yE] = 0;
                }
                xE--;
                yE--;
            }
        }

        private static bool ItsInRange(int[,] matrix, int x, int y)
        {
            return x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1);
            
        }

        private static int CreateMatrix(int x, int y, int[,] matrix, int value)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    matrix[i, j] = value++;
                }
            }

            return value;
        }
    }
}
