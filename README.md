# MazeSolverProject
CPSC481

Here is the code and files for the CPSC 481 project. The project is a maze solver project that creates a randomized maze and has a simple
AI/algorithm solve it. The program also has a UI that allows the user to test different sizes of mazes and different starting and end
points for the maze solver. It will display these results on a maze shown above the UI. This porject was made entirely using the Unity
game enigine and coded in C#.

The Assests folder holds all of the 3d models, effects, sounds, or other similar objects used for the Unity 3d space. For this maze project, it holds the wall, ground, color materials, and scrpits used within the 3d space. The actual code for the project is within a folder called "Scripts" that is inside the Assets folder. Some files within this code that were used to create the maze were taken and altered from a github that I obtained from the follwoing link: https://www.youtube.com/watch?v=IrO4mswO2o4&t=539s. These files were edited in order to function properly with the maze algorithm that I created. All other files and folders within this project were created by Unity as settings for the project itself or external libraries that Unity needs to function properly.



To run the actual program, locate the file called "MazeSolverProgram.zip" and unzip the file. The program itself is an executable that should run the project when executed.

When executed, the program will ask about screen resolution and how you wish to display the project itself. Choose the the desired settings and click play once ready. The program will run and display the maze and several buttons as a UI.
The buttons within the progarm do the following:


The "Restart" button recreates another randomize maze based on the given criteria, which includes the start and end positions of the maze solver, and the number of rows and columns that the maze has. The changes that are implemented will not take effect until after the restart button is pressed.

The four arrow buttons to the right of "Start" change the starting position of the maze algorithm. The higher left arrow or "<" decreases the row(x) value for the starting position for the maze algorithm, while the lower left arrow "<" decreases the column(y) value for the starting postion for the algorithm in the maze. The higher right arrow ">" increases the row(x) value for the algorithm starting position, and the lower right arrow ">" increases the column(y) value for the algorithm starting position.

Similarily to the Start arrow buttons, the four arrow buttons to the right of "End" change the end position of the maze algorithm. They function exactly the same as the Start buttons do, except they effect the end position instead.

The buttons on the far right next to "Rows" and "Columns" are buttons that increase or decrease the number of rows or columns for the maze being displayed. They function the same way the Start and End buttons function (left arrow "<" decreases value of row/column, right arrow ">" increases value of row/column.

The "Solve" button will display the maze algorithm or maze solver with the solution to the maze when pressed. It will display on the maze in red, showing a path from the given starting position to the given end position.

Let me know if there are any issues with the code or project itself by contacting me as soon as you can. Thanks.
