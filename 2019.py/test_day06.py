from assertpy import assert_that

from day06 import assemble_orbits, trace_route


def test_count_orbits():
    orbits = [
        ["COM", "B"],
        ["B", "C"],
        ["C", "D"],
        ["D", "E"],
        ["E", "F"],
        ["B", "G"],
        ["G", "H"],
        ["D", "I"],
        ["E", "J"],
        ["J", "K"],
        ["K", "L"],
    ]

    assert_that(sum(assemble_orbits(orbits).values())).is_equal_to(42)


def test_orbotal_transfers():
    orbits = [
        ["COM", "B"],
        ["B", "C"],
        ["C", "D"],
        ["D", "E"],
        ["E", "F"],
        ["B", "G"],
        ["G", "H"],
        ["D", "I"],
        ["E", "J"],
        ["J", "K"],
        ["K", "L"],
        ["K", "YOU"],
        ["I", "SAN"],
    ]

    orbit_nos = assemble_orbits(orbits)
    my_route = trace_route(orbits, "YOU")
    santas_route = trace_route(orbits, "SAN")

    for orbit in my_route:
        if orbit in santas_route:
            first_common_orbit = orbit
            break

    orbital_transfers = (
        orbit_nos["YOU"]
        - 1
        - orbit_nos[first_common_orbit]
        + orbit_nos["SAN"]
        - 1
        - orbit_nos[first_common_orbit]
    )

    assert_that(orbital_transfers).is_equal_to(4)
