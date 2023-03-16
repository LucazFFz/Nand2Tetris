// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

@status
M=-1 // status == 0xFFFF
@LOOP
0;JMP

(LOOP)
    @KBD
    D=M

    @SETSCREEN
    D;JEQ 
    D=-1
    @SETSCREEN
    0;JMP

(SETSCREEN) // D is equal to the new status
    @newStatus
    M=D

    @status
    D=D-M // if status is the same as before, D will be 0
    @LOOP
    D;JEQ // if new status == old status, do nothing

    @newStatus
    D=M
    @status
    M=D // status = newStatus

    @SCREEN
    D=A // address = 16384 (base address of the HACK screen)
    @8192 // number of bytes the screen displays
    D=D+A // D = the byte after the last screen address
    @i
    M=D 

    @RENDERSCREEN
    0;JMP

(RENDERSCREEN)
    @i
    D=M-1 
    M=D // Decrement i by 1
    @LOOP
    D;JLT // if i < 0 goto LOOP

    @status
    D=M
    @i
    A=M
    M=D // M[current screen address] = status
    @RENDERSCREEN
    0;JMP 