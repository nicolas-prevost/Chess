#include <stdio.h>
#include <stdlib.h>
#include "board.h"

int main()
{
    unsigned char** board=init_board();
    init_board(board);
    print(board);
}
