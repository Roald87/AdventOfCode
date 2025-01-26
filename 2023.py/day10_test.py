from assertpy import assert_that
from day10 import part1, read_input, part2


def test_part1_with_test_input():
    assert_that(part1(read_input("day10_test.txt"))).is_equal_to(4)
