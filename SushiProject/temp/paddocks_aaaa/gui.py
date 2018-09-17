#Gui testing
import tkinter
import tkinter.messagebox
import ai
import random
import grid
from grid import Grid
from grid import GRID_HEIGHT
from grid import GRID_WIDTH
import threading

turn = random.randint(0, 1)
#t = threading.Timer(1.0)

class Gui:
    def __init__(self) :
        self.main_window = tkinter.Tk()
        
        self.main_label = tkinter.Label(self.main_window,text="Paddocks",font=('Times New Roman',16))
        self.main_label.grid(column = 0, row = 0)
        self.main_label = self 
        main_frame = self.create_main_grid()
        main_frame.grid(row=4, column=0,padx = 30)
        
        tkinter.mainloop()
   
    
    def create_main_grid(self):
        frame = tkinter.Frame(self.main_window)
        buttons = []

        for row_index in range(0,GRID_HEIGHT) :
            row = []
            for column_index in range(0,GRID_WIDTH) :
                button = tkinter.Button(frame, \
                                        text = "  ", \
                                        command=lambda row=row_index, \
                                            column=column_index: \
                                            self.main_button_clicked(row,column),
                                        state='normal')
                button.grid(row=row_index, column=column_index)
                
                #These if conditionals help seperate the different buttons
                #The spaces and dots are disabled
                #while only the vertical and horizontal remain the same
                if (row_index%2 == 0 and column_index%2 != 0):
                    button.config(width = 4)
                    button['bg'] = '#009acd'
                if (row_index%2 != 0 and column_index%2 == 0):
                    button.config(height = 4)
                    button['bg'] = '#009acd'
                if (row_index%2 != 0 and column_index%2 != 0):
                    button.config(width = 4, height = 4)
                    button['state'] = 'disabled'
                    button['bg'] = '#009acd'
                if (row_index%2 == 0 and column_index%2 ==0 ):
                    button['text'] = ", "
                    button['state'] = 'disabled'
                #Color codes come from http://www.color-hex.com/
                    button['bg'] = '#fffafa'
                
                row.append(button)
            buttons.append(row)

        self.main_buttons = buttons
        return frame



    def main_button_clicked(self,row,column) :
        global turn
        if not grid.game.grid_full():
            if turn == 0:
                if (row%2 == 0 and column%2 != 0):
                    direct = 'R'
                else:    
                    direct = 'D'                    
                repeat_turn = self.turn_taker(row, column, direct, 'I')
                if not repeat_turn:
                    turn = 1
                grid.game.grid_full()
         
            while turn == 1 and not grid.game.grid_full():
                column, row, direct = ai.get_input()
                repeat_turn = self.turn_taker(row, column, direct, '*')
                if not repeat_turn:
                    turn = 0    
                
        if grid.game.grid_full():
            self.check_win()
            
   
    def turn_taker(self, row, column, direct, name):
        button = self.main_buttons[row][column]
        if name == '*':
            button['bg'] = '#FF9429'
        else:     
            button['bg'] = '#e9967a'
        button['state'] = 'disabled'  
        grid.game.mutate_grid(row, column, direct)
        square_made = grid.game.square_created(name)
        if square_made:
            self.change_space_colour(row,column)
        return square_made
     
     
    def change_space_colour(self,row,column):
        for space_row in range(1, GRID_HEIGHT, 2):
            for space_column in range(1, GRID_WIDTH, 2): 
                space = grid.game.grid[space_row][space_column]
                button = self.main_buttons[space_row][space_column]
                button['foreground'] = '#fffafa'
                button['font'] = ('Times New Roman',10)
                button['text'] = grid.game.grid[space_row][space_column]
                if space != ' ' and space!= '*':
                    button['bg'] = '#cd2626'
                if space == '*':
                    button['bg'] = '#FFDD75'
                    
                    
    def check_win(self):
        winner = grid.game.win('I', '*')
        if winner == 'human':
            tkinter.messagebox.showinfo('FINISHED!', 'Congratulations, you win!')
        else:
            tkinter.messagebox.showinfo('GAME OVER', 'Better luck next time!')


            
#Instance of Gui        
game = Gui()
