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
        default:
            break;
    }
}
