l, c, n = [int(i) for i in input().split()]
p = dict()
for i in range(n):
    p[i] = int(input())

def compute_ride(first_group_index):
    seats_left = l
    group_index = first_group_index
    while seats_left >= p[group_index] and (group_index != first_group_index or seats_left == l):
        seats_left -= p[group_index]
        group_index = (group_index + 1) % n
    return group_index, l - seats_left

catalogue = dict()
def ride(first_group_index):
    if first_group_index not in catalogue:
        catalogue[first_group_index] = compute_ride(first_group_index)
    return catalogue[first_group_index]

earnings = group_index = 0
for _ in range(c):
    group_index, seats_sold = ride(group_index)
    earnings += seats_sold
print(earnings)

# https://www.codingame.com/training/hard/roller-coaster