#include "board.h"

int get_value(char* piece)
{
        switch (get_class(piece))
        {
            case 1:
                return 1;
            case 2:
                return 3;
            case 3:
                return 3;
            case 4:
                return 5;
            case 5:
                return 8;
            case 6:
                return 1000;
            default:
                return 0;
        }
}

int evaluate(unsigned char** board)
{
    int count=0;

    for(int i=0; i<64;i++)
    {
        if(*board[i])
        {
            count += get_value(board[i]) * (get_team(board[i])?-1:1);
        }
    }
    return count;
}
