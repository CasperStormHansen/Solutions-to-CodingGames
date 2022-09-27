l = int(input())
h = int(input())
t = input()
rows = {}
for i in range(h):
    rows[i] = input()

text_as_char_codes = [ord(a) for a in t]

def number_conversion(a):
    if 65 <= a <= 90:
        return a - 65
    elif 97 <= a <= 122:
        return a - 97
    else:
        return 26

text_as_position_codes = [number_conversion(a) for a in text_as_char_codes]

for line_index in range(h):
    out = ''
    for a in text_as_position_codes:
        out += rows[line_index][a*l:a*l+l]
    print(out)

# https://www.codingame.com/training/easy/ascii-art