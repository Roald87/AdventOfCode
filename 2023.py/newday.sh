#!/bin/bash

# Check if an argument is provided
if [ $# -eq 0 ]; then
    echo "No arguments provided. Usage: newday.sh <day_number>"
    exit 1
fi

# Assign the argument to a variable
DAY=$1

# Create an empty test file
touch "day${DAY}_test.txt"

# Create a Python file with the specified content
cat <<EOF > "day${DAY}.py"
def read_input(fname):
    with open(fname) as f:
        # inp = [int(i) for i in f.readline().strip().split(",")]
        # inp = [i for i in f.readline().strip().split(",")]
        # inp = [int(i.strip()) for i in f.readlines()]
        # inp = [i.strip() for i in f.readlines()]

        return inp

def part1(inp):
    numbers = []
    return numbers

def part2(inp):
    return 0

if __name__ == "__main__":
    day = "day${DAY}"
    inp = read_input(f"{day}.txt")
    print(inp)
    test_inp = read_input(f"{day}_test.txt")
    print(test_inp)

    print(f"test input a: {part1(test_inp)}")
    print(f"Part a: {part1(inp)}")

    # print(f"test input b: {part2(test_inp)}")
    # print(f"Part b: {part2(inp)}")
EOF

cat <<EOF > "day${DAY}_test.py"
from assertpy import assert_that
from day${DAY} import part1, read_input, part2
EOF

echo "Files day${DAY}_test.txt and day${DAY}.py created successfully."
