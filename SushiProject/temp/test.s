








	.section ".data"
	.align 8
print:
	.asciz "%c"

testPrint:
    .asciz "Test the floating point double precision: %f" 
testArgi:
    .asciz "Test argument: %s" 

testString:
    .asciz "ARE YOU EVEN PRINTING???"

errorString:
    .asciz "ERROR FILE CANNOT BE OPENED"

x_m: .double 0r0.0

	.section ".text"
	.align 4
	.global main
main:
	save %sp, -96, %sp
    
    set testString, %o0
    call printf
    nop


	! get the first file name from argv[1]
	
    ld [%i1 + 4], %o0	! address of the string corresponding to src
	mov 0, %o1
	clr %o2
	mov 5, %g1		! int fd = open(filename, mode, permissions)
	ta 0

	bcc open_ok
	nop

    ba errorOpeningFile
    nop

open_ok:
	mov %o0, %l0!Store f.d



    set testString, %o0
    call printf
    nop
	! here is the stuff of read/write

	mov %l0, %o0
	mov 6, %g1		! close(l0)
	ta 0
 
    set testString, %o0
    call printf
    nop

	!mov 1, %g1		! exit()
	!ta 0

errorOpeningFile:
    set errorString, %o0
    call printf
    nop

    ret
    restore
	!mov 1, %g1		! if file didn't open, exit()
	!ta 0



