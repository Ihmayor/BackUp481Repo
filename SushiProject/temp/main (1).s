.section    .init
.globl     _start

_start:
    b       main
    
.section .text

main:
    mov     sp, #0x8000
	
	bl		EnableJTAG

	bl		InitFrameBuffer

	// branch to the halt loop if there was an error initializing the framebuffer
	cmp		r0, #0
	beq		haltLoop$

	mov		r1, #100
	mov		r2, #100
	ldr		r3,	=0xFFFF
	bl		DrawPixel16bpp
    
haltLoop$:
	b		haltLoop$


/* Draw Pixel to a 1024x768x16bpp frame buffer
 * Note: no bounds checking on the (X, Y) coordinate
 *	r0 - frame buffer pointer
 *	r1 - pixel X coord
 *	r2 - pixel Y coord
 *	r3 - colour (use low half-word)
 */
DrawPixel16bpp:
	push	{r4}

	offset	.req	r4

	// offset = (y * 1024) + x = x + (y << 10)
	add		offset,	r1, r2, lsl #10
	// offset *= 2 (for 16 bits per pixel = 2 bytes per pixel)
	lsl		offset, #1

	// store the colour (half word) at framebuffer pointer + offset
	strh	r3,		[r0, offset]

	pop		{r4}
	bx		lr

.section .data


