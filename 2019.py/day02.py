from more_itertools import chunked


def read_input(fname):
    with open(fname) as f:
        lines = f.readlines()

    return [int(n) for n in lines[0].split(",")]


def day1a(instructions):
    for instruction in chunked(instructions, 4):
        if instruction[0] == 99:
            break

        opcode, read_pos1, read_pos2, save_pos = instruction
        if opcode == 1:
            instructions[save_pos] = instructions[read_pos1] + instructions[read_pos2]
        elif opcode == 2:
            instructions[save_pos] = instructions[read_pos1] * instructions[read_pos2]


if __name__ == "__main__":
    instructions = read_input("day02.txt")
    updated_instructions = instructions[:]
    updated_instructions[1] = 12
    updated_instructions[2] = 2
    print(day1a(updated_instructions))
    print("day1a: ", updated_instructions[0])

    for noun in range(100):
        for verb in range(100):
            updated_instructions = instructions[:]
            updated_instructions[1] = noun
            updated_instructions[2] = verb

            day1a(updated_instructions)
            if updated_instructions[0] == 19690720:
                print("day1b: ", 100 * noun + verb)
                break
