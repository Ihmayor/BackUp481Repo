define(EOF,1)

include(macro_defs.m)

	.section ".bss"
	.align 1
	.common	ch, 1, 1	! char ch;

    .section ".data"
    .align 8
    
dstarString:
    .asciz "*"
charString:
    .asciz "%c"

    .section ".text"
    .align 4
    .global find
    
find: 
    save %sp, -96, %sp
    
doLoop:     
            set ch, %l0
            !ldub [%l0], %l1
            nop
            call scanf
            nop

!   while (ch == ' ');
    cmp ch, ' '
    be doLoop
  !  if (ch == EOF)
   cmp ch, EOF
   bne endIf
   nop
   call exit
   nop
   
   ! exit();
   endIf:   ret
            restore
    
expression:
    save %sp, -92, %sp
!    char op;
    call term
    nop


 !   while (ch == '+' || ch == '-')

  !  {

   ! op = ch;
    call find
    nop
    
    call term
    nop
    
    set charString, %o0
    !mov op, %o1
    call printf
    nop
    
    ret 
    restore

term:
    save %sp, -92, %sp
    call factor
    nop
    

!while (ch == '*')
    !find();
    !factor();
    
    set starString, %o1
    call printf
    nop

factor:
    !cmp for if (ch == '(') {
    !bne elseCondition
    
    call find
    nop
    
    call expression
    nop
    
    ba final
    nop

    elseCondition:
    set charString, %o0
    !mov ch to %o1
    call printf
    nop

    
    final:
        call find
        nop
 
    ret
    restore
    
