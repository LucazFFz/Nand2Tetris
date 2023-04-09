// Computes: 
// RAM[2] = RAM[0] * RAM[1]

// i = R1;
// 
// while(i - 1 > 0)
//    R2 = R2 + R0
//    i = i - 1

    @R1
    D=M
    @i
    M=D // i = R1

(LOOP)
    @i
    D=M-1
    @END
    D;JLT // if i - 1 < 0

    @R0
    D=M 
    @R2
    M=D+M // R2 = R2 + R0

    @i
    M=M-1 // i = i - 1

    @LOOP
    0; JMP

(END)
    @END
    0; JMP