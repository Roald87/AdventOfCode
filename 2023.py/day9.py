import itertools


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        # inp = [i.strip() for i in f.readlines()]

        inp = []
        for line in f.readlines():
            inp.append([int(l) for l in line.strip().split()])
        return inp


def part1(inp):
    history = []
    for line in inp:
        zero_diffs = [line]
        while not all(n == 0 for n in zero_diffs[-1]):
            zero_diffs.append([n[1] - n[0] for n in itertools.pairwise(zero_diffs[-1])])
        # print(zero_diffs)

        # extrapolate value
        add_to_next_line = 0
        for line in reversed(zero_diffs):
            add_to_next_line = line[-1] + add_to_next_line
        history.append(add_to_next_line)

    return sum(history)  # 1641934234


def part2(inp):
    history = []
    for line in inp:
        zero_diffs = [line]
        while not all(n == 0 for n in zero_diffs[-1]):
            zero_diffs.append([n[1] - n[0] for n in itertools.pairwise(zero_diffs[-1])])
        # print(zero_diffs)

        # extrapolate value
        add_to_next_line = 0
        for line in reversed(zero_diffs):
            add_to_next_line = line[0] - add_to_next_line
        history.append(add_to_next_line)

    return sum(history)  # 1641934234


if __name__ == "__main__":
    day = "day9"
    inp = read_input(f"{day}.txt")
    print(inp)
    test_inp = read_input(f"{day}_test.txt")
    print(test_inp)

    print(f"test input a: {part1(test_inp)}")
    print(f"Part a: {part1(inp)}")

    print(f"test input b: {part2(test_inp)}")
    print(f"Part b: {part2(inp)}")
