#!/usr/bin/env python

# 1 2 3
# 4 5 6
# 7 8 9

# A B C
# D E F
# G H I

# J K L
# M N O
# P Q R

positions = "123456789ABCDEFGHIJKLMNOPQR"

posmap = {};

for i, c in enumerate(positions):
    posmap[c] = [i%3, int(i/3)%3, int(i/9)]

def posat(xyz):
    [x,y,z] = xyz
    return positions[x + y*3 + z*9]

def posflipX(xyz):
    [x,y,z] = xyz
    return [2-x,y,z]

def posflipY(xyz):
    [x,y,z] = xyz
    return [x,2-y,z]

def posflipZ(xyz):
    [x,y,z] = xyz
    return [x,y,2-z]

def posflipD(xyz):
    [x,y,z] = xyz
    return [y,x,z]

def flipX(n):
    return ''.join(posat(posflipX(posmap[c])) for c in n)

def flipY(n):
    return ''.join(posat(posflipY(posmap[c])) for c in n)

def flipZ(n):
    return ''.join(posat(posflipZ(posmap[c])) for c in n)

def flipD(n):
    return ''.join(posat(posflipD(posmap[c])) for c in n)

neighbours = {};

neighbours['1'] = "24568ABDE"
neighbours['2'] = "1345679ABCDEF"
neighbours['3'] = flipX(neighbours['1'])
neighbours['4'] = flipD(neighbours['2'])
neighbours['5'] = "12346789ABCDEFGHI"
neighbours['6'] = flipX(neighbours['4'])
neighbours['7'] = flipY(neighbours['1'])
neighbours['8'] = flipY(neighbours['2'])
neighbours['9'] = flipY(neighbours['3'])

neighbours['A'] = "1245BDEFHJKMN"
neighbours['B'] = "123456ACDEFGIJKLMNO"
neighbours['C'] = flipX(neighbours['A'])
neighbours['D'] = flipD(neighbours['B'])
neighbours['E'] = "123456789ABCDFGHIJKLMNOPQR"
neighbours['F'] = flipX(neighbours['D'])
neighbours['G'] = flipY(neighbours['A'])
neighbours['H'] = flipY(neighbours['B'])
neighbours['I'] = flipY(neighbours['C'])

neighbours['J'] = flipZ(neighbours['1'])
neighbours['K'] = flipZ(neighbours['2'])
neighbours['L'] = flipZ(neighbours['3'])
neighbours['M'] = flipZ(neighbours['4'])
neighbours['N'] = flipZ(neighbours['5'])
neighbours['O'] = flipZ(neighbours['6'])
neighbours['P'] = flipZ(neighbours['7'])
neighbours['Q'] = flipZ(neighbours['8'])
neighbours['R'] = flipZ(neighbours['9'])

# print neighbours

def dfs(prefix, nbrs):
    if len(prefix) == 4:
        print prefix
        return
    for n in nbrs:
        if n not in prefix:
            dfs(prefix+n, neighbours[n])

dfs("", "123456789")
