import itertools
import string
import more_itertools


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = []
        # inp = f.readline().strip()
        inp = [i.strip() for i in f.readlines()]

        return inp


inp = read_input("day5.txt")
print(inp)


def part1(inp):
    nice_strings = []
    for line in inp:
        nice_string = (
            not ("ab" in line or "cd" in line or "pq" in line or "xy" in line)
            and sum([line.count(vowel) for vowel in "aeiuo"]) >= 3
            and sum([line.count(2 * letter) for letter in string.ascii_lowercase]) >= 1
        )
        if nice_string:
            nice_strings.append(line)

    return nice_strings


print(len(part1(inp)))  # 32 false
test_input = [
    "ugknbfddgicrmopn",
    "jchzalrnumimnmhp",
    "haegwjzuvuyypxyu",
    "dvszwmarrgswjxmb",
]
print(part1(test_input))


def part2(inp):
    nice_strings = []
    for line in inp:
        pairs = [
            part
            for part in itertools.pairwise(line)
            if len(line.replace("".join(part), "")) <= (len(line) - 4)
        ]
        repeats_in_3 = [
            part
            for part in more_itertools.sliding_window(line, 3)
            if part[0] == part[-1]
        ]
        if len(pairs) >= 1 and len(repeats_in_3) >= 1:
            nice_strings.append(line)

    return nice_strings


print(len(part2(inp)))
print(part2(["qjhvhtzxzqqjkmpb", "xxyxx", "uurcxstgmygtbstg", "ieodomkazucvgmuy"]))
