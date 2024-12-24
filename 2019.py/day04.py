from itertools import count
from collections import Counter

possible_passwords = [
    i
    for i in range(246515, 739105)
    if len(set(str(i))) < 6 and i == int("".join(sorted(str(i))))
]
print("part a", len(possible_passwords))

possible_passwords_b = [
    i
    for i in range(246515, 739105)
    if 2 in dict(Counter(str(i))).values() and i == int("".join(sorted(str(i))))
]
print("part b", len(possible_passwords_b))
