# https://stackoverflow.com/a/36961645/6329629

from math import ceil, sqrt
from itertools import cycle, count
from pprint import pprint
from itertools import product


def spiral_distances():
    """
    Yields 1, 1, 2, 2, 3, 3, ...
    """
    for distance in count(1):
        for _ in (0, 1):
            yield distance


def clockwise_directions():
    """
    Yields right, down, left, up, right, down, left, up, right, ...
    """
    left = (-1, 0)
    right = (1, 0)
    up = (0, -1)
    down = (0, 1)
    return cycle((right, down, left, up))


def spiral_movements():
    """
    Yields each individual movement to make a spiral:
    right, down, left, left, up, up, right, right, right, down, down, down, ...
    """
    for distance, direction in zip(spiral_distances(), clockwise_directions()):
        for _ in range(distance):
            yield direction


def square(width):
    """
    Returns a width x width 2D list filled with Nones
    """
    return [[0] * width for _ in range(width)]


dim = 9
array = square(dim)

x, y = dim // 2, dim // 2
array[x][y] = 1

# The surrounding array elements to check
# Remove the array position itself (0, 0) coordinate
chk_directions = [
    (i, j)
    for i, j in itertools.product([-1, 0, 1], repeat=2)
    if not (i == 0 and j == 0)
]

for count, move in enumerate(spiral_movements()):
    x += move[0]
    y += move[1]
    if count > dim**2:
        break
    for i, j in chk_directions:
        try:
            array[x][y] += array[x + i][y + j]
        except (IndexError, TypeError):
            pass

    if array[x][y] > 312051:
        print(array[x][y])
        break

pprint(array)
