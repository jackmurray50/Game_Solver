using System;
using System.Collections;
using chess_solver.GameTree;
using System.Diagnostics;
using System.Collections.Generic;

namespace chess_solver
{
    class Solver
    {
        static void Main(string[] args)
        {

            SolveChess();
        }

        /// <summary>
        /// One of a few 'solve' methods. It'll create a large binary tree and try to go through
        /// every possible permutation of the game, trying to solve it.
        /// It'll return True if it ever actually solves it, though that's largely impossible for
        /// chess.
        /// Want to make it depth-first so 'completed' branches can be deleted, cleaning up memory and cutting down
        /// on breadth.
        /// </summary>
        /// <returns>True when (if?) its solved.</returns>
        public static bool SolveChess()
        {
            Board b = BaseChessBoard();
            List<GameTree<move>> gametrees = new List<GameTree<move>>();
            foreach(move m in b.PossibleMoves())
            {
                gametrees.Add(new GameTree<move>(new Node<move>(null, m)));
                SolveChess(b, gametrees[gametrees.Count - 1 ].root);
            }
            //The GameTree all moves will be inserted into

            return true;
        }
        /// <summary>
        /// Recursive SolveChess function.
        /// </summary>
        /// <param name="b">The board to start working on</param>
        /// <param name="cur">The node to add children to</param>
        /// <returns></returns>
        private static int iterations = 0;
        public static void SolveChess(Board b, Node<move> cur)
        {
            iterations++;
            //Create a copy of the board
            Board newBoard = b.Copy();
            foreach(move m in newBoard.PossibleMoves())
            {
                Node<move> newnode = new Node<move>(cur, m);
                if (newBoard.MakeMove(m))
                {
                    //In an async function, add the required moves to a text file.
                    //For now, just yell
                    Console.WriteLine("SOMEONE WON");
                }
                else
                {
                    string output = newBoard.ToString(m);
                    foreach (char c in output)
                    {
                        //The string is formatted so the place the piece came from is
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
                    SolveChess(newBoard, newnode);
                }
            }
        }

        public static Board BaseChessBoard()
        {
            Board board = new Board(8, 8, Board.GameType.CHESS, ChessPiece.piece_colour.WHITE);
            board.SetUp(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK, 0,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN, 1,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP, 2,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.QUEEN, 3,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.KING, 4,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP, 5,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN, 6,0),
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK, 7,0),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK, 0,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN, 1,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP, 2,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.QUEEN, 3,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.KING, 4,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP, 5,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN, 6,7),
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK, 7,7)
                );
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.PAWN, i, 6));
            }            
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.PAWN, i, 1));
            }

            return board;
        }

        public static int WhiteWins = 0;
        public static int BlackWins = 0;
        public static int draws = 0;
    }
}
