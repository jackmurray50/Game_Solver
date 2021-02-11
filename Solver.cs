using System;
using System.Collections;
using chess_solver.GameTree;

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
        /// It'll return Tru0e if it ever actually solves it, though that's largely impossible for
        /// chess.
        /// </summary>
        /// <returns>True when (if?) its solved.</returns>
        public static bool SolveChess()
        {
            Board b = BaseChessBoard();
            Console.Write(b.ToString());
            //The stack of moves. 
            Stack stack = new Stack();

            //The GameTree all moves will be inserted into
            GameTree<Board> gt = new GameTree<Board>(new Node<Board>(null, b));

            

            //Step one: List all possible moves for current state. Should put this on a Stack.
            //Step two: Make a move, and publish it to the Tree.
            //Step three: Check if the game is won
            //Step four: Show winning tree
            //Step five: Show tree depth
            //Step six: Show time elapsed
            //Step seven: Repeat

            return true;
        }

        public static Board BaseChessBoard()
        {
            Board board = new Board(8, 8);
            board.SetUp(
                new piece_position(
                    new ChessPiece("L_Rook", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK), 0, 0),
                new piece_position(
                    new ChessPiece("L_Horseman", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN), 0, 1),
                new piece_position(
                    new ChessPiece("L_Bishop", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP), 0, 2),
                new piece_position(
                    new ChessPiece("Queen", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.QUEEN), 0, 3),
                new piece_position(
                    new ChessPiece("King", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.KING), 0, 4),
                new piece_position(
                    new ChessPiece("R_Bishop", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP), 0, 5),
                new piece_position(
                    new ChessPiece("R_Horseman", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN), 0, 6),
                new piece_position(
                    new ChessPiece("R_Rook", ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK), 0, 7),
                new piece_position(
                    new ChessPiece("L_Rook", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK), 7, 0),
                new piece_position(
                    new ChessPiece("L_Horseman", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN), 7, 1),
                new piece_position(
                    new ChessPiece("L_Bishop", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP), 7, 2),
                new piece_position(
                    new ChessPiece("Queen", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.QUEEN), 7, 3),
                new piece_position(
                    new ChessPiece("King", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.KING), 7, 4),
                new piece_position(
                    new ChessPiece("R_Bishop", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP), 7, 5),
                new piece_position(
                    new ChessPiece("R_Horseman", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN), 7, 6),
                new piece_position(
                    new ChessPiece("R_Rook", ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK), 7, 7)
                );
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new piece_position(
                    new ChessPiece("Pawn_" + i, ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.PAWN), 6, i));
            }            
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new piece_position(
                    new ChessPiece("Pawn_" + i, ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.PAWN), 1, i));
            }

            return board;
        }
    }
}
