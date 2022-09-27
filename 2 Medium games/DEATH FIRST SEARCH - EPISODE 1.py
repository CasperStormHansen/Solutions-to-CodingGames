n, l, e = [int(i) for i in input().split()]

links = []
for i in range(l):
    n1, n2 = [int(j) for j in input().split()]
    links.append((n1, n2))

gateways = []
for i in range(e):
    ei = int(input())
    gateways.append(ei)

immediate_neighbors = {}
for node in range(n):
    immediate_neighbors[node] = []
for link in links:
    immediate_neighbors[link[0]].append(link[1])
    immediate_neighbors[link[1]].append(link[0])

# game loop
while True:
    si = int(input())

    flag = True
    queue = [si]
    for node in queue:
        for neighbor in immediate_neighbors[node]:
            if neighbor not in queue:
                if neighbor in gateways:
                    if node in immediate_neighbors[neighbor]:
                        immediate_neighbors[neighbor].remove(node)
                    if neighbor in immediate_neighbors[node]:
                        immediate_neighbors[node].remove(neighbor)
                    print(str(node) + ' ' + str(neighbor))
                    flag = False
                    break
                else:
                    queue.append(neighbor)
        if flag == False:
            break

# https://www.codingame.com/training/medium/death-first-search-episode-1