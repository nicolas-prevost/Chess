CC=gcc
CFLAGS=-I.
DEPS = hellomake.h

%.o: %.c $(DEPS)
	$(CC) -c -o $@ $< $(CFLAGS)

main: main.o board.o move.o eval.o list.o
	$(CC) -o main main.o board.o move.o eval.o list.o

clean:
	@echo "cleaning ./main"
	@rm main
	@echo "cleaning every.o"
	@rm main.o
	@rm board.o
	@rm move.o
	@rm eval.o
