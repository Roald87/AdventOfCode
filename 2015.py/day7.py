import numpy as np


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = f.readline().strip()
        inp = sorted(
            [line.strip().split(" -> ") for line in f.readlines()],
            key=lambda x: (len(x[1]), x[1]),
        )

        return inp[1:] + [inp[0]]


inp = read_input("day7.txt")
print(inp)
test_inp = read_input("day7_test")


def part1(inp):
    wires = {"1": 1}
    for instr, out in inp:
        if "AND" in instr:
            wire1, wire2 = instr.split(" AND ")
            wires[out] = np.uint16(wires[wire1] & wires[wire2])
        elif "OR" in instr:
            wire1, wire2 = instr.split(" OR ")
            wires[out] = np.uint16(wires[wire1] | wires[wire2])
        elif "NOT" in instr:
            _, wire = instr.split(" ")
            wires[out] = np.uint16(~(wires[wire]))
        elif "LSHIFT" in instr:
            wire, shift = instr.split(" LSHIFT ")
            wires[out] = np.uint16(wires[wire] << int(shift))
        elif "RSHIFT" in instr:
            wire, shift = instr.split(" RSHIFT ")
            wires[out] = np.uint16(wires[wire] >> int(shift))
        elif instr.isnumeric():
            wires[out] = np.uint16(instr)
        else:
            wires[out] = wires[instr]

    return wires


print(part1(inp)["a"])
# print(part1(test_inp))  # doenst work with the current version


def part2(inp):
    wires = part1(inp)
    inp[0] = [str(wires["a"]), "b"]
    new_result = part1(inp)

    return new_result


print(part2(inp)["a"])
