import math
from sympy.ntheory import factorint


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        # inp = [i.strip() for i in f.readlines()]
        rl_instrcutions = f.readline().strip()
        f.readline()  # discard

        nodes = []
        for line in f.readlines():
            nodes.append(
                (
                    line.strip().split(" = ")[0],
                    line.strip().split(" = ")[1][1:-1].split(", "),
                )
            )

        return rl_instrcutions, nodes


def part1(inp):
    _mapping = {_map[0]: _map[1] for _map in inp[1]}
    starting_point = "AAA"
    i = 0
    while starting_point != "ZZZ":
        instruction = inp[0][i % len(inp[0])]
        if instruction == "R":
            starting_point = _mapping[starting_point][1]
        elif instruction == "L":
            starting_point = _mapping[starting_point][0]

        if starting_point == "ZZZ":
            return i + 1

        i += 1


def part2(inp):
    starting_points = [start for (start, maps) in inp[1] if start[2] == "A"]
    _mapping = {_map[0]: _map[1] for _map in inp[1]}

    i = 0
    reached_z = dict()
    while True:
        for j, starting_point in enumerate(starting_points):
            instruction = inp[0][i % len(inp[0])]
            if instruction == "R":
                starting_points[j] = _mapping[starting_point][1]
            elif instruction == "L":
                starting_points[j] = _mapping[starting_point][0]

        i += 1

        for starting_point in starting_points:
            if starting_point[2] == "Z" and not starting_point in reached_z:
                reached_z[starting_point] = i

        if len(reached_z) == len(starting_points):
            least_common_multiple = math.lcm(*list(reached_z.values()))

            return least_common_multiple


if __name__ == "__main__":
    day = "day8"
    inp = read_input(f"{day}.txt")
    # print(inp)
    test_inp = read_input(f"{day}_test.txt")
    # print(test_inp)

    # print(f"test input a: {part1(test_inp)}")
    # print(f"Part a: {part1(inp)}")

    print(f"Part b: {part2(inp)}")  # 10818234074807
