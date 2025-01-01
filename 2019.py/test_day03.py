from assertpy import assert_that

from day03 import intersections, wiring, read_input, wire_distance


def test_intersections():
    testwires = read_input("test03")
    testwire1 = wiring(testwires[0])
    testwire2 = wiring(testwires[1])

    testcrossings = [(x, y) for x, y in intersections(testwire1, testwire2)]

    assert_that(testcrossings).is_equal_to([(7, 6), (4, 4)])


def test_distance():
    crossings = [(7, 6), (4, 4)]
    testwires = read_input("test03")
    testwire1 = wiring(testwires[0])
    testwire2 = wiring(testwires[1])

    distances1 = wire_distance(testwire1, crossings)
    assert_that(distances1).is_equal_to([15, 20])

    distances2 = wire_distance(testwire2, crossings)
    assert_that(distances2).is_equal_to([15, 20])
