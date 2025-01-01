# -*- coding: utf-8 -*-
"""
Created on Sun Dec 10 14:41:29 2017

@author: roald
"""

with open("day7_input.txt") as file:
    data = file.read().splitlines()

for i, line in enumerate(data):
    data[i] = line.split(" ")
    data[i][1] = int(data[i][1].lstrip("(").rstrip(")"))
    line = []
    for el in data[i]:
        try:
            line.append(el.rstrip(","))
        except AttributeError:
            line.append(el)
    data[i] = line


def part1():
    i = 0
    level = data[1][0]

    while i < len(data):
        if len(data[i]) > 3 and level in data[i][3:]:
            level = data[i][0]
            print(i, level)
            i = 0
            continue
        i += 1

    return level


# def part2():
#     levels = {part1(): 357}

#     while True:
#         for level, i in levels.items():
