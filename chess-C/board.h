#ifndef BOARD_H
#define BOARD_H

unsigned char* new_piece(unsigned char cast, int team);

unsigned char** init_board();

unsigned char get_team(unsigned char* c);
unsigned char get_class(char* piece);

void print(unsigned char** board);

int evaluate(unsigned char** board);

#endif
