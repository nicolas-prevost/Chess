#ifndef MOVE_H
#define MOVE_H

void append(struct list* list, struct move move);

void print_list(struct list* list);

struct list* possible_move(char** board, int x, int y);

#endif
