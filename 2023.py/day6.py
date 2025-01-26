import numpy as np

test_inp = [(7, 9), (15, 40), (30, 200)]
inp = [(40, 277), (82, 1338), (91, 1349), (66, 1063)]

print(inp)
print(test_inp)


def part1(inp):
    winning_times = []
    for time, distance in inp:
        times = [t for t in [t * (time - t) for t in range(time)] if t > distance]
        winning_times.append(len(times))

    return np.prod(winning_times)


print(f"test input a: {part1(test_inp)}")
print(f"Part a: {part1(inp)}")

# def part2(inp):
#     return 0

print(f"test input b: {part1([(71530, 940200)])}")
print(f"Part b: {part1([(40829166, 277133813491063)])}")
