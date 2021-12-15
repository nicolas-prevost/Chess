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

struct list* possible_move(unsigned char** board, int x, int y)
{
    struct list* ret=list_init();
    switch (get_class(board[x*8+y])) {
        case 1://pawn
            append(ret, new_move(x,y,x+1,y));
        default:
            break;
    }
    return ret;
}
