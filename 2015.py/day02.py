import itertools


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        inp = [tuple(int(n) for n in i.strip().split("x")) for i in f.readlines()]
        # inp = [i.strip() for i in f.readlines()]

        return inp


inp = read_input("day2.txt")
print(inp)


def day2a(inp):
    areas = []
    for l, w, h in inp:
        lw = l * w
        wh = w * h
        hl = h * l
        areas.append(2 * (lw + wh + hl) + min([lw, wh, hl]))

    return areas


print(sum(day2a(inp)))
print(day2a([(1, 1, 10)]))


def day2b(inp):
    ribbon_length = []
    for l, w, h in inp:
        present = min([2 * (l + w), 2 * (w + h), 2 * (l + h)])
        bow = l * w * h
        ribbon_length.append(present + bow)

    return ribbon_length


print(sum(day2b(inp)))  # too high 3753470
print(day2b([(2, 3, 4)]))
