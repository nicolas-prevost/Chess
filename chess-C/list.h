#ifndef LIST_H
#define LIST_H

struct list;

struct list* list_init();
void append(struct list* list, struct move* move);
void print_list(struct list* list);

#endif
