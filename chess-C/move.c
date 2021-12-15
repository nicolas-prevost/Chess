#include <stdio.h>
#include <stdlib.h>
#include "board.h"

struct move
{
    char x1,y1,x2,y2;
};

struct list
{
    struct move move;
    struct list* next;
};

struct move new_move(int x1, int y1, int x2, int y2)
{
    struct move ret;
    ret.x1=x1;ret.y1=y1;ret.x2=x2,ret.y2=y2;
    return ret;
}

void append(struct list* list, struct move move)
{
    struct list* add=malloc(sizeof(struct list));
    add->next = NULL;
    add->move = move;
    if(!list)
    {
        *list = *add;
        return;
    }
    while(list->next)
    {
        list = list->next;
    }
    list->next = add;
}

void print_list(struct list* list)
{
    while (list)
    {
        printf("%d,%d -> ",list->move.x1,list->move.y1);
        printf("%d,%d",list->move.x2,list->move.y2);
        list=list->next;
    }
    printf("\n");
}

struct list* possible_move(char** board, int x, int y)
{
    struct list* ret;
    switch (get_class(board[x*8+y])) {
        case 1://pawn
            append(ret, new_move(x,y,x+1,y));
        default:
            break;
    }
    return ret;
}
