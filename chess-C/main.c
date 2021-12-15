#include <stdio.h>
#include <stdlib.h>
#include "board.h"
#include "move.h"
#include "list.h"

int main()
{
    unsigned char** board=init_board();
    init_board(board);

    print(board);
    struct list* l = list_init();
    possible_move(board,l, 1,1);
    print_list(l);
    unsigned char* has_moved=malloc(sizeof(unsigned char));
    unsigned char *p2 = malloc(sizeof(unsigned char));
    *has_moved=0;
    *p2 = 0;
    printf("te5st\n" );
    make_move(board, new_move(1,1,0,0),has_moved,p2);

    print(board);
    printf("%u %u\n",*has_moved, *p2);
    return 0;
}
