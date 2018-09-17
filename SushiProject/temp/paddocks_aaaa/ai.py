#import all the grid and random module
import random
import grid

#Give the ai its name
name = '*'

#Get input from the ai
def get_input():
    #Get the ai to choose a random column, row and direction out of the four possible choices they have
    row, column, direction = get_position()
    while not grid.on_board(row, column, direction) or grid.game.line_overlap(column, row, direction):
        #Get the ai to choose a random column, row and direction out of the four possible choices they have
        row, column, direction = get_position()
    row, column, direction = value_conversion(row, column, direction)
    return column, row, direction

    
    
def get_position():
    column = random.randint(0, 3)
    row = random.randint(0, 3)
    direction =  random.randint(0, 1)
    return row, column, direction
    
    
    
def value_conversion(row, column, direction):
    if direction == 0:
        direction = 'D'      
        column = 2*column
        row = 2*row + 1
    if direction == 1:
        direction = 'R'
        column = 2*column + 1 
        row = 2*row 
    return row, column, direction
   
	

