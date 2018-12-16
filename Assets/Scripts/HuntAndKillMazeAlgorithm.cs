using UnityEngine;
using System.Collections;

public class HuntAndKillMazeAlgorithm : MazeAlgorithm {

	private int currentRow = 0;
	private int currentColumn = 0;

	private bool courseComplete = false;

	public HuntAndKillMazeAlgorithm(MazeCell[,] mazeCells) : base(mazeCells) {
	}

	public override void CreateMaze () {
		HuntAndKill ();
	}

	private void HuntAndKill() {
		mazeCells [currentRow, currentColumn].visited = true;

		while (! courseComplete) {
			Kill(); // Will run until it hits a dead end.
			Hunt(); // Finds the next unvisited cell with an adjacent visited cell. If it can't find any, it sets courseComplete to true.
		}
	}

	private void Kill() {
		while (RouteStillAvailable (currentRow, currentColumn)) {
			 int direction = Random.Range (1, 5);
			//int direction = ProceduralNumberGenerator.GetNextNumber ();		// 1 - 4

			if (direction == 1 && CellIsAvailable (currentRow - 1, currentColumn)) {
				// North
				DestroyNorthWallIfPossible (mazeCells [currentRow, currentColumn]);
				DestroySouthWallIfPossible (mazeCells [currentRow - 1, currentColumn]);

				//mazeCells [currentRow, currentColumn].isWallNorth = false;
				//mazeCells [currentRow - 1, currentColumn].isWallSouth = false;

				currentRow--;
			} else if (direction == 2 && CellIsAvailable (currentRow + 1, currentColumn)) {
				// South
				DestroySouthWallIfPossible (mazeCells [currentRow, currentColumn]);
				DestroyNorthWallIfPossible (mazeCells [currentRow + 1, currentColumn]);

				//mazeCells [currentRow, currentColumn].isWallSouth = false;
				//mazeCells [currentRow + 1, currentColumn].isWallNorth = false;

				currentRow++;
			} else if (direction == 3 && CellIsAvailable (currentRow, currentColumn + 1)) {
				// east
				DestroyEastWallIfPossible (mazeCells [currentRow, currentColumn]);
				DestroyWestWallIfPossible (mazeCells [currentRow, currentColumn + 1]);

				//mazeCells [currentRow, currentColumn].isWallEast = false;
				//mazeCells [currentRow, currentColumn + 1].isWallWest = false;

				currentColumn++;
			} else if (direction == 4 && CellIsAvailable (currentRow, currentColumn - 1)) {
				// west
				DestroyWestWallIfPossible (mazeCells [currentRow, currentColumn]);
				DestroyEastWallIfPossible (mazeCells [currentRow, currentColumn - 1]);

				//mazeCells [currentRow, currentColumn].isWallWest = false;
				//mazeCells [currentRow, currentColumn - 1].isWallEast = false;

				currentColumn--;
			}
				
			mazeCells [currentRow, currentColumn].visited = true;
		}
	}


	private void Hunt() {
		courseComplete = true; // Set it to this, and see if we can prove otherwise below!

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				if (!mazeCells [r, c].visited && CellHasAnAdjacentVisitedCell(r,c)) {
					courseComplete = false; // Yep, we found something so definitely do another Kill cycle.
					currentRow = r;
					currentColumn = c;
					DestroyAdjacentWall (currentRow, currentColumn);
					mazeCells [currentRow, currentColumn].visited = true;
					return; // Exit the function
				}
			}
		}
	}


	private bool RouteStillAvailable(int row, int column) {
		int availableRoutes = 0;

		if (row > 0 && !mazeCells[row-1,column].visited) {
			availableRoutes++;
		}

		if (row < mazeRows - 1 && !mazeCells [row + 1, column].visited) {
			availableRoutes++;
		}

		if (column > 0 && !mazeCells[row,column-1].visited) {
			availableRoutes++;
		}

		if (column < mazeColumns-1 && !mazeCells[row,column+1].visited) {
			availableRoutes++;
		}

		return availableRoutes > 0;
	}

	private bool CellIsAvailable(int row, int column) {
		if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells [row, column].visited) {
			return true;
		} else {
			return false;
		}
	}

//	private void DestroyWallIfItExists(GameObject wall) {
//		if ( (wall != null) && (!wall.Equals(null)) ) {
//			GameObject.Destroy (wall);
//		}
//	}

	private void DestroyNorthWallIfPossible(MazeCell cell) {
		GameObject wall = cell.northWall;

		if ( (wall != null) && (!wall.Equals(null)) ) {
			GameObject.Destroy (wall);
			cell.northWall = null;
		}
	}

	private void DestroyEastWallIfPossible(MazeCell cell) {
		GameObject wall = cell.eastWall;

		if ( (wall != null) && (!wall.Equals(null)) ) {
			GameObject.Destroy (wall);
			cell.eastWall = null;
		}
	}

	private void DestroySouthWallIfPossible(MazeCell cell) {
		GameObject wall = cell.southWall;

		if ( (wall != null) && (!wall.Equals(null)) ) {
			GameObject.Destroy (wall);
			cell.southWall = null;
		}
	}

	private void DestroyWestWallIfPossible(MazeCell cell) {
		GameObject wall = cell.westWall;

		if ( (wall != null) && (!wall.Equals(null)) ) {
			GameObject.Destroy (wall);
			cell.westWall = null;
		}
	}

	private bool CellHasAnAdjacentVisitedCell(int row, int column) {
		int visitedCells = 0;

		// Look 1 row up (north) if we're on row 1 or greater
		if (row > 0 && mazeCells [row - 1, column].visited) {
			visitedCells++;
		}

		// Look one row down (south) if we're the second-to-last row (or less)
		if (row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
			visitedCells++;
		}

		// Look one row left (west) if we're column 1 or greater
		if (column > 0 && mazeCells [row, column - 1].visited) {
			visitedCells++;
		}

		// Look one row right (east) if we're the second-to-last column (or less)
		if (column < (mazeColumns-2) && mazeCells [row, column + 1].visited) {
			visitedCells++;
		}

		// return true if there are any adjacent visited cells to this one
		return visitedCells > 0;
	}

	private void DestroyAdjacentWall(int row, int column) {
		bool wallDestroyed = false;

		while (!wallDestroyed) {
			 int direction = Random.Range (1, 5);
			//int direction = ProceduralNumberGenerator.GetNextNumber ();		// 1 - 4

			if (direction == 1 && row > 0 && mazeCells [row - 1, column].visited) {
				// North
				DestroyNorthWallIfPossible (mazeCells [row, column]);
				DestroySouthWallIfPossible (mazeCells [row - 1, column]);
				wallDestroyed = true;

				//mazeCells [row, column].isWallNorth = false;
				//mazeCells [row - 1, column].isWallSouth = false;

			} else if (direction == 2 && row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
				// South
				DestroySouthWallIfPossible (mazeCells [row, column]);
				DestroyNorthWallIfPossible (mazeCells [row + 1, column]);
				wallDestroyed = true;

				//mazeCells [row, column].isWallSouth = false;
				//mazeCells [row + 1, column].isWallNorth = false;

			} else if (direction == 3 && column > 0 && mazeCells [row, column-1].visited) {
				// East
				DestroyWestWallIfPossible (mazeCells [row, column]);
				DestroyEastWallIfPossible (mazeCells [row, column-1]);
				wallDestroyed = true;

				//mazeCells [row, column].isWallWest = false;
				//mazeCells [row, column - 1].isWallEast = false;

			} else if (direction == 4 && column < (mazeColumns-2) && mazeCells [row, column+1].visited) {
				// West
				DestroyEastWallIfPossible (mazeCells [row, column]);
				DestroyWestWallIfPossible (mazeCells [row, column+1]);
				wallDestroyed = true;

				//mazeCells [row, column].isWallEast = false;
				//mazeCells [row, column + 1].isWallWest = false;

			}
		}
	}
}
