import numpy as np


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = []
        # inp = f.readline().strip()
        inp = [i.strip().replace("turn ", "").split(" ") for i in f.readlines()]

        return inp


inp = read_input("day6.txt")
print(inp)


def part1(inp):
    arr = np.zeros(shape=(1000, 1000), dtype=bool)
    for action, coordinate_lu, _, coordinate_rd in inp:
        x1, y1 = [int(coordinate) for coordinate in coordinate_lu.split(",")]
        x2, y2 = [int(coordinate) for coordinate in coordinate_rd.split(",")]
        if action == "toggle":
            arr[x1 : x2 + 1, y1 : y2 + 1] = ~arr[x1 : x2 + 1, y1 : y2 + 1]
        elif action == "on":
            arr[x1 : x2 + 1, y1 : y2 + 1] = True
        elif action == "off":
            arr[x1 : x2 + 1, y1 : y2 + 1] = False

    return arr


print(part1(inp).sum())  # 542387 too low


def part2(inp):
    arr = np.zeros(shape=(1000, 1000), dtype=int)
    for action, coordinate_lu, _, coordinate_rd in inp:
        x1, y1 = [int(coordinate) for coordinate in coordinate_lu.split(",")]
        x2, y2 = [int(coordinate) for coordinate in coordinate_rd.split(",")]
        if action == "toggle":
            arr[x1 : x2 + 1, y1 : y2 + 1] += 2
        elif action == "on":
            arr[x1 : x2 + 1, y1 : y2 + 1] += 1
        elif action == "off":
            arr[x1 : x2 + 1, y1 : y2 + 1] += -1

        arr[arr < 0] = 0

    return arr


print(part2(inp).sum())
