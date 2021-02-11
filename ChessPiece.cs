using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver
{
    class ChessPiece : Piece
    {
        public string name { get; set; }
        new public enum piece_colour
        {
            BLACK,
            WHITE
        }
        new public enum piece_type
        {
            PAWN,
            ROOK,
            HORSEMAN,
            BISHOP,
            QUEEN,
            KING
        }

        public char graphic { get; set; }
        public piece_colour colour { get; set; }
        public piece_type type { get; set; }

        public ChessPiece(string _name, piece_colour _colour, piece_type _type)
        {
            graphic = '\u265B';
            name = _name;
            colour = _colour;
            type = _type;
            #region graphic_selector
            if (_type == piece_type.PAWN)
            {
                if(_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265F';
                    console_graphic = 'p';
                }
                else
                {
                    graphic = '\u2659';
                    console_graphic = 'P';
                }
                
            }
            else if (_type == piece_type.BISHOP)
            {
                if (_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265D';
                    console_graphic = 'b';
                }
                else
                {
                    graphic = '\u2657';
                    console_graphic = 'B';
                }
            }
            else if (_type == piece_type.HORSEMAN)
            {
                if (_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265E';
                    console_graphic = 'h';
                }
                else
                {
                    graphic = '\u2658';
                    console_graphic = 'H';
                }
            }
            else if (_type == piece_type.ROOK)
            {
                if (_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265C';
                    console_graphic = 'r';
                }
                else
                {
                    graphic = '\u2656';
                    console_graphic = 'R';
                }
            }            
            else if (_type == piece_type.KING)
            {
                if (_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265A';
                    console_graphic = 'k';
                }
                else
                {
                    graphic = '\u2654';
                    console_graphic = 'K';
                }
            }            
            else if (_type == piece_type.QUEEN)
            {
                if (_colour == piece_colour.BLACK)
                {
                    graphic = (char)'\u265B';
                    console_graphic = 'q';
                }
                else
                {
                    graphic = '\u2655';
                    console_graphic = 'Q';
                }
            }            
            #endregion graphic_selector
        }
    }
    struct move
    {
        (int, int) to;
        (int, int) from;

        public move((int,int) _to, (int,int) _from)
        {
            to = _to;
            from = _from;
        }
    }
}
