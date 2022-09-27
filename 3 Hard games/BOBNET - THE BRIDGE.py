import copy

m = int(input())
v = int(input())
l = list()
for i in range(4):
    l.append(input() + '1' * 50)

state = {'t': 0}

def strategy(state, command = None):
    state['t'] += 1
    if state['t'] == 51:
        return None
    if command:
        if command == "SPEED":
            if state['s'] == 49: 
                return None
            state['s'] += 1
            for y in list(state['y']):
                if '0' in l[y][state['x']+1:state['x']+state['s']+1]:
                    state['y'].remove(y)
        elif command == "SLOW":
            if state['s'] == 0: 
                return None
            state['s'] -= 1
            for y in list(state['y']):
                if '0' in l[y][state['x']+1:state['x']+state['s']+1]:
                    state['y'].remove(y)
        elif command == "JUMP":
            if state['s'] == 0: 
                return None
            for y in list(state['y']):
                if '0' == l[y][state['x']+state['s']]:
                    state['y'].remove(y)
        elif command == "UP":
            if 0 in state['y']: 
                return None
            for y in list(state['y']): # order of list important
                state['y'].remove(y)
                if '0' not in l[y][state['x']+1:state['x']+state['s']] and '0' not in l[y-1][state['x']+1:state['x']+state['s']+1]:
                     state['y'].append(y-1)
        elif command == "DOWN":
            if 3 in state['y']: 
                return None
            for y in list(state['y'][::-1]): # order of list important
                state['y'].remove(y)
                if '0' not in l[y][state['x']+1:state['x']+state['s']] and '0' not in l[y+1][state['x']+1:state['x']+state['s']+1]:
                     state['y'].append(y+1)
        state['x'] += state['s']
        if len(state['y']) < v: 
            return None
        elif l[0][state['x']] == '1': 
            return [command]
    list_of_commands = ["SPEED", "JUMP", "UP", "DOWN", "SLOW"]
    if state['s'] == 0 and command == "UP":
        list_of_commands.remove("DOWN")
    if state['s'] == 0 and command == "DOWN":
        list_of_commands.remove("UP")    
    for com in list_of_commands:
        value = strategy(copy.deepcopy(state), com)
        if value:
            if command:
                value.append(command)
            return value
    return None

while True:
    state['y'] = list()
    state['s'] = int(input())
    for i in range(m):
        x, y, a = [int(j) for j in input().split()]
        if a == 1:
            state['x'] = x
            state['y'].append(y)
    print(strategy(state).pop())
    state['t'] += 1
    
# https://www.codingame.com/training/hard/the-bridge-episode-2