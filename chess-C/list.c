#include <stdio.h>
#include <stdlib.h>
#include "move.h"
#include "list.h"

struct list
{
    struct move* move;
    struct list* next;
    int len;
};

struct list* list_init()
{
    struct list* l = malloc(sizeof(struct list));
    l->next=NULL;
    l->move=NULL;
    l->len=0;
    return l;
}

void append(struct list* list, struct move* move)
{
    struct list* add=malloc(sizeof(struct list));
    add->next = NULL;
    add->move = move;
    list->len++;

    while(list->next)
    {
        list = list->next;
    }
    list->next = add;
}


void print_list(struct list* list)
{
    printf("\nlen: %d\n", list->len);
    if(!list->len)
    {
        printf("no move\n");
        return;
    }
    list=list->next;
    int x=0;
    while(list)
    {
        printf("%d,%d -> ",list->move->x1,list->move->y1);
        printf("%d,%d",list->move->x2,list->move->y2);
        list=list->next;
        printf(x==4?"\n":"  |  ");
        x=x==4?0:x+1;
    }
    printf("\n");
}
