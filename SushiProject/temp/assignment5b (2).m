.section ".data"
	.align 8
string:	.asciz "num args: %d\targv[1]: %d\t%s\n"

outthString: .asciz "%s %dth, %d\n"
    
outstString:
    .asciz "%s %dst, %d\n"
    
errorString:
    .asciz "usage mm dd yy"

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
    
    !mov %i1, %l0
	add %i0, 4, %l0	! address of second arg (argv[1])
    ld [%l0],%o0 ! this puts the pointer to argv[1] in %o0
    call atoi
	nop
	mov	%o0, %o2	! convert argv[1] to an int, mov to %o2

	set months, %o3	! set pointer to start of months array
	sll	%o2, 2, %l1	! compute offset based on argv[1]
	add	%o3, %l1, %o3	! we now have the address of the correct
				! element from the months array
	ld	[%o3], %o3	! load the contents of that element, which is
				! another address, this time to the actual
				! string "jan" or "feb"

	set	string, %o0	! initialize our string for printf
	mov	%i0, %o1	! this is argc, number of command line params

				! %o2 has argv[1] converted to an int
				! %o3 has the addr of the string "jan" or "feb"
	call printf
	nop

	ret
	restore
