using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess_solver
{
    public class ChessPiece : Piece
    {
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

        public ChessPiece(piece_colour _colour, piece_type _type)
        {
            graphic = '\u265B';
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

         #region moves
        public List<move> PossibleMoves(Board b, (int,int) position)
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
            //    return HorsemanMoves(b); }

            if(this.type == piece_type.KING)
            {
                moves = KingMoves(b, position);

            }
            if(this.type == piece_type.PAWN)
            {
                moves = PawnMoves(b, position);

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
                if (position.Item1 + m.Item1 > 7 || position.Item2 + m.Item2 > 7 || 
                    position.Item1 + m.Item1 < 0 || position.Item2 + m.Item2 < 0) {
                    toRemove.Add(m);
                }
            }
            foreach (var m in toRemove)
            {
                moves.Remove(m);
            }
            //For each possible move, check if its occupied and if so, which colour
            foreach (var m in moves)
            {
                //Horsemen use different movement rules, so they get their own block
                if(this.type == ChessPiece.piece_type.HORSEMAN)
                {
                    if (!(b.b[position.Item1 + m.Item1][position.Item2 + m.Item2] is null))//Check if theres another piece in the square
                    {
                        //Check the colour of the piece. If it's the same colour, stop movement.
                        if (((ChessPiece)b.b[position.Item1 + 1][position.Item2 + 1]).colour == this.colour)
                        {   
                            moves.Remove(m);
                        }

                    }
                }
                else //anything that isn't a horseman or pawn
                {
                    if (!(b.b[position.Item1 + m.Item1][position.Item2 + m.Item2] is null))//Check if theres another piece in the square
                    {
                        //Check the colour of the piece. If it's the same colour, stop movement.
                        if (((ChessPiece)b.b[position.Item1 + m.Item1][position.Item2 + m.Item2]).colour == this.colour)
                        {
                            toRemove.Add(m);
                        }

                    }
                }
            }
            foreach (var m in toRemove)
            {
                moves.Remove(m);
            }
            foreach (var m in moves)
            {

                output.Add(new move((position.Item1 + m.Item1, position.Item2+m.Item2),position, this));
            }
            return output;
        }

        //private List<move> BishopMoves(Board b)
        //{

        //}        
        //private List<move> RookMoves(Board b)
        //{

        //}        
        private List<(int,int)> KingMoves(Board b, (int,int) position)
        {
            List<(int, int)> moves = new List<(int, int)>();
            moves.Add((1, 1));
            moves.Add((1, 0));
            moves.Add((1, -1));
            moves.Add((0, 1));
            moves.Add((0, -1));
            moves.Add((-1, -1));
            moves.Add((-1, 0));
            moves.Add((-1, 1));
            return moves;
        }
        private List<(int, int)> PawnMoves(Board b, (int,int) position)
        {
            //All Horizontal moves will be multiplied by this to ensure the pawns only go 'forwards'
            int compass = -1;
            if (colour == ChessPiece.piece_colour.BLACK)
            {
                compass = 1;
            }
            List<(int, int)> possible_moves = new List<(int, int)>();


            //Move diagonal, but only if there's another piece in that square
            if ((position.Item1+ 1) < 7 && 
                (position.Item1+ 1) > 0 && 
                position.Item2+ (1 * compass) < 7 && 
                position.Item2+ (1 * compass) > 0)
            {
                if (!(b.b[position.Item1+ 1][position.Item2+ (1 * compass)] is null))
                {
                    possible_moves.Add((1, 1 * compass));
                }
            }
            if ((position.Item1- 1) < 7 &&
                (position.Item1- 1) > 0 &&
                position.Item2+ (1 * compass) < 7 &&
                position.Item2+ (1 * compass) > 0)
            {
                if (!(b.b[position.Item1- 1][position.Item2+ (1 * compass)] is null))
                {
                    possible_moves.Add((-1, 1 * compass));
                }
            }
            //Move 'forward' one space if there's no piece in its way
            //Check if it'll be out of range
            if (position.Item2+ (1 * compass) < 7 && position.Item2+ (1 * compass) >= 0)
            {
                if (b.b[position.Item1][position.Item2 + (1 * compass)] is null)
                {
                    possible_moves.Add((0, 1 * compass));
                }
            }
            //Check if the pawn is in its original place. If so, it can move up to 2 spaces.
            if ((position.Item2== 1 && colour == piece_colour.BLACK) ||
                (position.Item2== 6 && colour == piece_colour.WHITE))
            {
                {
                    if (b.b[position.Item1][position.Item2 + (1 * compass)] is null)
                    {
                        possible_moves.Add((0, 2 * compass));
                    }
                }
            }
            return possible_moves;
        }
        #endregion moves

        public override string ToString()
        {
            return $"{this.colour} {this.type}";
        }
    }
    public struct move
    {
        public (int, int) to;
        public (int, int) from;
        public ChessPiece piece;

        public move((int,int) _to, (int,int) _from, ChessPiece _piece)
        {
            to = _to;
            piece = _piece;
            from = _from;
        }

        public override string ToString()
        {
            return $"From: {from.Item1},{from.Item2}  to: {to.Item1},{to.Item2}";
        }
    }
}
