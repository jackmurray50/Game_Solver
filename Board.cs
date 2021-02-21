﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess_solver
{
    /// <summary>
    /// A chess board with pieces
    /// </summary>
    class Board
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

        private List<Piece> pieces;
        /// <summary>
        /// A constructor for a standard chess board.
        /// </summary>
        public Board(int _x, int _y, GameType _gt, ChessPiece.piece_colour _turn)
        {
            turn = _turn;
            pieces = new List<Piece>();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns>True if the king has been slain, false otherwise.</returns>
        public bool MakeMove(move m)
        {
            if (gt == GameType.CHESS)
            {
                //Check if the king is dead
                if (!(board[m.to.Item1][m.to.Item2] is null)){ 
                    if(((ChessPiece)board[m.to.Item1][m.to.Item2]).t == ChessPiece.piece_type.KING){
                        return true;
                    }   
                }
            }


            //Move piece

            //Hold the piece we're moving in memory
            Piece movingPiece = (ChessPiece)board[m.from.Item1][m.from.Item2];
            ((ChessPiece)movingPiece).x = m.to.Item1;
            ((ChessPiece)movingPiece).y = m.to.Item2;
            //See what you're killing
            if(!(board[m.to.Item1][m.to.Item2] is null))
            {
               pieces.Remove(board[m.to.Item1][m.to.Item2]);
               Console.WriteLine(((ChessPiece)movingPiece).c + "_" + ((ChessPiece)movingPiece).t + 
               " killed a " +
               ((ChessPiece)board[m.to.Item1][m.to.Item2]).c + "_" + ((ChessPiece)board[m.to.Item1][m.to.Item2]).t + "\n");
            }
            //Put the piece in its new position, overwriting the old space
            board[m.to.Item1][m.to.Item2] = movingPiece;
            //Tell the piece where it is now
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
            return false;
        }

        public List<move> PossibleMoves()
        {
            List < move > pm= new List<move>();
            if(this.gt == GameType.CHESS)
            {
                foreach (ChessPiece p in pieces)
                {
                    if(p.c == this.turn)
                    {
                        pm.AddRange(p.PossibleMoves(this));
                    }
                }
            }
            return pm;
        }

        public bool SetUp(params ChessPiece[] pieces) {
            this.pieces.Clear();
            try
            {
                foreach(var p in pieces)
                {
                    board[p.x][p.y] = p;
                }
            } catch(Exception)
            {
                return false;
            }
            for(int x = 0; x < board.Count; x++)
            {
                for(int y = 0; y < board[x].Count; y++)
                {
                    if(!(board[x][y] is null)){
                        ChessPiece toAdd = (ChessPiece)board[x][y];
                        toAdd.x = x;
                        toAdd.y = y;
                        this.pieces.Add(toAdd);
                    }
                }
            }

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
    }
}
