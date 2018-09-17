include(macro_defs.m)

define(OPEN, 5)
define(CREATE, 8)
define(READ, 3)
define(WRITE, 4)
define(CLOSE, 6)

define(src_fd, l0)
define(dest_fd, l1)

local_var
var(buf, 1, 1*8)

	.section ".data"
	.align 8
print:
	.asciz "%c"

testPrint:
    .asciz "Test the floating point double precision: %.10f" 
testArgi:
    .asciz "Test argument: %s" 

testString:
    .asciz "\nARE YOU EVEN PRINTING???\n"

errorString:
    .asciz "ERROR FILE CANNOT BE OPENED"
errorString2: .asciz "CANNOT BE READ"

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
	mov OPEN, %g1		! int fd = open(filename, mode, permissions)
	ta 0

	bcc open_ok
	nop

    ba errorOpeningFile
    nop

open_ok:
	mov %o0, %src_fd!Store f.d



    set testString, %o0
    call printf
    nop
	! here is the stuff of read/write
read:    
    mov %src_fd, %o0
    add %fp, buf, %o1
    mov 8, %o2
    mov READ, %g1
    ta 0

    std %o0, [%fp-8]
    ldd [%fp-8], %f0
    addcc %o0, 0, %l2
    bg read_ok
    nop


    ba errorReadingFile
    nop

read_ok:
    
       
    !ldd [%fp-8], %f0! Stores what's in the stack to  %f0, %f1
    fitod %f0, f%4
    
    


    
    set testPrint, %o0
    std %f0, [%fp-8]
    ld [%fp-8], %o1
    ld [%fp-4], %o2
    call printf
    nop
    
    ba read
    nop

    !set testString, %o0
    !call printf
    !nop    

    



	mov %src_fd, %o0
	mov CLOSE, %g1		! close(src_fd)
	ta 0
 
    

	mov 1, %g1		! exit()
	ta 0
    ba end
    nop

errorReadingFile:
    set errorString2, %o0
    call printf
    nop
   
    ba end
    nop

errorOpeningFile:
    set errorString, %o0
    call printf
    nop

end:
    ret
    restore
	!mov 1, %g1		! if file didn't open, exit()
	!ta 0



