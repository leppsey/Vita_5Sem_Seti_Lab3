# frame = int("1101011111",2)
#poly = int("10011", 2)
name = "31 32 33 34 35 36 37 38 39".split()
frame = ""
for i in name:
    frame += bin(int(i, 16))[2:]
frame = "1100010011001000110011001101000011010100110110001101110011100000111001"
frame = int(frame,2)

poly = int("10589", 16)

print(bin(frame))
print(bin(poly))

def CRC(frame, poly):
    def get_degree(a):
        if a == 0:
            return 0
        degree = 0
        while 2 ** degree / a <= 1:
            degree += 1
        return degree

    poly_degree = get_degree(poly)
    frame = frame << poly_degree - 1
    while get_degree(frame) >= poly_degree:
        if get_degree(frame) > get_degree(poly):
            poly = poly << get_degree(frame) - get_degree(poly)
        else:
            poly = poly >> get_degree(poly) - get_degree(frame)
        frame = frame ^ poly
    return frame


def ParityHor(num):
    while num > 1:
        lastBit = num ^ ((num >> 1) << 1)
        num = (num >> 1) ^ lastBit
    return num


def ParityVert(bytes):
    checkSum = bytes[0] ^ bytes[1]
    for i in range(2, len(bytes)):
        checkSum = checkSum ^ bytes[i]
    return checkSum

print("----Метод  CRC----")
print("Введенная строка -", frame)
print("Полином -", poly)
print("Соль -", hex(CRC(frame,poly)))