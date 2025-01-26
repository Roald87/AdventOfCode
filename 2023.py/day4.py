import math
from collections import Counter


def read_input(fname):
    with open(fname) as f:
        inp = []
        for line in f.readlines():
            for win, mine in [line.split(":")[1].split("|")]:
                inp.append(([n for n in win.split()], [n for n in mine.split()]))

        return inp


day = "day4"
inp = read_input(f"{day}.txt")
# print(inp)
test_inp = read_input(f"{day}_test.txt")
# print(test_inp)


def part1(inp):
    score = []
    for winning_numbers, my_numbers in inp:
        winning = set(winning_numbers)
        mine = set(my_numbers)
        score.append(math.floor(2 ** (len(mine.intersection(winning)) - 1)))

    return sum(score)


print(f"test input a: {part1(test_inp)}")
print(f"Part a: {part1(inp)}")
assert 27845 == part1(inp)


def part2(inp):
    scores = []
    for winning_numbers, my_numbers in inp:
        winning = set(winning_numbers)
        mine = set(my_numbers)
        scores.append(len(mine.intersection(winning)))

    cards = Counter(list(range(1, len(inp) + 1)))
    for card_no, score in enumerate(scores, 1):
        new_cards = list(range(card_no + 1, card_no + score + 1)) * cards[card_no]
        cards.update(new_cards)

    return sum(cards.values())


print(f"test input b: {part2(test_inp)}")
ans_b = part2(inp)
print(f"Part b: {ans_b}")
assert ans_b == 9496801
