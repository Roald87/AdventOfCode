from collections import namedtuple
from enum import Enum

import numpy as np


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        inp = [i.strip() for i in f.readlines()]
        # inp = []
        # for line in f.readlines():
        #     inp.append[(line.strip()])
        return inp


class ApproachedFrom(Enum):
    UP = 1
    DOWN = -1
    LEFT = 2
    RIGHT = -2


Point = namedtuple("Point", ["x", "y"])


def next_coordinate(current_coordinate: Point, approach: ApproachedFrom, direction):
    if direction == "|":
        new_point = Point(
            x=current_coordinate.x, y=current_coordinate.y + 1 * approach.value
        )
        new_approach = approach
    elif direction == "-":
        new_point = Point(
            x=current_coordinate.x + 1 * approach.value / 2, y=current_coordinate.y
        )
        new_approach = approach
    elif direction == "L" and approach == ApproachedFrom.UP:
        new_point = Point(x=current_coordinate.x + 1, y=current_coordinate.y)
        new_approach = ApproachedFrom.LEFT
    elif direction == "L" and approach == ApproachedFrom.RIGHT:
        new_point = Point(x=current_coordinate.x, y=current_coordinate.y - 1)
        new_approach = ApproachedFrom.DOWN
    elif direction == "J" and approach == ApproachedFrom.LEFT:
        new_point = Point(x=current_coordinate.x, y=current_coordinate.y - 1)
        new_approach = ApproachedFrom.DOWN
    elif direction == "J" and approach == ApproachedFrom.UP:
        new_point = Point(x=current_coordinate.x - 1, y=current_coordinate.y)
        new_approach = ApproachedFrom.RIGHT
    elif direction == "F" and approach == ApproachedFrom.RIGHT:
        new_point = Point(x=current_coordinate.x, y=current_coordinate.y + 1)
        new_approach = ApproachedFrom.UP
    elif direction == "F" and approach == ApproachedFrom.DOWN:
        new_point = Point(x=current_coordinate.x + 1, y=current_coordinate.y)
        new_approach = ApproachedFrom.LEFT
    elif direction == "7" and approach == ApproachedFrom.LEFT:
        new_point = Point(x=current_coordinate.x, y=current_coordinate.y + 1)
        new_approach = ApproachedFrom.UP
    elif direction == "7" and approach == ApproachedFrom.DOWN:
        new_point = Point(x=current_coordinate.x - 1, y=current_coordinate.y)
        new_approach = ApproachedFrom.RIGHT
    else:
        raise ValueError(f"Unknown {current_coordinate} {approach} {direction}.")

    return new_approach, new_point


def part1(inp):
    starting_point = [(i, line.index("S")) for i, line in enumerate(inp) if "S" in line]
    numbered_input = np.zeros((len(inp), len(inp[0])), dtype=int)
    sp = Point(x=starting_point[0][1], y=starting_point[0][0])
    i = 0

    next_points = []
    approach = []
    if inp[sp.y][sp.x + 1] in "J7-" and sp.x + 1 in range(0, len(inp[0])):
        next_points.append(Point(y=sp.y, x=sp.x + 1))
        approach.append(ApproachedFrom.LEFT)
    if inp[sp.y][sp.x - 1] in "F-L" and sp.x - 1 in range(0, len(inp[0])):
        next_points.append(Point(y=sp.y, x=sp.x - 1))
        approach.append(ApproachedFrom.RIGHT)
    if inp[sp.y + 1][sp.x] in "J|L" and sp.y + 1 in range(0, len(inp)):
        next_points.append(Point(y=sp.y + 1, x=sp.x))
        approach.append(ApproachedFrom.UP)
    if inp[sp.y - 1][sp.x] in "|F7" and sp.y - 1 in range(0, len(inp)):
        next_points.append(Point(y=sp.y - 1, x=sp.x))
        approach.append(ApproachedFrom.DOWN)

    assert (
        len(next_points) == 2
    ), f"Starting point has {len(next_points)} next points: {next_points}. Maximum = 2!"

    while next_points[0] != next_points[1]:
        i += 1
        for j, point in enumerate(next_points):
            numbered_input[int(point.y)][int(point.x)] = i
            next = next_coordinate(point, approach[j], inp[int(point.y)][int(point.x)])

            approach[j] = next[0]
            next_points[j] = next[1]

    numbered_input[int(next_points[0].y)][int(next_points[0].x)] = i + 1

    print(numbered_input)
    return np.max(numbered_input)


# def part2(inp):
#     numbered_input = np.zeros((len(inp), len(inp[0])), dtype=int)
#     for row in inp:
#         for col in row:
#             if
#     return 0


if __name__ == "__main__":
    day = "day10"
    inp = read_input(f"{day}.txt")
    # print(inp)
    test_inp = read_input(f"{day}_test.txt")
    test_inp2 = read_input(f"{day}_test2.txt")
    # print(test_inp)

    # print(f"test input a: {part1(test_inp)}")
    # print(f"test input a: {part1(test_inp2)}")

    print(f"Part a: {part1(inp)}")

    # print(f"test input b: {part2(test_inp)}")
    # print(f"Part b: {part2(inp)}")
