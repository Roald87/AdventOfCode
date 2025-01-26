with open("captcha.txt", "r") as file:
    number = file.read().strip("\n")

total = 0
for i, digit in enumerate(number):
    if digit == number[(i + 1) % len(number)]:
        total += int(digit)

print("The answer to part 1 is {}.".format(total))

# Part 2

total2 = 0
for i, digit in enumerate(number):
    if digit == number[(i + len(number) // 2) % len(number)]:
        total2 += int(digit)

print("The answer to part 2 is {}.".format(total2))
