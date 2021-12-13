#include "board.h"
#include <stdio.h>

char* init_board()
{
    char* ret = malloc(64*sizeof(char));
    return ret;
}

void Print(char* board)
{
    for (char i = 0; i < 8; i++)
    {
        for (char j = 0; j < 8; j++)
        {
            printf("0");
        }
        printf("\n");
    }
}
