CC=gcc
CFLAGS=-I.
DEPS = hellomake.h

%.o: %.c $(DEPS)
	$(CC) -c -o $@ $< $(CFLAGS)

main: main.o board.o move.o eval.o
	$(CC) -o main main.o board.o move.o eval.o
