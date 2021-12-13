using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO.Pipes;
using System.Threading;

namespace Chess
{
    public class Piece
    {
        public enum cast
        {
            king,
            queen,
            rook,
            bishop,
            knight,
            pawn
        }
        
        public enum team{black,white}

        public cast _cast;
        public team _Team;

        public bool _hasMoved=false;

        public Piece(cast c, team t) { _cast = c; _Team = t; }

        public override string ToString()
        {
            switch (_cast)
            {
                case cast.rook:
                    return "R";
                case cast.queen:
                    return "Q";
                case cast.pawn:
                    return "P";
                case cast.bishop:
                    return "B";
                case cast.knight:
                    return "k";
                case cast.king:
                    return "K";
                default:
                    return "X";
            }

            return "";
        }

        static public List<Move> PossibleMove(Board b, int x, int y)
        {
            List<Move> ret = new List<Move>();
            if (b.board[x, y] == null)
            {
                return ret;
            }
            Piece p = b.board[x, y];
            int temp, tempX, tempY;
            switch (p._cast)
            {
                case cast.pawn://=======================================================================================
                    if (p._Team == team.black)//si noir
                    {
                        if (b.board[x+1, y] == null)//si rien 1 devant
                        {
                            ret.Add(new Move(x, y, x + 1, y));
                            if (x==1)
                            {
                                if(b.board[x+2, y] == null)//si rien 2 devant 
                                    ret.Add(new Move(x, y, x + 2, y));
                            }
                        }
                        if(y!=0 && b.board[x+1,y-1]!=null )//manger1
                            if(b.board[x+1,y-1]._Team==team.white)
                                ret.Add(new Move(x, y, x+1, y-1));
                        if(y!=7 && b.board[x+1,y+1]!=null)//manger2
                            if(b.board[x+1,y+1]._Team==team.white)
                                ret.Add(new Move(x, y, x+1, y+1));
                    }
                    else
                    {
                        if (b.board[x-1, y] == null)//si rien 1 devant
                        {
                            ret.Add(new Move(x, y, x - 1, y));
                            if (x==6)
                            {
                                if(b.board[x-2, y] == null)//si rien 2 devant 
                                    ret.Add(new Move(x, y, x - 2, y));
                            }
                        }
                        if(y!=0 && b.board[x-1,y-1]!=null )//manger1
                            if(b.board[x-1,y-1]._Team==team.black)
                                ret.Add(new Move(x, y, x-1, y-1));
                        if(y!=7 && b.board[x-1,y+1]!=null)//manger2
                            if(b.board[x-1,y+1]._Team==team.black)
                                ret.Add(new Move(x, y, x-1, y+1));
                    }
                    break;
                case cast.bishop://=====================================================================================
                    tempX = x + 1; tempY = y + 1;
                    while (tempX < 8 && tempY < 8 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX++; tempY++;
                    }
                    if (tempX < 8 && tempY < 8 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x + 1; tempY = y - 1;
                    while (tempX < 8 && tempY >= 0 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX++; tempY--;
                    }
                    if (tempX < 8 && tempY >= 0 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x - 1; tempY = y + 1;
                    while (tempX >=0 && tempY < 8 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX--; tempY++;
                    }
                    if (tempX >=0 && tempY < 8 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x - 1; tempY = y - 1;
                    while (tempX >=0 && tempY >= 0 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX--; tempY--;
                    }
                    if (tempX >= 0 && tempY >= 0 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    break;
                case cast.knight://=====================================================================================
                    if (x + 1 < 8 && y + 2 < 8)    //x+1 y+2
                        if(b.board[x + 1, y + 2] == null || b.board[x + 1, y + 2]._Team != p._Team)
                            ret.Add(new Move(x, y, x + 1, y + 2));
                    
                    if (x + 2 < 8 && y + 1 < 8)    //x+2 y+1
                        if(b.board[x + 2, y + 1] == null || b.board[x + 2, y + 1]._Team != p._Team)
                            ret.Add(new Move(x, y, x + 2, y + 1));
                    
                    if (x + 2 < 8 && y - 1 >= 0)    //x+2 y-1
                        if(b.board[x + 2, y - 1] == null || b.board[x + 2, y - 1]._Team != p._Team)
                            ret.Add(new Move(x, y, x + 2, y - 1));
                    
                    if (x + 1 < 8 && y - 2 >= 0)    //x+1 y-2
                        if(b.board[x + 1, y - 2] == null || b.board[x + 1, y - 2]._Team != p._Team)
                            ret.Add(new Move(x, y, x + 1, y - 2));
                    
                    if (x - 1 >= 0 && y - 2 >= 0)    //x-1 y-2
                        if(b.board[x - 1, y - 2] == null || b.board[x - 1, y - 2]._Team != p._Team)
                            ret.Add(new Move(x, y, x - 1, y - 2));
                    
                    if (x - 2 >= 0 && y - 1 >= 0)    //x-2 y-1
                        if(b.board[x - 2, y - 1] == null || b.board[x - 2, y - 1]._Team != p._Team)
                            ret.Add(new Move(x, y, x - 2, y - 1));
                    
                    if (x - 2 >= 0 && y + 1 < 8)    //x-2 y+1
                        if(b.board[x - 2, y + 1] == null || b.board[x - 2, y + 1]._Team != p._Team)
                            ret.Add(new Move(x, y, x - 2, y + 1));
                    
                    if (x - 1 >= 0 && y + 2 < 8)    //x-1 y+2
                        if(b.board[x - 1, y + 2] == null || b.board[x - 1, y + 2]._Team != p._Team)
                            ret.Add(new Move(x, y, x - 1, y + 2));
                    
                    break;
                case cast.rook://=======================================================================================
                    temp = x + 1;
                    while (temp < 8 && b.board[temp, y] == null)
                    {
                        ret.Add(new Move(x,y,temp,y));
                        temp++;
                    }
                    if(temp < 8 && b.board[temp,y]._Team!=p._Team)
                        ret.Add(new Move(x,y,temp,y));
                    
                    temp = y + 1;
                    while (temp < 8 && b.board[x, temp] == null)
                    {
                        ret.Add(new Move(x,y,x,temp));
                        temp++;
                    }
                    if(temp < 8 && b.board[x,temp]._Team!=p._Team)
                        ret.Add(new Move(x,y,x,temp));
                    
                    temp = x - 1;
                    while (temp >= 0 && b.board[temp, y] == null)
                    {
                        ret.Add(new Move(x,y,temp,y));
                        temp--;
                    }
                    if(temp >= 0 && b.board[temp,y]._Team!=p._Team)
                        ret.Add(new Move(x,y,temp,y));
                    
                    temp = y - 1;
                    while (temp >= 0 && b.board[x, temp] == null)
                    {
                        ret.Add(new Move(x,y,x,temp));
                        temp--;
                    }
                    if(temp >= 0 && b.board[x,temp]._Team!=p._Team)
                        ret.Add(new Move(x,y,x,temp));
                    
                    break;
                case cast.queen://======================================================================================
                    temp = x + 1;
                    while (temp < 8 && b.board[temp, y] == null)
                    {
                        ret.Add(new Move(x,y,temp,y));
                        temp++;
                    }
                    if(temp < 8 && b.board[temp,y]._Team!=p._Team)
                        ret.Add(new Move(x,y,temp,y));
                    
                    temp = y + 1;
                    while (temp < 8 && b.board[x, temp] == null)
                    {
                        ret.Add(new Move(x,y,x,temp));
                        temp++;
                    }
                    if(temp < 8 && b.board[x,temp]._Team!=p._Team)
                        ret.Add(new Move(x,y,x,temp));
                    
                    temp = x - 1;
                    while (temp >= 0 && b.board[temp, y] == null)
                    {
                        ret.Add(new Move(x,y,temp,y));
                        temp--;
                    }
                    if(temp >= 0 && b.board[temp,y]._Team!=p._Team)
                        ret.Add(new Move(x,y,temp,y));
                    
                    temp = y - 1;
                    while (temp >= 0 && b.board[x, temp] == null)
                    {
                        ret.Add(new Move(x,y,x,temp));
                        temp--;
                    }
                    if(temp >= 0 && b.board[x,temp]._Team!=p._Team)
                        ret.Add(new Move(x,y,x,temp));
                    tempX = x + 1; tempY = y + 1;
                    while (tempX < 8 && tempY < 8 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX++; tempY++;
                    }
                    if (tempX < 8 && tempY < 8 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x + 1; tempY = y - 1;
                    while (tempX < 8 && tempY >= 0 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX++; tempY--;
                    }
                    if (tempX < 8 && tempY >= 0 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x - 1; tempY = y + 1;
                    while (tempX >=0 && tempY < 8 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX--; tempY++;
                    }
                    if (tempX >=0 && tempY < 8 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    
                    tempX = x - 1; tempY = y - 1;
                    while (tempX >=0 && tempY >= 0 && b.board[tempX,tempY]==null)
                    {
                        ret.Add(new Move(x,y,tempX,tempY));
                        tempX--; tempY--;
                    }
                    if (tempX >= 0 && tempY >= 0 && b.board[tempX, tempY]._Team != p._Team)
                        ret.Add(new Move(x, y, tempX, tempY));
                    break;
                case cast.king://=======================================================================================
                    if(x+1<8 && y+1<8)
                        if(b.board[x+1,y+1]==null || b.board[x+1,y+1]._Team != p._Team)
                            ret.Add(new Move(x,y,x+1,y+1));
                    if(x+1<8)
                        if(b.board[x+1,y]==null || b.board[x+1,y]._Team != p._Team)
                            ret.Add(new Move(x,y,x+1,y));
                    if(x+1<8 && y-1>=0)
                        if(b.board[x+1,y-1]==null || b.board[x+1,y-1]._Team != p._Team)
                            ret.Add(new Move(x,y,x+1,y-1));
                    if(y-1>=0)
                        if(b.board[x,y-1]==null || b.board[x,y-1]._Team != p._Team)
                            ret.Add(new Move(x,y,x,y-1));
                    if(x-1>=0 && y-1>=0)
                        if(b.board[x-1,y-1]==null || b.board[x-1,y-1]._Team != p._Team)
                            ret.Add(new Move(x,y,x-1,y-1));
                    if(x-1>=0)
                        if(b.board[x-1,y]==null || b.board[x-1,y]._Team != p._Team)
                            ret.Add(new Move(x,y,x-1,y));
                    if(x-1>=0 && y+1<8)
                        if(b.board[x-1,y+1]==null || b.board[x-1,y+1]._Team != p._Team)
                            ret.Add(new Move(x,y,x-1,y+1));
                    if(y+1<8)
                        if(b.board[x,y+1]==null || b.board[x,y+1]._Team != p._Team)
                            ret.Add(new Move(x,y,x,y+1));
                    break;
            }
        return ret;
        }
        
        static public List<Move> AllPossibleMove(Board b, team team)
        {
            List<Move> ret= new List<Move>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (b.board[i, j] != null && b.board[i,j]._Team== team)
                        PossibleMove(b,i,j).ForEach(move => { ret.Add(move); });
            return ret;
        }

        static public bool IsCheck(Board b, team team)
        {
            foreach (var move in AllPossibleMove(b,team))
            {
                if (b.board[move.x2,move.y2]!= null && b.board[move.x2,move.y2]._cast==cast.king)
                {
                    return true;
                }
            }

            return false;
        }

        public static void RemoveIllegalMove(Board b, List<Move> list, team team)
        {
            if (IsCheck(b, team))
            {
                foreach (var move in list)
                {
                    (Piece p, bool hasMove) = MakeMove(b, move);
                    if (IsCheck(b, team))
                    {
                        UnMakeMove(b,move,hasMove,p);
                        list.Remove(move);
                    }
                    else
                    {
                        UnMakeMove(b,move,hasMove,p);
                    }
                }
            }
        }

        

        static public (Piece,bool) MakeMove(Board b, Move m)//return the previous hasMoved and the eaten piece
        {
            Piece ret = b.board[m.x2, m.y2];
            bool ret1 = b.board[m.x1, m.y1]._hasMoved;
            b.board[m.x2, m.y2] = b.board[m.x1, m.y1];
            if (b.board[m.x2, m.y2]._cast == cast.king || b.board[m.x2, m.y2]._cast == cast.rook)
                b.board[m.x2, m.y2]._hasMoved = true;
            b.board[m.x1, m.y1] = null;
            return (ret,ret1);
        }

        static public void UnMakeMove(Board b, Move m, bool hasMoved, Piece eaten)
        {
            b.board[m.x1, m.y1] = b.board[m.x2, m.y2];
            b.board[m.x2, m.y2] = eaten;
            b.board[m.x1, m.y1]._hasMoved = hasMoved;
        }
        
        static public int Bench(Board b, int depth, bool print, team team)
        {
            if (depth == 0)
                return 1;
            int count = 0;
            List <Move> list = AllPossibleMove(b, team);
            RemoveIllegalMove(b,list,team);
            foreach (var move in list)
            {
                bool hasMoved;
                Piece p;
                (p,hasMoved) = MakeMove(b, move);
                if (print)
                {
                    b.Print(true);
                    Thread.Sleep(500);
                }
                    
                
                count += Bench(b, depth - 1, print,team==team.white?team.black:team.white);
                
                UnMakeMove(b,move,hasMoved,p);
            }
            return count;
        }
    }

    public class Move
    {
        public int x1, y1, x2, y2;

        public Move(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public override string ToString() { return "("+x1+", "+y1+") -> ("+x2+", "+y2+")"; }
    }
    
    public class Board
    {
        public Piece[,] board = new Piece[8,8];

        public Board()
        {
            board[0,0]=new Piece(Piece.cast.rook, Piece.team.black);
            board[7,0]=new Piece(Piece.cast.rook, Piece.team.white);
            board[0,1]=new Piece(Piece.cast.knight, Piece.team.black);
            board[7,1]=new Piece(Piece.cast.knight, Piece.team.white);
            board[0,2]=new Piece(Piece.cast.bishop, Piece.team.black);
            board[7,2]=new Piece(Piece.cast.bishop, Piece.team.white);
            board[0,3]=new Piece(Piece.cast.queen, Piece.team.black);
            board[7,3]=new Piece(Piece.cast.queen, Piece.team.white);
            board[0,4]=new Piece(Piece.cast.king, Piece.team.black);
            board[7,4]=new Piece(Piece.cast.king, Piece.team.white);
            board[0,5]=new Piece(Piece.cast.bishop, Piece.team.black);
            board[7,5]=new Piece(Piece.cast.bishop, Piece.team.white);
            board[0,6]=new Piece(Piece.cast.knight, Piece.team.black);
            board[7,6]=new Piece(Piece.cast.knight, Piece.team.white);
            board[0,7]=new Piece(Piece.cast.rook, Piece.team.black);
            board[7,7]=new Piece(Piece.cast.rook, Piece.team.white);
            for (int i = 0; i < 8; i++)
            {
                board[1,i]=new Piece(Piece.cast.pawn, Piece.team.black);
                board[6,i]=new Piece(Piece.cast.pawn, Piece.team.white);
            }
        }

        public void Print(bool color)
        {
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(color)
                        Console.BackgroundColor = i % 2 == 0 ^ j % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                    if (board[i,j] != null)
                    {
                        
                        Console.ForegroundColor = board[i, j]._Team == Piece.team.white
                            ? ConsoleColor.Green
                            : ConsoleColor.Red;
                        
                        Console.Write(board[i,j].ToString());
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = default;
                Console.WriteLine(" "+i);
            }
            Console.WriteLine("12345678\n");
        }
    }
}