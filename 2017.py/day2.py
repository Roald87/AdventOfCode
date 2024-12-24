# -*- coding: utf-8 -*-
"""
Created on Sat Dec  2 18:18:24 2017

@author: roald
"""

import pandas as pd

data = pd.read_csv('2_spreadsheet.txt', sep='\t', header=None)

print(sum(data.max(axis=1) - data.min(axis=1)))

# Part 2

import itertools

chksum = 0

for row in data.iterrows():
    row = row[1]
    for pair in itertools.permutations(row, 2):
        if max(pair) % min(pair) == 0:
            chksum += (max(pair) / min(pair))
            break

print(chksum)
