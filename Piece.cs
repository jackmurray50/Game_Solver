using System;
using System.Collections.Generic;
using System.Text;

namespace chess_solver
{
    public abstract class Piece
    {
        public char console_graphic { get; set; }
        public abstract Piece Copy();
        public enum piece_colour { };
        public enum piece_type { };
    }
}
