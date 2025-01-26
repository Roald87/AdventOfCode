from collections import Counter, namedtuple
from enum import Enum
from itertools import starmap


def read_input(fname):
    with open(fname) as f:
        inp = []
        for line in f.readlines():
            inp.append((line.split()))

        return inp


class HandType(Enum):
    HIGH_CARD = 1
    ONE_PAIR = 2
    TWO_PAIR = 3
    THREE_OF_A_KIND = 4
    FULL_HOUSE = 7
    FOUR_OF_A_KIND = 8
    FIVE_OF_A_KIND = 9


def hand_type(cards):
    card_counts = [count for _, count in Counter(cards).most_common()]
    match card_counts:
        case [5]:
            return HandType.FIVE_OF_A_KIND
        case [4, 1]:
            return HandType.FOUR_OF_A_KIND
        case [3, 2]:
            return HandType.FULL_HOUSE
        case [3, 1, 1]:
            return HandType.THREE_OF_A_KIND
        case [2, 2, 1]:
            return HandType.TWO_PAIR
        case [2, 1, 1, 1]:
            return HandType.ONE_PAIR
        case _:
            return HandType.HIGH_CARD


def score_part1(card):
    return {suit: score for score, suit in enumerate("23456789TJQKA", 1)}[card]


Hand = namedtuple("Hand", "cards bid")


def part1(inp):
    inputs = list(starmap(Hand, inp))
    sorted_hands: list[Hand] = sorted(
        inputs,
        key=lambda hand: (
            hand_type(hand.cards).value,
            *[score_part1(card) for card in hand.cards],
        ),
    )

    return sum([i * int(hand.bid) for i, hand in enumerate(sorted_hands, 1)])


def try_increase_rank(cards):
    CardCounts = namedtuple("CardCounts", "card count")
    card_counts = list(starmap(CardCounts, Counter(cards).most_common()))

    sorted_card_counts: list[CardCounts] = sorted(
        card_counts,
        key=lambda hand: (hand.count, score_part1(hand.card)),
        reverse=True,
    )

    if sorted_card_counts[0].card == "J" and len(card_counts) > 1:
        return cards.replace("J", sorted_card_counts[1].card)
    else:
        return cards.replace("J", sorted_card_counts[0].card)


def score_part2(card):
    return {suit: score for score, suit in enumerate("J23456789TQKA", 1)}[card]


def part2(inp):
    hands = list(starmap(Hand, inp))
    sorted_hands = sorted(
        hands,
        key=lambda hand: (
            hand_type(try_increase_rank(hand.cards)).value,
            *[score_part2(card) for card in hand.cards],
        ),
    )

    return sum([i * int(bid) for i, (_, bid) in enumerate(sorted_hands, 1)])


if __name__ == "__main__":
    day = "day7"
    inp = read_input(f"{day}.txt")
    test_inp = read_input(f"{day}_test.txt")

    print(f"test input a: {part1(test_inp)}")
    print(f"Part a: {part1(inp)}")

    print(f"test input b: {part2(test_inp)}")
    print(f"Part b: {part2(inp)}")
