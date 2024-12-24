def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        inp = [i.strip() for i in f.readlines()]

        return inp


inp = read_input("day3.txt")
# print(inp)
test_inp = read_input("day03_test.txt")
# print(test_inp)


def part1(inp):
    numbers = []
    for i, line in enumerate(inp):
        n = ""
        start_number = 1e6
        for j, char in enumerate(line):
            if char in "1234567890":
                n += char
                if start_number == 1e6:
                    start_number = j
            elif len(n) > 0:
                numbers.append((n, i, start_number))
                n = ""
                start_number = 1e6
            else:
                n = ""
                start_number = 1e6
        if len(n) > 0:
            numbers.append((n, i, start_number))

    symbols = []
    for i, line in enumerate(inp):
        for j, char in enumerate(line):
            if char not in ".1234567890":
                symbols.append((char, i, j))

    valid_numbers = []
    for number in numbers:
        for symbol in symbols:
            number_start_idx = number[2]
            number_end_idx = number_start_idx + len(number[0])
            test_range = list(range(number_start_idx - 1, number_end_idx + 1))
            if abs(symbol[1] - number[1]) <= 1 and symbol[2] in range(
                number_start_idx - 1, number_end_idx + 1
            ):
                valid_numbers.append(int(number[0]))
                break

    return sum(valid_numbers)
    # return sum(ans)


# print(f"test input a: {part1(test_inp)}")
# print(f"Part a: {part1(inp)}")  # 185291 wrong, 297967, 528232, 521242, 519922


def part2(inp):
    numbers = []
    for i, line in enumerate(inp):
        n = ""
        start_number = 1e6
        for j, char in enumerate(line):
            if char in "1234567890":
                n += char
                if start_number == 1e6:
                    start_number = j
            elif len(n) > 0:
                numbers.append((n, i, start_number))
                n = ""
                start_number = 1e6
            else:
                n = ""
                start_number = 1e6
        if len(n) > 0:
            numbers.append((n, i, start_number))

    gears = []
    for i, line in enumerate(inp):
        for j, char in enumerate(line):
            if char == "*":
                gears.append((char, i, j))

    gear_ratio = []
    for gear in gears:
        possible_numbers = [
            number
            for number in numbers
            if abs(gear[1] - number[1]) <= 1
            and gear[2] in range(number[2] - 1, number[2] + len(number[0]) + 1)
        ]
        if len(possible_numbers) == 2:
            gear_ratio.append(int(possible_numbers[0][0]) * int(possible_numbers[1][0]))

    return sum(gear_ratio)


print(f"test input b: {part2(test_inp)}")
print(f"Part b: {part2(inp)}")
