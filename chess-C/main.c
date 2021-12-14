#include <stdio.h>
#include <stdlib.h>
#include "board.h"
//1    2 4 8 16 32 64
//team          piece
//
//
//
//
//

int main()
{
    char* board=init_board();
    init_board(board);
    Print(board);
}
