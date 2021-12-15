#ifndef MOVE_H
#define MOVE_H

struct move
{
    char x1,y1,x2,y2;
};

struct move* new_move(int x1, int y1, int x2, int y2);

void possible_move(unsigned char** board, struct list* ret, int x, int y);

void make_move(char** board, struct move* move, unsigned char* has_moved, unsigned char* p2);

#endif
