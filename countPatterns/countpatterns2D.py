#!/usr/bin/env python

# 1 2 3
# 4 5 6
# 7 8 9

positions = "123456789"

posmap = {};

for i, c in enumerate(positions):
    posmap[c] = [i%3, int(i/3)%3]

def posat(xy):
    [x,y] = xy
    return positions[x + y*3]

def posflipX(xy):
    [x,y] = xy
    return [2-x,y]

def posflipY(xy):
    [x,y] = xy
    return [x,2-y]

def posflipD(xy):
    [x,y] = xy
    return [y,x]

def flipX(n):
    return ''.join(posat(posflipX(posmap[c])) for c in n)

def flipY(n):
    return ''.join(posat(posflipY(posmap[c])) for c in n)

def flipD(n):
    return ''.join(posat(posflipD(posmap[c])) for c in n)

neighbours = {};

neighbours['1'] = "24568"
neighbours['2'] = "1345679"
neighbours['3'] = flipX(neighbours['1'])
neighbours['4'] = flipD(neighbours['2'])
neighbours['5'] = "12346789"
neighbours['6'] = flipX(neighbours['4'])
neighbours['7'] = flipY(neighbours['1'])
neighbours['8'] = flipY(neighbours['2'])
neighbours['9'] = flipY(neighbours['3'])

# print neighbours

def dfs(prefix, nbrs):
    if len(prefix) == 4:
        print prefix
        return
    for n in nbrs:
        if n not in prefix:
            dfs(prefix+n, neighbours[n])

dfs("", "123456789")
