using System;
using System.Collections;
using chess_solver.GameTree;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chess_solver
{
    class Solver
    {
        static void Main(string[] args)
        {

            SolveChess();
        }

        /// <summary>
        /// One of a few 'solve' methods. It'll create a large tree and try to go through
        /// every possible permutation of the game, trying to solve it.
        /// It'll return True if it ever actually solves it, though that's largely impossible for
        /// chess.
        /// Want to make it depth-first so 'completed' branches can be deleted, cleaning up memory and cutting down
        /// on breadth.
        /// </summary>
        /// <returns>True when (if?) its solved.</returns>
        public static bool SolveChess()
        {
            double Shannon = Math.Pow(10, 40);
            double moves = 0;
            Board b = BaseChessBoard();
            Stack<(move, Board)> operationStack = new Stack<(move, Board)>();
            //Push the initial stack
            foreach(move m in b.PossibleMoves())
            {
                operationStack.Push((m, b));
            }
            //While there's still operations on the stack
            while(operationStack.Count > 0)
            {
                moves++;
                //Step 0: Grab the necessary variables
                (move, Board) operation = operationStack.Pop();
                move opMove = operation.Item1;
                Board opBoard = operation.Item2;
                //Step 1: Perform the operation
                moves++;
                if (opBoard.MakeMove(opMove))
                {
                    //Step 2: Check if it's a win or not.
                    string output = opBoard.ToString(opMove);
                    foreach (char c in output)
                    {
                        //the string is formatted so the place the piece came from is
                        //in red, and the place its going is in green
                        if (c == '[')
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                        else if (c == ']' || c == '}')
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else if (c == '{')
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.Write(c);
                        }
                    }
                    Console.Write($"\nBlack wins: {BlackWins}\nWhite wins: {WhiteWins}\nDraws: {draws}\nTotal games: {BlackWins + WhiteWins + draws}\n");
                    DetermineWinner(opBoard);
                }
                else
                {
                    //Step 3: If its not a win, push more operations to the stack
                    foreach (move m in opBoard.PossibleMoves())
                    {
                        operationStack.Push((m, opBoard.Copy()));
                    }
                }
                Console.WriteLine("Percent to completion: " + (moves / Shannon).ToString("P10"));
            }

            return true;
        }

        public static Board BaseChessBoard()
        {
            Board board = new Board(8, 8, Board.GameType.CHESS, ChessPiece.piece_colour.WHITE);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK), 0,0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN), 1, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP), 2, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.QUEEN), 3, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.KING), 4, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP), 5, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN), 6, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK), 7, 0);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK), 0, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN), 1, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP), 2, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.QUEEN), 3, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.KING), 4, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP), 5, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN), 6, 7);
            board.SetUp(new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK), 7,7);
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.PAWN), i, 6);
            }            
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.PAWN), i, 1);
            }

            return board;
        }

        public static void DetermineWinner(Board b)
        {
            bool foundBlack = false;
            bool foundWhite = false;

            for(int x = 0; x < b.b.Count; x++)
            {
                for(int y = 0; y < b.b[x].Count; y++)
                {
                    //Check that the tile we're checking isn't null
                    if(!(b.b[x][y] is null))
                    {
                        ChessPiece p = (ChessPiece)b.b[x][y];
                        if(p.t == ChessPiece.piece_type.KING)
                        {
                            if(p.c == ChessPiece.piece_colour.BLACK)
                            {
                                foundBlack = true;
                            }
                            if (p.c == ChessPiece.piece_colour.WHITE)
                            {
                                foundWhite = true;
                            }
                        }
                    }
                }
            }

            if(foundBlack && foundWhite)
            {
                draws++;
            }
            else if(foundBlack && !foundWhite)
            {
                BlackWins++;
            }else if(!foundBlack && foundWhite)
            {
                WhiteWins++;
            }
        }

        public static int WhiteWins = 0;
        public static int BlackWins = 0;
        public static int draws = 0;
    }
}