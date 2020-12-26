using System;

namespace P06_Sneaking
{
    internal class Core
    {
        private int n;
        private char[][] room;

        public Core(int n)
        {
            this.n = n;
            this.room = room = new char[n][];
            
        }

        internal  void Run()
        {
            FillTheRoom(n);

            var moves = Console.ReadLine().ToCharArray();

            int[] samPosition = FindSamPosition();

            for (int i = 0; i < moves.Length; i++)
            {
                MoveEnemy();

                int[] getEnemy = new int[2];

                getEnemy = SetEnemyPosition(samPosition, getEnemy);

                bool firstPosComp = samPosition[1] < getEnemy[1];
                char enemyType = 'd';
                CheckIfSamHitsEnemy(samPosition, getEnemy, firstPosComp, enemyType);


                firstPosComp = getEnemy[1] < samPosition[1];
                enemyType = 'b';
                CheckIfSamHitsEnemy(samPosition, getEnemy, firstPosComp, enemyType);

                MoveSam(moves, samPosition, i);

                getEnemy = SetEnemyPosition(samPosition, getEnemy);

                CheckForNikoladzeDeath(samPosition, getEnemy);
            }
        }
        private  void CheckForNikoladzeDeath(int[] samPosition, int[] getEnemy)
        {
            if (room[getEnemy[0]][getEnemy[1]] == 'N' && samPosition[0] == getEnemy[0])
            {
                room[getEnemy[0]][getEnemy[1]] = 'X';
                Console.WriteLine("Nikoladze killed!");
                for (int row = 0; row < room.Length; row++)
                {
                    for (int col = 0; col < room[row].Length; col++)
                    {
                        Console.Write(room[row][col]);
                    }
                    Console.WriteLine();
                }
                Environment.Exit(0);
            }
        }

        private  void MoveSam(char[] moves, int[] samPosition, int i)
        {
            room[samPosition[0]][samPosition[1]] = '.';
            switch (moves[i])
            {
                case 'U':
                    samPosition[0]--;
                    break;
                case 'D':
                    samPosition[0]++;
                    break;
                case 'L':
                    samPosition[1]--;
                    break;
                case 'R':
                    samPosition[1]++;
                    break;
                default:
                    break;
            }
            room[samPosition[0]][samPosition[1]] = 'S';
        }

        private  void CheckIfSamHitsEnemy(int[] samPosition, int[] getEnemy, bool firstPositionComp, char enemyType)
        {
            if (firstPositionComp && room[getEnemy[0]][getEnemy[1]] == enemyType && getEnemy[0] == samPosition[0])
            {
                room[samPosition[0]][samPosition[1]] = 'X';
                Console.WriteLine($"Sam died at {samPosition[0]}, {samPosition[1]}");
                for (int row = 0; row < room.Length; row++)
                {
                    for (int col = 0; col < room[row].Length; col++)
                    {
                        Console.Write(room[row][col]);
                    }
                    Console.WriteLine();
                }
                Environment.Exit(0);
            }
        }

        private  int[] SetEnemyPosition(int[] samPosition, int[] getEnemy)
        {
            for (int j = 0; j < room[samPosition[0]].Length; j++)
            {
                if (room[samPosition[0]][j] != '.' && room[samPosition[0]][j] != 'S')
                {
                    getEnemy[0] = samPosition[0];
                    getEnemy[1] = j;
                }
            }

            return getEnemy;
        }

        private  void MoveEnemy()
        {
            for (int row = 0; row < room.Length; row++)
            {
                for (int col = 0; col < room[row].Length; col++)
                {
                    if (room[row][col] == 'b')
                    {
                        if (CheckIfEnemyIsIn(row, col + 1))
                        {
                            room[row][col] = '.';
                            room[row][col + 1] = 'b';
                            col++;
                        }
                        else
                        {
                            room[row][col] = 'd';
                        }
                    }
                    else if (room[row][col] == 'd')
                    {
                        if (CheckIfEnemyIsIn(row, col - 1))
                        {
                            room[row][col] = '.';
                            room[row][col - 1] = 'd';
                        }
                        else
                        {
                            room[row][col] = 'b';
                        }
                    }
                }
            }
        }

        private  bool CheckIfEnemyIsIn(int row, int col)
        {
            return row >= 0 && row < room.Length && col >= 0 && col < room[row].Length;
        }

        private  int[] FindSamPosition()
        {
            int[] samPosition = new int[2];
            for (int row = 0; row < room.Length; row++)
            {
                for (int col = 0; col < room[row].Length; col++)
                {
                    if (room[row][col] == 'S')
                    {
                        samPosition[0] = row;
                        samPosition[1] = col;
                    }
                }
            }

            return samPosition;
        }

        private  void FillTheRoom(int n)
        {
            for (int row = 0; row < n; row++)
            {
                var input = Console.ReadLine().ToCharArray();
                room[row] = new char[input.Length];
                for (int col = 0; col < input.Length; col++)
                {
                    room[row][col] = input[col];
                }
            }
        }
    }
}