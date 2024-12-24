import re


def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        inp = [tuple(i.strip().split(";")) for i in f.readlines()]
        # inp = [j.split(",") for i in inp for j in i]
        return inp


inp = read_input("day02.txt")
print(inp)


def day2a(inp):
    impossible_games = []
    for i, game in enumerate(inp, 1):
        for turn in game:
            for c in turn.split(","):
                res = re.search("(\d+) (\w+)", c)
                no = int(res.group(1))
                color = res.group(2)
                if (
                    (color == "red" and no > 12)
                    or (color == "green" and no > 13)
                    or (color == "blue" and no > 14)
                ):
                    impossible_games.append(i)

    return sum([i for i in range(1, len(inp) + 1) if i not in impossible_games])


print(f"Part a: {day2a(inp)}")  # wrong: 234092
test_input = read_input("day02_test.txt")
print(f"test input: {day2a(test_input)}")


def day2b(inp):
    game_power = []
    for i, game in enumerate(inp, 1):
        minimum_blocks = {}
        for turn in game:
            for c in turn.split(","):
                res = re.search("(\d+) (\w+)", c)
                no = int(res.group(1))
                color = res.group(2)
                minimum_blocks[color] = max(minimum_blocks.get(color, 0), no)
        game_power.append(
            minimum_blocks["red"] * minimum_blocks["green"] * minimum_blocks["blue"]
        )

    return sum(game_power)


print(f"test input: {day2b(test_input)}")
print(f"Part b: {day2b(inp)}")
