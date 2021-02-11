using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver
{
    /// <summary>
    /// A chess board with pieces
    /// </summary>
    class Board
    {
        /// <summary>
        /// Our current board. Should be a list of 8 lists that contain 8 pieces    
        /// </summary>
        List<List<Piece>> board = new List<List<Piece>>();
        /// <summary>
        /// A constructor for a standard chess board.
        /// </summary>
        public Board(int _x, int _y)
        {
            //First, set the whole board to null
            for(int x = 0; x < _x; x++)
            {
                board.Add(new List<Piece>());
                for(int y = 0; y < 8; y++)
                {
                    board[x].Add(null);
                }
            }
        }

        public bool SetUp(params piece_position[] pieces) {
            try
            {
                foreach(var p in pieces)
                {
                    board[p.x][p.y] = p.p;
                }
            } catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            string output = "";
            for(int x = 0; x < board.Count; x++)
            {
                for(int y = 0; y < board[x].Count; y++)
                {
                    //No piece
                    if(!(board[x][y] is null))
                    {
                        output += board[x][y].console_graphic;
                    }
                    else
                    {
                        //Figure out if a square is white or black
                        if((x + y) % 2 == 0)
                        {
                            output += 'X';
                        }
                        else
                        {
                            output += '#';
                        }
                    }
                }
                output += "\n";
            }
            return output;
        }
    }
    public struct piece_position
    {
        public piece_position(Piece _p, int _x, int _y)
        {
            p = _p;
            x = _x;
            y = _y;
        }
        public Piece p { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
