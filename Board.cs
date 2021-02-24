using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess_solver
{
    /// <summary>
    /// A chess board with pieces
    /// </summary>
    public class Board
    {
        public enum GameType
        {
            CHESS
        }
        public GameType gt {get;}
        public (int, int) size = (0,0);
        /// <summary>
        /// Our current board. Should be a list of 8 lists that contain 8 pieces    
        /// </summary>
        private List<List<Piece>> board;
        public List<List<Piece>> b {
            get { return board; }
            private set { board = value; }
        }

        public ChessPiece.piece_colour turn = ChessPiece.piece_colour.WHITE;
        public int TurnsSinceCapture = 0;
        /// <summary>
        /// A constructor for a standard chess board.
        /// </summary>
        public Board(int _x, int _y, GameType _gt, ChessPiece.piece_colour _turn, int _TurnsSinceCapture)
        {
            turn = _turn;
            size = (_x, _y);
            gt = _gt;

            this.board = new List<List<Piece>>();
            //First, set the whole board to null
            for (int x = 0; x < _x; x++)
            {
                board.Add(new List<Piece>());
                for(int y = 0; y < _y; y++)
                {
                    board[x].Add(null);
                }
            }
        }
        public Board(int _x, int _y, GameType _gt, ChessPiece.piece_colour _turn) : this(_x, _y, _gt, _turn, 0)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns>True if the king has been slain, false otherwise.</returns>
        public bool MakeMove(move m)
        {

            bool returnable = false;

            //Hold the piece we're moving in memory
            Piece movingPiece = (ChessPiece)board[m.from.Item1][m.from.Item2];

            //See what you're killing
            if(!(board[m.to.Item1][m.to.Item2] is null))
            {
                //If its a king, make sure to return true at the end
                if(((ChessPiece)board[m.to.Item1][m.to.Item2]).t == ChessPiece.piece_type.KING)
                {
                    returnable = true;
                }
            }
            //Put the piece in its new position, overwriting the old space
            board[m.to.Item1][m.to.Item2] = movingPiece;
            //Remove the piece being moved from its original position
            board[m.from.Item1][m.from.Item2] = null;

            //Change turn
            if (this.turn == ChessPiece.piece_colour.BLACK)
            {
                this.turn = ChessPiece.piece_colour.WHITE;
            }
            else
            {
                this.turn = ChessPiece.piece_colour.BLACK;
            }
            
            //50 turn rule
            if(TurnsSinceCapture > 50)
            {
                //Signifies a draw
                returnable = true;
            }
            this.TurnsSinceCapture++;   
            
            return returnable;
        }

        public List<move> PossibleMoves()
        {
            List < move > pm= new List<move>();
            if(this.gt == GameType.CHESS)
            {
                for(int x = 0; x < board.Count; x++)
                {
                    for(int y = 0; y < board[x].Count; y++)
                    {
                        if(!(board[x][y] is null))
                        {
                            pm.AddRange(((ChessPiece)board[x][y]).PossibleMoves(this, (x,y)));
                        }
                    }
                }
            }
            return pm;
        }

        public bool SetUp(ChessPiece piece, int x, int y) {
            board[x][y] = piece;

            return true;
        }
        public override string ToString()
        {
            string output = "  12345678\n-----------";
            for(int x = 0; x < board.Count; x++)
            {
                output += "\n" + (char)(x+65) + "|";
                for (int y = 0; y < board[x].Count; y++)
                {
                    //No piece
                    if (!(board[x][y] is null))
                    {
                        output += board[x][y].console_graphic;
                    }
                    else
                    {
                        //Figure out if a square is white or black
                        if ((x + y) % 2 == 0)
                        {
                            output += 'X';
                        }
                        else
                        {
                            output += '#';
                        }
                    }
                }
                output += "|";
            }
            output += "\n-----------\n";
            return output;
        }

        public string ToString(move m) {
            string output = "  12345678\n-----------";
            for (int x = 0; x < board.Count; x++)
            {
                output += "\n" + (char)(x + 65) + "|";
                for (int y = 0; y < board[x].Count; y++)
                {
                    if(m.from.Item1 == x && m.from.Item2 == y)
                    {
                        output += "[";
                    }
                    if (m.to.Item1 == x && m.to.Item2 == y)
                    {
                        output += "{";
                    }
                    //No piece
                    if (!(board[x][y] is null))
                    {
                        output += board[x][y].console_graphic;
                    }
                    else
                    {
                        //Figure out if a square is white or black
                        if ((x + y) % 2 == 0)
                        {
                            output += 'X';
                        }
                        else
                        {
                            output += '#';
                        }
                    }
                    if (m.from.Item1 == x && m.from.Item2 == y)
                    {
                        output += "]";
                    }
                    if (m.to.Item1 == x && m.to.Item2 == y)
                    {
                        output += "}";
                    }
                }
                output += "|";
            }
            output += "\n-----------\n";
            return output;
        }
    
        //Create a deep copy
        public Board Copy()
        {
            //Create the board
            Board newBoard = new Board(board.Count, board[0].Count, GameType.CHESS, this.turn, this.TurnsSinceCapture);
            for(int x = 0; x < board.Count; x++)
            {
                for(int y = 0; y < board[x].Count; y++)
                {
                    if(board[x][y] is null)
                    {
                        newBoard.b[x][y] = null;
                    }
                    else
                    {
                        ChessPiece temp = (ChessPiece)board[x][y];
                        ChessPiece newPiece = new ChessPiece(temp.c, temp.t);
                        newBoard.SetUp(newPiece, x, y);
                    }
                }
            }
            return newBoard;

        }
    }
}
