#include <stdio.h>
#include <stdlib.h>
#include "board.h"
#include "list.h"
#include "move.h"

struct move;

struct move* new_move(int x1, int y1, int x2, int y2)
{
    struct move* ret= malloc(sizeof(struct move));
    ret->x1=x1;ret->y1=y1;ret->x2=x2,ret->y2=y2;
    return ret;
}

void possible_move(unsigned char** board, struct list* ret, int x, int y)
{
    //append(ret, new_move(x,y,x,y));
    unsigned char team = get_team(board[8*x+y]);
    switch (get_class(board[x*8+y])) {
        case 1://pawn
            if(get_team(board[x*8+y]))
            {//black
                if(*board[(x+1)*8+y]==0)
                {
                    append(ret, new_move(x,y,x+1,y));
                    if(x==1 && *board[(x+2)*8+y]==0)
                        append(ret, new_move(x,y,x+2,y));
                }
                if(y!=0)
                    if(*board[(x+1)*8+y-1]!=0)
                        if(!get_team(board[(x+1)*8+y-1]))
                            append(ret, new_move(x,y,x+1,y-1));
                if(y!=7)
                    if(*board[(x+1)*8+y+1]!=0)
                        if(!get_team(board[(x+1)*8+y+1]))
                            append(ret, new_move(x,y,x+1,y+1));

            }
            else
            {//white

            }
        case 2://knight
            if(x+1<8 && y+2<8)
                if(*board[8*(x+1)+y+2]==0 || get_team(board[8*(x+1)+y+2])!=team)
                    append(ret, new_move(x,y,x+1,y+2));
            if(x+2<8 && y+1<8)
                if(*board[8*(x+2)+y+1]==0 || get_team(board[8*(x+2)+y+1])!=team)
                    append(ret, new_move(x,y,x+2,y+1));
            if(x+2<8 && y-1>=0)
                if(*board[8*(x+2)+y-1]==0 || get_team(board[8*(x+2)+y-1])!=team)
                    append(ret, new_move(x,y,x+2,y-1));


        default:
            break;
    }
}

//return (has moved of p1, p2 if eaten)
void make_move(char** board, struct move* move, unsigned char* has_moved, unsigned char* p2)
{
    printf("test\n" );
    *p2 = *board[move->x2*8+move->y2];
    *has_moved = *board[move->x1*8+move->y1]&64;
    *board[move->x2*8+move->y2] = *board[move->x1*8+move->y1]|64;
    *board[move->x1*8+move->y1] = 0;
    return (has_moved, p2);
}
