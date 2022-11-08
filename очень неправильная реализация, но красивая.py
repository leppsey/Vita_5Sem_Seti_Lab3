frame1 = "1100011100101100111101001101011101101101111110001110010000000000000000"
frame1 = "11000100110010001100110011010000110101001101100011011100111000001110010000000000000000"
poly =   "10000010110001001"


def cut(f):
    while f[0] == '0':
        f = f[1:]
    return f


frame = frame1[0:len(poly)]
pointer = len(poly)

def xor(f, p):
    frame = int(f, 2)
    poly = int(p, 2)
    return bin(frame ^ poly)[2:]

otstup=0
n=1
print(str(n)+" "+frame1)
n+=1
while pointer != len(frame1):
    frame = cut(frame)
    while len(frame) != len(poly):
        frame += frame1[pointer]
        pointer+=1
        otstup+=1
    if n != 2:
        print(str(n)+" "+otstup*" "+frame)
        n += 1
    print(str(n)+" "+otstup*" "+poly)
    n += 1
    frame = xor(frame, poly)

print(str(n)+(otstup+1)*" "+" "+frame)
