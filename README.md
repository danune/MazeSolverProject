# MazeSolverProject
CPSC481

Here is the code and files for the CPSC 481 project. The project is a maze solver project that creates a randomized maze and has a simple
AI/algorithm solve it. The program also has a UI that allows the user to test different sizes of mazes and different starting and end
points for the maze solver. It will display these results on a maze shown above the UI. This porject was made entirely using the Unity
game enigine and coded in C#.

To run the actual program, locate the file called MazeSolverProgram.zip and unzip the file. The program itself is an executable that should run the project when executed.

When executed, the program will ask about screen resolution and how you wish to display the project itself. Choose the the desired settings and click play once ready. The program will run and display the maze and several buttons as a UI.
The buttons within the progarm do the following:


The "Restart" button recreates another randomize maze based on the given criteria, which includes the start and end positions of the maze solver, and the number of rows and columns that the maze has. The changes that are implemented will not take effect until after the restart button is pressed.

The four arrow buttons to the right of "Start" change the starting position of the maze algorithm. The higher left arrow or "<" decreases the row(x) value for the starting position for the maze algorithm, while the lower left arrow "<" decreases the column(y) value for the starting postion for the algorithm in the maze. The higher right arrow ">" increases the row(x) value for the algorithm starting position, and the lower right arrow ">" increases the column(y) value for the algorithm starting position.

Similarily to the Start arrow buttons, the four arrow buttons to the right of "End" change the end position of the maze algorithm. They function exactly the same as the Start buttons do, except they effect the end position instead.

The buttons on the far right next to "Rows" and "Columns" are buttons that increase or decrease the number of rows or columns for the maze being displayed. They function the same way the Start and End buttons function (left arrow "<" decreases value of row/column, right arrow ">" increases value of row/column.

The "Solve" button will display the maze algorithm or maze solver with the solution to the maze when pressed. It will display on the maze in red, showing a path from the given starting position to the given end position.
