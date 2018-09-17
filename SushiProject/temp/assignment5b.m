outString:
    .asciz "%s %dth, %d"
errorString:
    .asciz "usage mm dd yy"
    
    .align 4
    .global main
main:
    save %sp, -96, %sp
    
    
    
    
    ret
    restore
