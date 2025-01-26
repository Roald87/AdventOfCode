def read_input(fname):
    with open(fname) as f:
        lines = [int(line) for line in f.readlines()]

    return lines


def day1a(numbers) -> int:
    return sum([n // 3 - 2 for n in numbers])


numbers = read_input("day01.txt")
print("day1a:", day1a(numbers))


def day1b(mass, total_fuel) -> int:
    fuel = mass // 3 - 2
    if fuel <= 0:
        return total_fuel
    else:
        return day1b(fuel, total_fuel + fuel)


print("day1b: ", sum(day1b(n, 0) for n in numbers))
