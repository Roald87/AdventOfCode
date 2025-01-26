import itertools

import numpy as np


def read_input(fname):
    with open(fname) as f:
        lines = f.readlines()

    return [
        [(n[0], int(n[1:])) for n in lines[0].split(",")],
        [(n[0], int(n[1:])) for n in lines[1].split(",")],
    ]


wires = read_input("day03.txt")


def wiring(wire):
    wire1 = [(1, 1)]
    for x, y in wire:
        if x == "R":
            wire1.append((wire1[-1][0] + y, wire1[-1][1]))
        elif x == "L":
            wire1.append((wire1[-1][0] - y, wire1[-1][1]))
        elif x == "U":
            wire1.append((wire1[-1][0], wire1[-1][1] + y))
        elif x == "D":
            wire1.append((wire1[-1][0], wire1[-1][1] - y))

    return wire1


wire1 = wiring(wires[0])
wire2 = wiring(wires[1])


# plt.plot([x[0] for x in wire1], [x[1] for x in wire1])
# plt.plot([x[0] for x in wire2], [x[1] for x in wire2])
# plt.scatter(1, 1, c="r")
# plt.show() # answer 225


def intersections(wire1, wire2):
    cross = []
    for start, end in itertools.pairwise(wire1):
        for start2, end2 in itertools.pairwise(wire2):
            if start[0] == end[0] and start2[1] == end2[1]:  # vert line and horz line
                if start[0] in range(
                    min(start2[0], end2[0]), max(start2[0], end2[0])
                ) and start2[1] in range(min(start[1], end[1]), max(start[1], end[1])):
                    cross.append((start[0], start2[1]))
            if start[1] == end[1] and start2[0] == end2[0]:  # horz line and vert line
                if start[1] in range(
                    min(start2[1], end2[1]), max(start2[1], end2[1])
                ) and start2[0] in range(min(start[0], end[0]), max(start[0], end[0])):
                    cross.append((start2[0], start[1]))

    return cross


crossings = [(x, y) for x, y in intersections(wire1, wire2)]

# # check to see if I have all crossings
# plt.plot([x[0] for x in wire1], [x[1] for x in wire1])
# plt.plot([x[0] for x in wire2], [x[1] for x in wire2])
# x = [c[0] for c in crossings]
# y = [c[1] for c in crossings]
# plt.scatter(x, y, c='black')
# plt.scatter(1, 1, c="r")
# plt.show()


# distance to intersection from wire start
def wire_distance(wire, crossings):
    distances = []
    for cross in crossings:
        distance1 = 0
        prev_x = wire[0][0]
        prev_y = wire[0][1]
        for x, y in wire:
            if (
                x == cross[0]
                and prev_x == cross[0]
                and cross[1] in range(min(prev_y, y), max(prev_y, y))
            ):
                distance1 += abs(prev_y - cross[1])
                break
            elif (
                y == cross[1]
                and prev_y == cross[1]
                and cross[0] in range(min(prev_x, x), max(prev_x, x))
            ):
                distance1 += abs(prev_x - cross[0])
                break
            else:
                distance1 += abs(x - prev_x) + abs(y - prev_y)
            prev_x = x
            prev_y = y
        distances.append(distance1)

    return distances


dis1 = np.array(wire_distance(wire1, crossings))
dis2 = np.array(wire_distance(wire2, crossings))
print(dis1, dis2)
print(np.min(dis1 + dis2))
