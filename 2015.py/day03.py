import itertools


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = []
        inp = [i.strip() for i in f.readline()]

        return inp


inp = read_input("day3.txt")
print(inp)


def part1(inp):
    houses = [(0, 0)]
    for direction in inp:
        x, y = houses[-1]
        if direction == "^":
            houses.append((x, y + 1))
        elif direction == "v":
            houses.append((x, y - 1))
        elif direction == ">":
            houses.append((x + 1, y))
        elif direction == "<":
            houses.append((x - 1, y))

    return houses


print(len(set((part1(inp)))))
print(part1([(1, 1, 10)]))


def part2(inp):
    santa = [(0, 0)]
    robot_santa = [(0, 0)]
    for i, direction in enumerate(inp):
        if i % 2 == 0:
            x, y = santa[-1]
        else:
            x, y = robot_santa[-1]

        if direction == "^":
            new_location = (x, y + 1)
        elif direction == "v":
            new_location = (x, y - 1)
        elif direction == ">":
            new_location = (x + 1, y)
        elif direction == "<":
            new_location = (x - 1, y)

        if i % 2 == 0:
            santa.append(new_location)
        else:
            robot_santa.append(new_location)

    houses = santa + robot_santa

    return houses


print(len(set(part2(inp))))
