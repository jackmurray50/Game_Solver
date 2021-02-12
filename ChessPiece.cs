using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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
        private piece_colour colour;
        public piece_colour c
        {
            get { return colour; }
            protected set { colour = value; }
        }
        private piece_type type;
        public piece_type t
        {
            get { return type; }
            protected set { type = value; }
        }

        public char graphic { get; set; }

        public int x = 0;
        public int y = 0;

        public ChessPiece(piece_colour _colour, piece_type _type, int _x, int _y)
        {
            graphic = '\u265B';
            colour = _colour;
            type = _type;
            x = _x;
            y = _y;
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

         #region moves
        public List<move> PossibleMoves(Board b)
        {
            //The fully featured set of moves including starting position
            List < move > output = new List<move>();
            //Just the moves, once 
            List<(int, int)> moves = new List<(int, int)>();
            //if(this.type == piece_type.BISHOP)
            //{
            //    return BishopMoves(b);

            //}else if(this.type == piece_type.HORSEMAN)
            //{
            //    return HorsemanMoves(b);

            //}else if(this.type == piece_type.KING)
            //{
            //    return KingMoves(b);

            //} else
            if(this.type == piece_type.PAWN)
            {
                moves = PawnMoves(b);

            }
            foreach(var m in moves)
            {
                Debug.Write(this.colour + "_" + this.type + "\n");
                Debug.Write("\tFrom: " + x + ", " + y + "\n");
                Debug.Write("\tTo: " + (m.Item1 + x) + ", " + (m.Item2 + y )+ "\n");
            }
            //else if(this.type == piece_type.QUEEN)
            //{
            //    return QueenMoves(b);

            //}else if(this.type == piece_type.ROOK)
            //{
            //    return RookMoves(b);
            //}

            //Move filtering: Go through all the moves a piece COULD make, and filter it down.
            //First, see if the piece would go off the board; obviously anything that would make it do that
            //isn't possible.
            List<(int,int)> toRemove = new List<(int,int)>();
            foreach(var m in moves)
            {
                //Check if it'd go off the board; if so, don't allow it to.
                if (x + m.Item1 > 7 || y + m.Item2 > 7 || x + m.Item1 < 0 || y + m.Item2 < 0) {
                    toRemove.Add(m);
                }
            }
            foreach(var m in toRemove)
            {
                    moves.Remove(m);
            }
            //For each possible move, check if its occupied and if so, which colour
            foreach (var m in moves)
            {
                //Horsemen use different movement rules, so they get their own block
                if(this.type == ChessPiece.piece_type.HORSEMAN)
                {
                    if (!(b.b[x + m.Item1][y + m.Item2] is null))//Check if theres another piece in the square
                    {
                        //Check the colour of the piece. If it's the same colour, stop movement.
                        if (((ChessPiece)b.b[x + 1][y + 1]).colour == this.colour)
                        {   
                            moves.Remove(m);
                        }

                    }
                }
                else //anything that isn't a horseman
                {
                    if (!(b.b[x + m.Item1][y + m.Item2] is null))//Check if theres another piece in the square
                    {
                        //Check the colour of the piece. If it's the same colour, stop movement.
                        if (((ChessPiece)b.b[x + m.Item1][y + m.Item2]).colour == this.colour)
                        {

                        }

                    }
                }

            }
            foreach(var m in moves)
            {
                output.Add(new move((x+m.Item1,y+m.Item2), (x, y)));
            }
            return output;
        }

        //private List<move> BishopMoves(Board b)
        //{

        //}        
        //private List<move> RookMoves(Board b)
        //{

        //}        
        private List<(int,int)> PawnMoves(Board b)
        {
            //All Vertical moves will be multiplied by this to make working out which way is 'south'
            //easier.
            int compass = -1;
            if (colour == ChessPiece.piece_colour.BLACK)
            {
                compass = 1;
            }
            List<(int, int)> possible_moves = new List<(int, int)>();

            //Move 'forward' one space
            possible_moves.Add((0, 1*compass));


            //Check if the pawn is in its original place. If so, it can move up to 2 spaces.
            if (y == 1 && colour == piece_colour.BLACK ||
                y == 6 && colour == piece_colour.WHITE)
            {
                possible_moves.Add((0, compass * 2));
            }
            return possible_moves;
        }
        //Move diagonal, but only if there's another piece in that square
        #endregion moves

        public override Piece Copy()
        {
            return new ChessPiece(this.c, this.t, this.x, this.y);
        }
    }
    public struct move
    {
        public (int, int) to;
        public (int, int) from;

        public move((int,int) _to, (int,int) _from)
        {
            to = _to;
            from = _from;
        }
    }
}
