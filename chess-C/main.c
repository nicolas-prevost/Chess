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
    l=possible_move(board, 6,6);
    print_list(l);
    return 0;
}
