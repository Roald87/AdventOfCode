import itertools


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        # inp = [i.strip() for i in f.readlines()]
        inp = f.readline().strip()

        return inp


inp = read_input("day1.txt")
print(inp)
# test_inp = read_input("day01_test.txt")


def day1a(inp):
    return [1 if i == "(" else -1 for i in inp]


print(sum(day1a(inp)))


def day1b(inp):
    return list(itertools.accumulate(day1a(inp))).index(-1) + 1


print(day1b(inp))
