.section ".bss"
.align 1
.common	ch, 1, 1	! char ch;

include(macro_defs.m)
define(EOF,1)

        
    .section ".data"
    .align 8
    
starString:
    .asciz "*"

charString:
    .asciz "%c"
    .align 1


    .section ".text"
    .align 4
   
    .global find!Make global for linkage
    
find: 
    save %sp, -96, %sp
    
doLoop: 

    !Start Do loop
    
    set ch, %l0 !set ch address to l0
    ldub [%l0], %l1!Load unsigned byte from address into l1
    nop
    set charString, %o0 !Set char string to get char
    set ch, %o1 !set char address to hold char
    call scanf !Scanf for user input
    nop

    cmp %l1, ' ' !while (ch == ' ');
    be doLoop !Repeat loop if condition holds
    nop
    cmp %l1, EOF !if (ch == EOF)
    bne endIf !Skip below code if condition is not met
    nop
    mov 0, %o0 !Exit needs to take an argument
    call exit !Call exit
    nop
   
   endIf:   ret
            restore
    
    
   .global expression !Made global for linkage

expression:
        save %sp, -96, %sp
        call term !Call term
        nop
        
                
        checkLoop: 
            !Set up to check loop
            set ch, %l0 !Set char address to l0
            ldub [%l0], %l1 !Load unsigned byte into l1
            nop
        
            cmp %l1, '+' !Compare unsigned byte to '+'
            be whileLoop !Loop since it met one of the 'or' conditions
            nop
            
            cmp %l1, '-' !Compare unsigned byte to '-'
            be whileLoop! Loop since it met one of the 'or' conditions
            nop
        
            ba endLoop !Otherwise fall through these conditions and skip loop
            nop
        
            whileLoop:
                
                call find !Call find subroutine
                nop
            
                
                call term !Call term subroutine
                nop
                
                set charString, %o0 !Set up char string
                mov %l1, %o1 !Move ch value into o1
                call printf !Print
                nop
            
                ba checkLoop !Set up the value of ch again to check while loop
                nop
        
        endLoop:
            ret
            restore

    .global term !Made global for linkage
    
term:
    save %sp, -96, %sp
    call factor !Call factor subroutine
    nop
    
    checkWhileLoop: !Set up while loop conditons
    
        set ch, %l0 !Set char address into l0
        ldub [%l0], %l1!Load unsigned byte into l1
        nop
    
        cmp %l1, '*' !Compare ch to '*'
        be takeWhile !Take while loop if this the case
        nop
        
        ba endWhile !Otherwise condition doesn't hold skip while loop code
        nop
        
    takeWhile:
        call find  !Call find subroutine
        nop
        
        call factor !Call factor subroutine
        nop
        
        set starString, %o0 !set starString to print
        call printf!Call print to print a *
        nop
        
        ba checkWhileLoop!Set up while loop conditions again
        nop
        
    endWhile:
        ret
        restore
 

    .global factor !Made global for linkage

factor:
    save %sp,-96,%sp
    
    set ch, %l0 !Set char address into l0
    ldub [%l0], %l1 !Load unsigned byte from char address into l1
    nop
    
    cmp %l1, '(' !Check if ch == '('
    bne elseBranch !Otherwise skip to the else branch
    nop
    
    !If Branch
    
        call find !Call find subroutine
        nop
    
        call expression !Call find subroutine
        nop
    
        ba endBranch !Skip else branch code
        nop
    
    elseBranch:
        set charString, %o0 !Set char string
        mov %l1, %o1 !Move ch value here
        call printf !Call print to print ch
        nop
        
        
    endBranch:
        call find !Call find subroutine
        nop

        ret 
        restore
