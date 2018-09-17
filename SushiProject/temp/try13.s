





 
!local variables

buf = -8

	.section ".data"
	.align 8

headerPrint:
    .asciz "Input Degrees\t\tSin Value\t\tCosValue\n"
 
inputPrint:
    .asciz "%.10f\t\t"
 
sinValue:
    .asciz "%.10f\t\t"
cosValue:
    .asciz "%.10f\n"
    
testPrint:
    .asciz "Testing some double: %.10f" 
piPrint:
    .asciz "THIS IS SHOULD BE PI: %.10f"

intTest:
    .asciz "Test argument: %d\n" 


testString:
    .asciz "\nARE YOU EVEN PRINTING???\n"

errorString:
    .asciz "ERROR FILE CANNOT BE OPENED"
errorString2: .asciz "CANNOT BE 3"

    
pi: .double 0r3.14159265159265!Define pi here HA!

small:
	.double 0r1e-10 !Define smallest digit here


	.section ".text"
	.align 4
	.global main
main:
	save %sp, (-92+-16)&-8, %sp

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

    set headerPrint, %o0
    call printf
    nop
    
read:
 
    mov %l0, %o0
    add %fp, buf, %o1
    mov 8, %o2
    mov 3, %g1
    ta 0

    cmp %o0, 8
	bne read_end
	nop
    
    ldd [%fp-8], %f0 !Stores what's in the stack to  %f0, %f1
    
    addcc %o0, 0, %l2
    bg read_ok
    nop
    
    ba read
    nop

    
read_ok:
    

    ldd [%fp-8], %f0
    !Convert Degrees to Radians
    ! d*pi divided = r
    
    set pi, %l2		! address of pi
	ldd [%l2], %f2  !Load pi into an f-register

    !set piPrint, %o0
    !std %f2, [%fp-16]
    !ld [%fp-16], %o1
    !ld [%fp-12], %o2
    !call printf
    !nop
    
    fmuld %f0, %f2, %f0 !degrees*pi
    
    
    !Converting integers to doubles
    mov 180, %l3
	st %l3, [%fp-4]		! move 180 onto the stack
	ld [%fp-4], %f10	! at this point it's still an int in a float reg
	fitod %f10, %f10	! this converts the int to double

    fdivd %f0, %f10, %f0!degrees*pi/180
     
    set testPrint, %o0
    std %f0, [%fp-8]
    ld [%fp-8], %o1
    ld [%fp-4], %o2
    call printf
    nop
    
    
    std %f0, [%fp-8]
    
    !ldd [%fp-8],%o0
    !mov 1, %o2
    !call sincos
    !nop
    
    !std %o0, [%fp-8]
    !ldd [%fp-8], %f4
    !std %f0, [%fp-8]
    
    !set testPrint, %o0
    !std %f4, [%fp-8]
    !ld [%fp-8], %o1 
    !ld [%fp-4], %o2 
    !call printf
    !nop
    
    
    !ldd [%fp-8], %o0
    !mov 0, %o2
    !call sincos
    !nop


    !Send this to the sincosx function
    !sincos(x,1) = sin(x)
    !sincos(x,0) = cos(x)
    
    
    
    
    !Print Statements below check the input is read!
!    set testPrint, %o0
!    std %f0, [%fp-8]
!    ld [%fp-8], %o1
!    ld [%fp-4], %o2
!    call printf
!    nop
    
    ba read
    nop
    
    
sincos:
    save %sp, (-92+-8)&-8, %sp

    std %i0, [%fp-8] !Load first arg x
    ldd [%fp-8], %f0 


    mov %i2, %l1 !Move exp
    !mov 3, %l1 !Move exp
    
    !Check second arg
!    set intTest, %o0
!    mov %l0, %o1
 !   call printf
 !   nop
  

    !Check # of function calls
  !  set testString, %o0
   ! call printf
   ! nop
    
    clr %l0 !Clear register for sign
    mov 0, %l2
    st %l2, [%fp-8]
    ld [%fp-8], %f2 !Clear register for calculating term
    
    whileLoopStart:
        set small, %l0
        ldd [%l0], %f4 !f4/f5    
        fcmpd %f2, %f4 !fcmp term, %f4 
        ble endWhileLoop
        nop
        
        
        !Check first arg
        !set testPrint, %o0
        !std %f0, [%fp-16]
        !ld [%fp-16], %o1
        !ld [%fp-12], %o2
        !call printf
        !nop
        
        std %f0, [%fp-8]
        ldd [%fp-8], %o0
        mov %l1, %o2
        call pow
        nop
        !Store value as the numerator
        std %o0, [%fp-16]        
        ldd [%fp-16], %f6
        
        !Check Pow output
        !set testPrint, %o0
        !std %f6, [%fp-16]
        !ld [%fp-16], %o1
        !ld [%fp-12], %o2
        !call printf
        !nop
        
        !ldd [%fp-16], %f6
        
       ! set errorString2, %o0
        !call printf
        !nop
        
        
        mov %l1, %o0         !mov exp, %o0
        call factorial !call factorial
        nop
        
        mov %o0, %l2          !Store value as the denominator  
        st %l2, [%fp-4]		! move 180 onto the stack
        ld [%fp-4], %f8	! at this point it's still an int in a float reg
        fitod %f8, %f8	! !Convert denominator

        fdivd %f6, %f8, %f2
                
        add %l1, 2, %l1
        
        ba whileLoopStart
        nop                    
    
    endWhileLoop:
    
    
    ret
    restore
    
    
pow:
    save %sp, -92, %sp
    !Take %i0 into an %f0: This is x
    
    std %i0, [%fp-8] !Load first arg x
    ldd [%fp-8], %f0 !This is x
    
    mov %i2, %l0!This is exp
    clr %l1 !Use to count
    
    
    !Initialize exp^0
    mov 1, %l2
    st %l2, [%fp-4]
    ld [%fp-4], %f2 !To store Terms x^i
    fitod %f2, %f2 
    
    cmp %l0, 0  
    bne loop1
    nop
    
    
    ba endLoop
    nop
    
    
    loop1:                
        fmuld %f0,%f2,%f2
        add %l1,1 , %l1
        cmp %l1, %l0
        bne loop1
        nop
    
    endLoop:
        std %f2, [%fp-8]
        ldd [%fp-8], %i0
    
        ret
        restore
    
factorial:
        save %sp, -92, %sp
        !Move i0 into an l-register or float register.
        !Start with counter at 1
        !Multiply this value by one more of itself until it reaches 
        !i0
        mov %i0, %l0
        mov 1, %l1
        mov 1, %l2
        whileFactorial:
            cmp %l0, %l1
            bl endWhileFact
            nop
            
            mov %l1, %o0
            mov %l2, %o1
            call .mul
            nop

            mov %o0, %l2
            
            add 1, %l1, %l1
            ba whileFactorial
            nop
        endWhileFact:
            
            mov %l2, %i0
            ret
            restore
            
        
         
        

read_end:    
    mov %l0, %o0
	mov 6, %g1		! close(l0)
	ta 0
    
    mov 4, %o1
    set intTest, %o0
    call printf
    nop
    
    
    ba end
    nop
errorReadingFile:
    set errorString2, %o0
    call printf
    nop
    
	mov %l0, %o0
	mov 6, %g1		! close(l0)
	ta 0

   
    ba end
    nop

errorOpeningFile:
    set errorString, %o0
    call printf
    nop

end:
    ret
    restore
    


