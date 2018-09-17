





 
!local variables

buf = -8

	.section ".data"
	.align 8
    
headerPrint:
    .asciz "Input Degrees\t\tSin Value\t\tCos Value\n"
 
inputPrint:
    .asciz "%.10f\t\t"
 
sinValue:
    .asciz "%.10f\t\t"
cosValue:
    .asciz "%.10f\n"
newLine:
    .asciz"\n"


errorString:
    .asciz "ERROR FILE CANNOT BE OPENED"
errorString2: .asciz "CANNOT BE 3"

    
pi: 
    .double 0r3.1415926515!Define pi here HA!

small:
	.double 0r1e-10 !Define smallest digit here


	.section ".text"
	.align 4
	.global main
main:
	save %sp, (-92+-8)&-8, %sp

	! get the first file name from argv[1]
	
    ld [%i1 + 4], %o0	! address of the string corresponding to src
	mov 0, %o1 !First arg of open
	clr %o2!Second arg of open
	mov 5, %g1! int fd = open(filename, mode, permissions)
	ta 0

	bcc open_ok !if Ok proced through rest of the process
	nop

    ba errorOpeningFile !Go to error.
    nop

open_ok:
	mov %o0, %l0!Store f.d

    set headerPrint, %o0
    call printf !Call print f for the column headings
    nop
    
read:
 
    
    mov %l0, %o0 !First arg of the read sys funct call.
    add %fp, buf, %o1 !Second arg of the read sys.funct call. Datat will be here
    mov 8, %o2 !How many bytes we're reading.
    mov 3, %g1 !Sys. func.call
    ta 0
    
    cmp %o0, 8 !Double check if we've read 8 bytes, otherwise stop reading loop.
	bne read_end
	nop
    
    ldd [%fp-8], %f0 !Stores what's in the stack to  %f0, %f1
    
    addcc %o0, 0, %l2 !Double check that it has been read correctly
    bg read_ok !Continue through the rest of the process
    nop
    
    ba read !Repeat loop otherwise to end
    nop

    
read_ok:
    
    
    !Convert Degrees to Radians    
    set pi, %l2	! address of pi
    ldd [%l2], %f2
    
       
    fmuld %f0, %f2, %f0 !degrees*pi
    
    

    
    !Converting integers to doubles
    mov 180, %l3        !Move the 180 constant to be used for radian/degree conversion to a register
	st %l3, [%fp-4]		! move 180 onto the stack
	ld [%fp-4], %f10	! at this point it's still an int in a float reg
	fitod %f10, %f10	! this converts the int to double

    
    fdivd %f0, %f10, %f0!degrees*pi/180
    
    
    !Print Degrees
    set inputPrint, %o0 !Set string to print
    std %f0, [%fp-8] !Store f-register on the stack since printf changes them.
    ld [%fp-8], %o1 !Since they're odd numbers 
    ld [%fp-4], %o2 !store these between their orders
    call printf
    nop
    
    
    ldd [%fp-8], %f0 !Load %f0 from the stack for the rest of the functions
    
   
    ldd [%fp-8],%o0 !Load this into the first arg of the sincos function
    mov 1, %o2 !To both distinguish this as a sin function call + be used for computation in that function. Called exp.
    call sincos !Call function
    nop
    
    std %o0, [%fp-16]
    ldd [%fp-16], %f2
    
     
    ldd [%fp-8], %o0 !Load x into cos(x)
    mov 0, %o2 !Distinguish this as cos(x)
    call sincos
    nop
    
    set newLine, %o0
    call printf
    nop
    
    ba read
    nop
    
    
sincos:
    save %sp, (-92+-8)&-8, %sp

    std %i0, [%fp-8] !Load first arg x
    ldd [%fp-8], %f0 


    mov %i2, %l1 !Move exp
    
    mov 0, %l3 !Sign
    clr %l0 !Clear register for small
    
    mov 0, %l2
    st %l2, [%fp-8]
    ld [%fp-8], %f2 !Clear register for calculating sum
    
    mov 0, %l2
    st %l2, [%fp-8]
    ld [%fp-8], %f12 !Clear register for calculating term
    
    whileLoopStart:
        set small, %l0
        ldd [%l0], %f4 !f4/f5    
        fcmpd %f12, %f4 !fcmp term, %f4 
        ble endWhileLoop !If less than or equal to right from the start then just skip the btottom.
        nop
        
        
        std %f0, [%fp-8] !Store %f0 on the stack
        ldd [%fp-8], %o0 !load it into an o-register for a power fucntion
        mov %l1, %o2 !Load exp
        call pow !Call said function.
        nop
        
        std %o0, [%fp-8] !Store value as the numerator       
        ldd [%fp-8], %f14 !Store this value in %f14
                        
        ldd [%l1], %o0   !mov exp, %o0
        call factorial !call factorial
        nop
        
        !Convert exp to double float     
            
        mov %o0, %l2    !Store value as the denominator  
        st %l2, [%fp-4] !Move the exp value onto the stack
        ld [%fp-4], %f8	! at this point it's still an int in a float reg
        fitod %f8, %f8	! !Convert denominator

        fdivd %f14, %f8, %f12 !Numerator divided by denominator
        cmp %l3,0 !If %l3 is not a zero skip to the appropriate sign code.
        bne elseCondition
        nop 
        
            faddd %f2, %f12, %f2 !sum += term
            mov 1, %l3 !sign = 1
            ba other
            nop
        
        elseCondition:
           fsubd %f2, %f12, %f2 !sum-=term
           mov 0, %l3 !Sign = 0
        
        other:        
            add %l1, 2, %l1 !exp +=2;

        
        ba whileLoopStart
        nop                    
    
    endWhileLoop:
    !Print cos/sin value."
    
    
    set sinValue, %o0
    std %f2, [%fp-8]
    ld [%fp-8], %o1
    ld [%fp-4], %o2
    call printf
    nop
    
    
    ret
    restore
    
    
pow:
    save %sp, -(92+8)&-8, %sp
    !Take %i0 into an %f0: This is x
    
    std %i0, [%fp-8] !Load first arg x
    ldd [%fp-8], %f0 !This is x
    
    mov %i2, %l0!This is exp
    clr %l1 !Use to count
    
    
    !Initialize exp^0
    mov 1, %l2
    st %l2, [%fp-4]
    ld [%fp-4], %f2 !To store Terms x^i
    fitod %f2, %f2  !Convert ints to doubles
    
    cmp %l0, 0  Compare it if it equals zero. It is is then return zero otherwise move to another bed.
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
        save %sp, (-92+-8)&-8, %sp
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
    


