with open("day01.txt") as f:
    # inp = [int(i) for i in f.readline().strip().split(",")]
    # inp = [i for i in f.readline().strip().split(",")]
    # inp = [int(i.strip()) for i in f.readlines()]
    inp = [i.strip() for i in f.readlines()]


def day1a(inp):
    ans = []
    for i in inp:
        n = ""
        for char in i:
            if ord(char) > ord("0") and ord(char) <= ord("9"):
                n += char
        ans.append(int(n[0] + n[-1]))

    return sum(ans)


print(f"Part a: {day1a(inp)}")


letter_nums = "one two three four five six seven eight nine".split(" ")
num_nums = "1 2 3 4 5 6 7 8 9".split(" ")


def day1b(inp):
    ans = []
    for i in inp:
        n = []
        for j, letter_num in enumerate(letter_nums, 1):
            if letter_num in i:
                idx = i.index(letter_num)
                n.append((idx, j))

        for j, letter_num in enumerate(letter_nums, 1):
            if letter_num in i:
                idx = i.rfind(letter_num)
                n.append((idx, j))

        for j, num_num in enumerate(num_nums, 1):
            if num_num in i:
                idx = i.index(num_num)
                n.append((idx, j))

        for j, num_num in enumerate(num_nums, 1):
            if num_num in i:
                idx = i.rfind(num_num)
                n.append((idx, j))

        nums = "".join(str(k[1]) for k in sorted(n, key=lambda x: x[0]))
        ans.append(int(nums[0] + nums[-1]))

    return sum(ans)


print(f"Part b: {day1b(inp)}")  # wrong 29221, wrong 53168, wriong 55656, wrong 55639
