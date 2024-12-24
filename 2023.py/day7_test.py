from functools import cmp_to_key

from assertpy import assert_that

from day7 import part1, read_input, part2, try_increase_rank


def test_part2():
    test_inp = read_input("day7_test.txt")

    assert_that(part2(test_inp)).is_equal_to(5905)


def test_part1_real_input():
    inp = read_input("day7.txt")

    assert_that(part1(inp)).is_equal_to(251287184)


def test_part2_real_input():
    inp = read_input("day7.txt")

    assert_that(part2(inp)).is_equal_to(250757288)


def test_increase_rank():
    assert_that(try_increase_rank("QJJQ2")).is_equal_to("QQQQ2")
    assert_that(try_increase_rank("T55J5")).is_equal_to("T5555")
    assert_that(try_increase_rank("KTJJT")).is_equal_to("KTTTT")
    assert_that(try_increase_rank("QQQJA")).is_equal_to("QQQQA")
    assert_that(try_increase_rank("QJJQQ")).is_equal_to("QQQQQ")
    assert_that(try_increase_rank("7J7QQ")).is_equal_to("7Q7QQ")
