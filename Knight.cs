using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Knight
{
    class Knight
    {
        public struct Moves
        {
            public int x, y, num_opciones;

            public Moves(int x, int y, int num_opciones = 11)
            {
                this.x = x;
                this.y = y;
                this.num_opciones = num_opciones;
            }
        }
        public static Moves[] options;
        public static int n;
        static void Main(string[] args)
        {
            n = 10;
            options = new Moves[]{
                new Moves(1, 2),
                new Moves(2, 1),
                new Moves(2,-1),
                new Moves(1,-2),
                new Moves(-1,-2),
                new Moves(-2,-1),
                new Moves(-2, 1),
                new Moves(-1, 2)
            };

            SetKnight();

            Console.ReadKey();
        }
        public static void PrintBoard(int?[,] board, int final = 0)
        {
            Console.Write("\n===========================================================\n");
            for (int i = 0; i < n; i++)
            {
                for (int f = 0; f < n; f++)
                {
                    if (board[i, f] != 0)
                    {
                        Console.Write("| " + board[i, f] + " |");
                        continue;
                    }
                    Console.Write("| 0 |");

                }
                Console.Write("\n===========================================================\n");
            }
        }

        public static int getOpciones(int x, int y, int?[,] board)
        {
            int count = 0;
            for (int i = 0; i < options.Length; i++)
            {
                int newx = options[i].x + x;
                int newy = options[i].y + y;
                try
                {
                    if (board[newx, newy] != null && board[newx, newy] == 0)
                    {
                        count++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }
            if (count == 0)
                return 11;
            return count;
        }

        public static int?[,] BlankBoard()
        {
            int?[,] board = new int?[n, n];
            for (int i = 0; i < n; i++)
                for (int f = 0; f < n; f++)
                    board[i, f] = 0;
            return board;

        }
        public static void SetKnight()
        {
            Random rand = new Random();
            int?[,] board;
            int x, y, count;
            do
            {
                board = BlankBoard();
                x = rand.Next(9);
                y = rand.Next(9);
                board[x, y] = count = 1;
                Console.WriteLine("Para \nx = " + x + "\ny = " + y);
            } while (!moveKnight(count, board, x, y));

        }

        public static bool moveKnight(int count, int?[,] board, int x, int y)
        {
            if (count >= (n*n)-1)
            {
                PrintBoard(board);
                return true;
            }
            // temp int for minimum options
            int min = 11;
            Moves nextMove = new Moves(11, 11);
            for (int i = 0; i < options.Length; i++)
            {
                int newx = options[i].x + x;
                int newy = options[i].y + y;
                try
                {
                    if (board[newx, newy] != null && board[newx, newy] == 0)
                    {
                        int opciones = getOpciones(newx, newy, board);
                        if (opciones < min)
                        {
                            nextMove.x = newx;
                            nextMove.y = newy;
                            min = opciones;

                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }
            if (min == 11)
            {
                PrintBoard(board);
                return false;
            }
            count += 1;
            board[nextMove.x, nextMove.y] = count;
            return moveKnight(count, board, nextMove.x, nextMove.y);

        }
    }
}
