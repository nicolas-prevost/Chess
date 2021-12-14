#ifndef BOARD_H
#define BOARD_H

unsigned char* new_piece(unsigned char cast, int team);

unsigned char** init_board();

unsigned char get_team(unsigned char* c);

void print(unsigned char** board);

#endif
