answer = 6000
n = int(input())
if n == 0:
    answer = 0
for i in input().split():
    t = int(i)
    if abs(t) < abs(answer) or abs(t) == abs(answer) and t > answer:
        answer = t

print(answer)

# https://www.codingame.com/training/easy/temperatures