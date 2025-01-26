# -*- coding: utf-8 -*-
"""
Created on Mon Dec  4 06:01:12 2017

@author: roald
"""

with open("passphrase.txt", "r") as file:
    data = file.read().split("\n")[:-1]

valid = 0
for line in data:
    line = line.split(" ")
    if len(line) == len(set(line)):
        valid += 1

print(valid)


valid2 = 0

for line in data:
    valid_words = True
    to_chk = []
    for word in line.split(" "):
        chars = [char for char in word]
        to_chk += ["".join(sorted(chars))]

    if len(to_chk) == len(set(to_chk)):
        valid2 += 1

print(valid2)
