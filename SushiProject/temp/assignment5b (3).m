.section ".data"
	.align 8

outthString:    .asciz "%s %dth, %d\n"
    
outstString:    .asciz "%s %dst, %d\n"

outrdString:    .asciz "%s %drd, %d\n"
     
outndString:    .asciz "%s %dnd, %d\n"
    
errorString:    .asciz "usage mm dd yy\n"

jan_m:	    .asciz "January"
feb_m:	    .asciz "Febuary"
march_m:    .asciz "March"
april_m:	.asciz "April"
may_m:	    .asciz "May"
june_m:	    .asciz "June"
july_m:	    .asciz "July"
aug_m:	    .asciz "August"
sept_m:	    .asciz "September"
oct_m:	    .asciz "October"
nov_m:	    .asciz "November"
dec_m:	    .asciz "December"
    .align 4
months:	.word	jan_m, feb_m, march_m, april_m, may_m, june_m, july_m, aug_m, sept_m, oct_m, nov_m, dec_m

    .section ".text"
    .align 4
    .global main
main:
    save %sp, -96, %sp
    
    cmp %i0, 4 !Checks that argc, amount of arguments is correct
    bne errorMessage !Otherwise branch to error message
    nop
    
    mov %i1, %l0 !argv ref
    add %l0, 4, %l0	! address of second arg (argv[1])
    ld [%l0],%o0 ! this puts the pointer to argv[1] in %o0
    
    call atoi !Call method to convert string to integer
	nop
	mov	%o0, %l2	! convert argv[1] to an int, mov to %o2 !Integer
    
    !Check Month is within the range 1-12
    cmp %l2, 1!Check that the month is NOT less than 1
    bl errorMessage!Otherwise branch to error message
    nop
    
    cmp %l2, 12 !Check that the month is NOT greater than 12
    bg errorMessage!Otherwise branch to error message
    nop
    
    sub %l2, 1, %l2 !Subtract 1 to get index since argv index count 0-11
	set months, %l3	! set pointer to start of months array
	sll	%l2, 2, %l1	! compute offset based on argv[1]
	add	%l3, %l1, %l3	! we now have the address of the correct !String element from the months array
    
    mov %i1, %l4!Place argv ref
    add %l4, 8, %l4!Gets third argv[2] to get day
    ld [%l4], %o0 !Loads to o-register to pass into atoi

    call atoi!Converts loaded value into an integer
    nop
    
    mov %o0, %l6!The day value is stored into an l-register
    
    
    
    mov %i1, %l5!Place argv into l5 register
    add %l5, 12, %l5 !Offset for arg[3]
    ld [%l5], %o0 !Place this into o-register to pass to atoi

    call atoi!Convert string to integer
    nop
    
    !Check Positive year
    cmp %o0, 1 !Make sure it is greater than one
    bl errorMessage !Otherwise branch to error message
    nop
    
    mov %o0, %o3 !The year is put into o3 register for print method call later
    
    
    !Check Days
    cmp %l6, 1 !Check that day is not less than 1
    bl errorMessage!Otherwise branch to error message
    nop
    
    cmp %l6, 31 !Check that day is not greater than 31
    bg errorMessage !Otherwise branch to error message
    nop
    
    !Check for the different suffixes for the date
    cmp %l6, 1 !Day = First
    be useSt !Use st branch
    nop
    
    cmp %l6, 21 !Day = Twenty-First
    be useSt!Use st branch
    nop
    
    cmp %l6, 31 !Day = Thirty-First
    be useSt!Use st branch
    nop
    
    cmp %l6, 2 !Day = Second
    be useNd !Use nd branch
    nop
    
    cmp %l6, 22 !Day = Twenty-Second
    be useNd !Use nd branch
    nop
    
    cmp %l6, 3 !Day = Third
    be useRd !Use rd branch
    nop
    
    cmp %l6, 23 !Day = Twenty-Thrid
    be useRd !Use rd branch
    nop
    
    useTh:
        set	outthString, %o0! use the th string for printf
        ba otherCode !Branch and skip all the other string assignments 
        nop
    useNd:
        set outndString, %o0 !Use the nd string for printf
        ba otherCode!Branch and skip all the other string assignments
        nop
    useRd:
        set outrdString, %o0 !Use the rd string for printf
        ba otherCode!Branch and skip all the other string assignments
        nop
    useSt:
        set outstString, %o0 !Use the st string for printf
    
    otherCode:
    
        ld [%l3], %o1! load the contents of that element, which is the months into o1
        mov %l6, %o2 !Mov register containing day into o2
     
        call printf !Call printf
        nop
        
        ba endProgram !Skip over error message
        nop
    
    errorMessage:
        set errorString, %o0 !Print error message
        call printf !Call printf to print
        nop
        
	endProgram:
        
        ret
        restore
