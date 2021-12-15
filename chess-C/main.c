#include <stdio.h>
#include <stdlib.h>
#include "board.h"
#include "move.h"

int main()
{
    unsigned char** board=init_board();
    init_board(board);
    print(board);
    struct list* l;
    print_list(possible_move(board, 6,6));
}
