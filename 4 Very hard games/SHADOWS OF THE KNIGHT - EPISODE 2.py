import math

w, h = [int(i) for i in input().split()]
n = int(input())
x0, y0 = [int(i) for i in input().split()]
xmin = ymin = 0
xmax = w - 1
ymax = h - 1

def update_min_max(oldmin, oldmax, old, new, bomb_dir, absolutemax):
    midpoint = (old + new) / 2
    if bomb_dir == 'SAME':
        return midpoint, midpoint
    if bomb_dir == 'COLDER' and old < new or bomb_dir == 'WARMER' and old > new:
        mi, ma = 0, math.floor(midpoint - 0.1)
    else:
        mi, ma = math.ceil(midpoint + 0.1), absolutemax
    return max(oldmin, mi), min(oldmax, ma)

def destination(mi, ma, old, absolutemax):
    midpoint = (mi + ma) / 2
    if old == 0 or old == absolutemax: # sacrifice gain in knowledge this turn to lower risk of getting back to the current edge
        new = math.floor(midpoint)
    else:
        new = int(old + 2 * (midpoint - old))
        new = min(max(new, 0), absolutemax)
    if new == old:
        new += 1
    return new

while True:
    bomb_dir = input()
    if bomb_dir != "UNKNOWN":
        if todo == 'x':
            xmin, xmax = update_min_max(xmin, xmax, xold, x0, bomb_dir, w - 1)
        else:
            ymin, ymax = update_min_max(ymin, ymax, yold, y0, bomb_dir, h - 1)
    todo = 'x' if xmin != xmax else 'y' if ymin != ymax else 'lastjump'
    if todo == 'lastjump':
        print(str(int(xmin)) + ' ' + str(int(ymin)))
    else:
        if todo == 'x':
            xold, x0 = x0, destination(xmin, xmax, x0, w - 1)
        else:
            yold, y0 = y0, destination(ymin, ymax, y0, h - 1)
        print(str(x0) + ' ' + str(y0))

# https://www.codingame.com/training/expert/shadows-of-the-knight-episode-2