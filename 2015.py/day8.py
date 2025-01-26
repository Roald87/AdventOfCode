def read_input(fname):
    with open(fname, "rb") as f:
        inp = f.read()
        return inp


inp = read_input("day8.txt")
print(inp)
test_inp = read_input("day8_test.txt")
print(test_inp)


def part1(inp):
    size_diff = []
    for line in inp:
        str_code_length = len(line)
        enc_dec = line[1:-1].encode().decode()
        memory_length = len(line[1:-1].encode().decode())
        size_diff.append(str_code_length - memory_length)

    return size_diff


# print(part1(inp))
print(part1(test_inp))


def part2(inp):
    arr = []

    return arr


print(part2(inp))
