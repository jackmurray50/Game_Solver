using System;
using System.Collections;
using chess_solver.GameTree;
using System.Diagnostics;

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
            Console.Write(b.ToString());
            //The stack of moves. 
            Stack stack = new Stack();

            //The GameTree all moves will be inserted into
            GameTree<Board> gt = new GameTree<Board>(new Node<Board>(null, b));
            SolveChess(b, gt.root);

            return true;
        }
        /// <summary>
        /// Recursive SolveChess function.
        /// </summary>
        /// <param name="b">The board to start working on</param>
        /// <param name="cur">The node to add children to</param>
        /// <returns></returns>
        public static bool SolveChess(Board b, Node<Board> cur)
        {
            Board newBoard = ObjectExtensions.Copy(b);

            foreach(var m in newBoard.PossibleMoves())
            {
                //Things that need to happen:
                //Each new child needs its own slightly modified board
                if (!newBoard.MakeMove(m)) {
                    //Console.WriteLine(newBoard.ToString());
                    Node<Board> node = new Node<Board>(newBoard);
                    cur.children.Add(node);
                    SolveChess(newBoard, node);
                }
                else
                {
                    Console.WriteLine(newBoard.ToString());
                    Console.ReadKey();
                }
            }
            Debug.WriteLine("End of branch");
            return true;
        }

        public static Board BaseChessBoard()
        {
            Board board = new Board(8, 8, Board.GameType.CHESS, ChessPiece.piece_colour.WHITE);
            board.SetUp(
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK, 0,0), 0, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN, 1,0), 1,0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP, 2,0), 2, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.QUEEN, 3,0), 3, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.KING, 4,0), 4, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.BISHOP, 5,0), 5, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.HORSEMAN, 6,0), 6, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.ROOK, 7,0), 7, 0),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK, 0,7), 0, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN, 1,7), 1, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP, 2,7), 2, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.QUEEN, 3,7), 3, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.KING, 4,7), 4, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.BISHOP, 5,7), 5, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.HORSEMAN, 6,7), 6, 7),
                new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.ROOK, 7,7), 7, 7)
                );
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.WHITE, ChessPiece.piece_type.PAWN, i, 6), i, 6));
            }            
            //Handle pawns in a for loop
            for(int i = 0; i < 8; i++)
            {
                board.SetUp(
                    new piece_position(
                    new ChessPiece(ChessPiece.piece_colour.BLACK, ChessPiece.piece_type.PAWN, i, 1), i, 1));
            }

            return board;
        }

        public static int WhiteWins = 0;
        public static int BlackWins = 0;
    }
}
