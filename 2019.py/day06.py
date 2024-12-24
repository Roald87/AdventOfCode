import itertools


def read_input(fname):
    with open(fname) as f:
        lines = [line.strip().split(")") for line in f.readlines()]

    return lines


orbits = read_input("day06.txt")


def assemble_orbits(orbits):
    def find_orbit_number(parent, acc_orbit_list) -> dict:
        children = [orbit[1] for orbit in orbits if orbit[0] == parent]
        if len(children) == 0:
            return acc_orbit_list
        else:
            new_orbit_nos = {child: acc_orbit_list[parent] + 1 for child in children}
            acc_orbit_list |= new_orbit_nos
            for child in children:
                find_orbit_number(child, acc_orbit_list)
            return acc_orbit_list

    orbit_nos = find_orbit_number("COM", {"COM": 0})

    return orbit_nos


orbit_nos = assemble_orbits(orbits)
print("Part a:", sum(orbit_nos.values()))


# find common orbit
def trace_route(orbits, child):
    def trace(child, acc_route):
        if child == "COM":
            return acc_route
        else:
            acc_route += [orbit[0] for orbit in orbits if orbit[1] == child]
            return trace(acc_route[-1], acc_route)

    route = trace(child, [])

    return route


my_route = trace_route(orbits, "YOU")
santas_route = trace_route(orbits, "SAN")

for orbit in my_route:
    if orbit in santas_route:
        first_common_orbit = orbit
        break

print(f"First common orbit={first_common_orbit}")
print(
    f"Part B:{(orbit_nos['YOU'] - 1 - orbit_nos[first_common_orbit]) + (orbit_nos['SAN'] - 1 - orbit_nos[first_common_orbit])}"
)
