# game loop
while True:
    for i in range(8):
        mountain_h = int(input())
        if i == 0:
            tallest_index = 0
            tallest_h = mountain_h
        if mountain_h > tallest_h:
            tallest_index = i
            tallest_h = mountain_h
            
    print(tallest_index)

# https://www.codingame.com/training/easy/the-descent