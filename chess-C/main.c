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
    possible_move(board,l, 1,0);
    print_list(l);
    return 0;
}
