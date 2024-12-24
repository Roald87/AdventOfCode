import more_itertools
import matplotlib.pyplot as plt


def read_input(fname):
    with open(fname) as f:
        seeds = [int(seed) for seed in f.readline().strip().split(":")[1].split()]

        maps = []
        map = []
        title = ""
        for line in f.readlines():
            if line[0].isalpha():
                title = line.strip()
            elif line[0].isdigit():
                map.append([int(i) for i in line.split()])
            elif line == "\n" and len(title) > 0:
                maps.append((title, map))
                map = []
                title = ""

        return seeds, maps


day = "day5"
inp = read_input(f"{day}.txt")
# print(inp)
test_inp = read_input(f"{day}_test.txt")
# print(test_inp)


def part1(inp):
    seeds, maps = inp

    from_to = []
    for _map in maps:
        if len(from_to) == 0:
            for seed in seeds:
                for destination, source, length in _map[1]:
                    if seed in range(source, source + length):
                        from_to.append([seed, destination + (seed - source)])
                        break
                else:
                    from_to.append([seed, seed])
        else:
            for i, prev_maps in enumerate(from_to):
                prev_source = prev_maps[-1]
                for destination, source, length in _map[1]:
                    if prev_source in range(source, source + length):
                        from_to[i].append(destination + (prev_source - source))
                        break
                else:
                    from_to[i].append(prev_source)

    closest = sorted(from_to, key=lambda x: x[-1])

    return closest[0][-1]


#
# print(f"test input a: {part1(test_inp)}")
# print(f"Part a: {part1(inp)}")  # 282444444 wrong, 303894139 too low


def part2(inp):
    seeds, maps = inp
    del inp

    sorted_seeds = sorted(list(more_itertools.chunked(seeds, 2)), key=lambda x: x[0])
    sorted_maps = [[title, sorted(_map, key=lambda x: x[1])] for title, _map in maps]

    inputs = [(seed_start, seed_start + length) for seed_start, length in sorted_seeds]
    all_steps = []

    expected = [
        [(55, 68), (79, 93)],
        [(57, 70), (81, 95)],
        [(57, 70), (81, 95)],
        [(53, 57), (61, 70), (81, 95)],
        [(46, 50), (54, 63), (74, 88)],
        [(45, 56), (78, 81), (82, 86), (90, 99)],
        [(46, 57), (78, 81), (82, 86), (90, 99)],
        [(46, 56), (56, 60), (60, 61), (82, 85), (86, 90), (94, 97), (97, 99)],
    ]

    for imap, map_ranges in enumerate(sorted_maps):
        new_seeds = []
        for input_start, input_end in inputs:
            for ex_range in expected[imap]:
                plt.plot(ex_range, [-imap] * 2, linewidth=10, alpha=0.2, c="gray")
            plt.plot((input_start, input_end), [-imap] * 2, marker="+", linewidth=2)
            batch_start = input_start
            plt.scatter(batch_start, -imap)
            for destination_start, map_range_start, length in map_ranges[1]:
                map_range_end = map_range_start + length
                delta = destination_start - map_range_start
                plt.plot(
                    (map_range_start, map_range_end),
                    [-imap - 0.3] * 2,
                    marker="x",
                    linestyle="--",
                )

                # no more inputs
                if batch_start >= input_end:
                    break

                # entire input larger than map range
                if batch_start >= map_range_end:
                    continue

                # part before a src
                # |o------|
                #  ^^^<---->
                if batch_start < map_range_start:
                    batch_end = min(map_range_start, input_end)
                    new_seeds.append((batch_start, batch_end))
                    plt.arrow(x=batch_start, dx=0, y=-imap, dy=-1)
                    batch_start = batch_end
                    plt.scatter(batch_start, -imap)

                # no more inputs
                if batch_start >= input_end:
                    break

                # part in a src
                # |---o--|
                #     <---->
                #      ^^
                if batch_start in range(map_range_start, map_range_end):
                    batch_end = min(input_end, map_range_end)
                    new_seeds.append((batch_start + delta, batch_end + delta))
                    plt.arrow(x=batch_start, dx=delta, y=-imap, dy=-1)
                    batch_start = batch_end
                    plt.scatter(batch_start, -imap)

            if batch_start < input_end:
                new_seeds.append((batch_start, input_end))
                plt.arrow(x=batch_start, dx=0, y=-imap, dy=-1)

        # inputs = new_seeds
        inputs = (
            list(sorted(set(new_seeds), key=lambda x: x[0]))
            if len(new_seeds) > 0
            else inputs
        )
        all_steps.append(inputs)
        print(inputs)
    # plt.show()

    return all_steps


test_from_to = part2(test_inp)
expected = [
    # [(55, 68), (79, 93)],
    [(57, 70), (81, 95)],
    [(57, 70), (81, 95)],
    [(53, 57), (61, 70), (81, 95)],
    [(46, 50), (54, 63), (74, 88)],
    [(45, 56), (78, 81), (82, 86), (90, 99)],
    [(46, 57), (78, 81), (82, 86), (90, 99)],
    [(46, 56), (56, 60), (60, 61), (82, 85), (86, 90), (94, 97), (97, 99)],
]
for act, ex in zip(test_from_to, expected):
    try:
        assert act == ex
        print(f"ok Actual   {act},\nexpected    {ex}\n")
    except AssertionError:
        print(f"NO Actual   {act},\nexpected    {ex}\n")
# print(f"test input b: {part2(test_inp)}")  # 46?

print(f"Part b: {part2(inp)[-1][0]}")  # 225784067 too high
