w, h = [int(i) for i in input().split()]
n = int(input())
x0, y0 = [int(i) for i in input().split()]
xmin = ymin = 0
xmax = w - 1
ymax = h - 1

# game loop
while True:
    bomb_dir = input()
    if 'D' in bomb_dir:
        ymin = y0 + 1
    elif 'U' in bomb_dir:
        ymax = y0 - 1
    else:
        ymin = ymax = y0

    if 'R' in bomb_dir:
        xmin = x0 + 1
    elif 'L' in bomb_dir:
        xmax = x0 - 1
    else:
        xmin = xmax = x0

    x0 = (xmax + xmin) // 2
    y0 = (ymax + ymin) // 2
    
    print(str(x0) + ' ' + str(y0))

# https://www.codingame.com/training/medium/shadows-of-the-knight-episode-1