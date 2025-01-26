import hashlib


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = []
        inp = f.readline().strip()

        return inp


inp = read_input("day4.txt")
print(inp)


def part1(inp):
    for i in range(10000000):
        hash = hashlib.md5((inp + str(i)).encode())
        if hash.hexdigest()[:5] == "00000":
            break

    return i


# print(part1(inp))
# print(part1([(1, 1, 10)]))


def part2(inp):
    for i in range(100000000):
        hash = hashlib.md5((inp + str(i)).encode())
        if hash.hexdigest()[:6] == "000000":
            break

    return i


print(part2(inp))
