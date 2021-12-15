#include <stdio.h>
#include <stdlib.h>
#include "board.h"
#include "eval.h"


unsigned char* new_piece(unsigned char cast, int team)
{
    unsigned char* ret=malloc(sizeof(unsigned char));
    *ret=cast;
    *ret += team?128:0;
    return ret;
}

unsigned char** init_board()
{
    unsigned char** ret=calloc(64, sizeof(unsigned char*));
    for (size_t i = 0; i < 64; i++) {
        unsigned char* c = malloc(sizeof(unsigned char));
        *c=0;
        ret[i]= c;
    }
    ret[0]=new_piece(4,1);
    ret[1]=new_piece(3,1);
    ret[2]=new_piece(2,1);
    ret[3]=new_piece(5,1);
    ret[4]=new_piece(6,1);
    ret[5]=new_piece(3,1);
    ret[6]=new_piece(2,1);
    ret[7]=new_piece(4,1);
    ret[0+56]=new_piece(4,0);
    ret[1+56]=new_piece(3,0);
    ret[2+56]=new_piece(2,0);
    ret[3+56]=new_piece(5,0);
    ret[4+56]=new_piece(6,0);
    ret[5+56]=new_piece(3,0);
    ret[6+56]=new_piece(2,0);
    ret[7+56]=new_piece(4,0);
    for (size_t i = 0; i < 8; i++) {
        ret[i+8]=new_piece(1,1);
        ret[i+48]=new_piece(1,0);
    }
    ret[18]=new_piece(4,1);

    return ret;
}

unsigned char get_team(unsigned char* c){return *c&128;}

unsigned char get_class(char* piece){return *piece&(1+2+4);}

char get_char(char* piece)
{
    switch (get_class(piece))
    {
        case 1:
            return 'p';
        case 2:
            return 'k';
        case 3:
            return 'b';
        case 4:
            return 'r';
        case 5:
            return 'Q';
        case 6:
            return 'K';
    }
}

void white () {printf("\033[0;37m");}
void black () {printf("\033[0;30m");}
void red () {printf("\033[0;31m");}
void green () {printf("\033[0;32m");}
void reset () {printf("\033[0m");}

void print(unsigned char** board)
{
    system("clear");
    white();
    for (char i = 0; i < 8; i++)
    {
        for (char j = 0; j < 8; j++)
        {
            if(*board[i*8+j])
            {
                if(get_team(board[i*8+j]))
                {
                    red();
                }
                else
                {
                    white();
                }
                printf("%c",get_char(board[i*8+j]));
            }
            else
            {
                printf(" ");
            }
        }
        printf("\n");
    }
    reset();
    printf("eval: %d\n", evaluate(board));
}
