from assertpy import assert_that
from day8 import part1, read_input, part2


def test_part1_test_data():
    test_inp = read_input("day8_test.txt")
    assert_that(part1(test_inp)).is_equal_to(2)


def test_part2_test_data():
    test_inp = read_input("day8_test_part2.txt")
    assert_that(part2(test_inp)).is_equal_to(6)
