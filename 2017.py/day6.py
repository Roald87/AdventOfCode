# -*- coding: utf-8 -*-
"""
Created on Wed Dec  6 18:46:35 2017

@author: roald
"""

def redistribute(data):
    max_idx = data.index(max(data))
    data_copy = data[:]
    data_copy[max_idx] = 0
    for i in range(max(data)):
        data_copy[(i + max_idx + 1) % len(data)] += 1

    return data_copy


with open('input_day6.txt', 'r') as file:
    data = file.read().split('\t')

data = list(map(int, data))

#data = [0, 2, 7, 0]

#print(data)

comp = [data]

for i in range(1, 10000):
#    print(comp[-1])
    candidate = redistribute(comp[-1])
    if candidate in comp:
        print(candidate)
        print('ans =', i)
        print('index =', i - comp.index(candidate))
        break
    else:
        comp.append(candidate)



