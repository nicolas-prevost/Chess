#include "board.h"
#include <stdio.h>
#include <stdlib.h>

char* init_board()
{
    char* ret=calloc(64, sizeof(char));
    ret[9]=129;

    return ret;
}


void white () {printf("\033[0;37m");}
void black () {printf("\033[0;30m");}
void red () {printf("\033[0;31m");}
void green () {printf("\033[0;32m");}

char get_team(char c){return c>>7;}


void reset () {
  printf("\033[0m");
}

void Print(char* board)
{
    system("clear");
    printf("%d====", get_team(board[9]));
    white();
    for (char i = 0; i < 8; i++)
    {
        for (char j = 0; j < 8; j++)
        {
            if(board[i*8+j])
            {
                if(get_team(board[i*8+j]))
                {
                    green();
                }
                else
                {
                    red();
                }
                printf("0");
            }
            else
            {
                printf(" ");
            }
        }
        printf("\n");
    }
}
