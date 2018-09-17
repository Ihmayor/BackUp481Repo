
.section ".bss"
.align 1
.common	ch, 1, 1	! char ch;

include(macro_defs.m)
define(EOF,1)

op: .byte ''
        
    .section ".data"
    .align 8
    
starString:
    .asciz "*"

charString:
    .asciz "%c"
    .align 1

!    .global ch
!ch:	.byte 'j'

    .section ".text"
    .align 4
   
    .global find
    
find: 
    save %sp, -96, %sp
    
doLoop: 
    set ch, %l0
    ldub [%l0], %l1
    nop
    set charString, %o0
    set ch, %o1
    call scanf
    nop

!   while (ch == ' ');
    cmp %l1, ' '
    be doLoop
    nop
  !  if (ch == EOF)
    cmp %l1, EOF
    bne endIf
    nop
    mov 0, %o0
    call exit
    nop
   
   ! exit();
   endIf:   ret
            restore
    
   .global expression

expression:
        save %sp, -96, %sp
        call term
        nop
        
                
        checkLoop: 
            set ch, %l0
            ldub [%l0], %l1
            nop
        
            cmp %l1, '+'
            be whileLoop
            nop
            
            cmp %l1, '-'
            be whileLoop
            nop
        
            ba endLoop
            nop
        
            whileLoop:
                
                call find
                nop
            
                
                call term
                nop
                
                set charString, %o0
                mov %l1, %o1
                call printf
                nop
            
                ba checkLoop
                nop
        
        endLoop:
            ret
            restore

    .global term
term:
    save %sp, -96, %sp
    call factor
    nop
    
    checkWhileLoop:
        set ch, %l0
        ldub [%l0], %l1
        nop
    
        cmp %l1, '*'
        be takeWhile
        nop
        
        ba endWhile
        nop
    takeWhile:
        call find 
        nop
        
        call factor
        nop
        
        set starString, %o0
        call printf
        nop
        
        ba checkWhileLoop
        nop
        
    endWhile:
        ret
        restore
 

    .global factor

factor:
    save %sp,-96,%sp
    set ch, %l0
    ldub [%l0], %l1
    nop
    
    cmp %l1, '('
    bne elseBranch
    nop
    
    !If Branch
    call find
    nop
    
    call expression
    nop
    
    ba endBranch
    nop
    
    elseBranch:
        set charString, %o0
        mov %l1, %o1
        call printf
        nop
        
        
    endBranch:
        call find
        nop

        ret 
        restore
