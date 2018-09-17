#Create the gird with markers, lines and spaces
C = ','
S = ' '
L = ' '

             
GRID_HEIGHT = 7
GRID_WIDTH = 7

    #FUNCTIONS THAT CHECK THE VALIDITY OF THE PLAYER INPUT
    #-----------------------------------------------------

    #This function makes sure that the player does not pick a line that goes off the board
def on_board(row, column, direct):
    on_board = False
    if direct == 1:
        if 0 <= column < 3 and 0 <= row <=3:
            on_board = True
    elif direct == 0:
        if 0 <= row < 3 and 0 <= column <=3:
            on_board = True
    return on_board


#FUNCTIONS USED TO CREATE, CHECK, AND MODIFY THE GRID
#---------------------------------

class Grid:

    def __init__(self):
        self.grid = [[C, L, C, L, C, L, C], \
                     [L, S, L, S, L, S, L], \
                     [C, L, C, L, C, L, C], \
                     [L, S, L, S, L, S, L], \
                     [C, L, C, L, C, L, C], \
                     [L, S, L, S, L, S, L], \
                     [C, L, C, L, C, L, C]]
                     
                     
    
    #The mutate grid function that mutates the grid, calls the square function and prints the grid
    def mutate_grid(self, row, column, direct):
        #Calculations for making a horizontal line 
        if direct == 'R':        
            self.grid[row][column] = '_'
        #Calculations for making a vertical line 
        elif direct == 'D':
            self.grid[row][column] = '|'
        

        
    def square_created(self, name): 
        #resetting the tally and square_made so it only checks for the turn it is on
        tally = 0
        square_made = False
        #Selecting each space in turn to see if it is surrounded
        for space_row in range(1, GRID_HEIGHT, 2):
            for space_column in range(1, GRID_WIDTH, 2): 
                #If they do form a square, and the space is empty put the player letter in the selected space
                if self.all_lines_filled(space_row, space_column) and self.grid[space_row][space_column] == ' ':
                    self.grid[space_row][space_column] = name  
                    square_made = True
        return square_made


        
    #A function that checks if the space's adjacent lines are all filled    
    def all_lines_filled(self, space_row, space_column):
        all_lines_filled = False
        if self.grid[space_row][space_column - 1] != ' ' and self.grid[space_row][space_column + 1] != ' '\
        and self.grid[space_row - 1][space_column] != ' ' and self.grid[space_row + 1][space_column] != ' ':
            all_lines_filled = True
        return all_lines_filled




  
    

    #This function makes sure that the player does not pick a spot that already has a line on it   
    def line_overlap(self, column, row, direct):
        line_overlap = False
        if direct == 1:        
            column_index = 2*column + 1
            row_index = 2*row       
        if direct == 0:
            column_index = 2*column
            row_index = 2*row + 1
        #Based on the above it uses index to check for overlap
        if self.grid[row_index][column_index] != ' ':
           line_overlap= True 
        return line_overlap    


        
    #FUNCTIONS USED TO GET TO THE END OF THE GAME
    #--------------------------------------------    

    #Function to check if the grid is full
    def grid_full(self):
        grid_full = True
        #Nested 'for loop' checks one row and then it checks every column in that row. 
        for row_space_check in range(1,GRID_HEIGHT,2):
            for column_space_check in range (1,GRID_WIDTH,2):       
                #The below shortens the point being checked to help shorten the conditional 
                grid_point = self.grid[row_space_check][column_space_check]          
                if  grid_point == ' ':
                    grid_full = False                            
        return grid_full
        
        
    
    #Function that finds who is the winner of the game 
    def win(self, human_name, ai_name):
        #setting accumulators to start from 0
        win_h = 0
        win_ai = 0
        #It goes through each space and adds a point to each player for every space their name fills
        for check_v in range(0, GRID_HEIGHT):
            for check_h in range (0, GRID_WIDTH):       
            #The below shortens the point being checked to help shorten the conditional 
                grid_point = self.grid[check_v][check_h]
                if grid_point == human_name:
                    win_h = win_h + 1
                if grid_point == ai_name:
                    win_ai = win_ai + 1 
                    
        #If the ai has more spaces filled, then they win
        if win_ai > win_h:
            winner = 'ai'
        if win_h > win_ai:
            winner = 'human'            
        return winner

            
            
#CREATING AN INSTANCE OF THE GAME!!!        
game = Grid()
