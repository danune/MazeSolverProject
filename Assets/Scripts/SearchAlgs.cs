using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SearchAlgs {

	MazeCell[,] givenMaze;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	//Always starts at 0,0
	//checks each direction, if wall is present in specific square, return false. else return true
	//order NORTH, EAST, SOUTH, WEST


	public void SetMazeForAlg(MazeCell[,] theMazeCells) {
		givenMaze = theMazeCells;
	}


	private List<MazeCell> getChildrenOfCell(MazeCell cell) {
		List<MazeCell> children = new List<MazeCell> ();

		int currentRow = cell.row;
		int currentCol = cell.col;


		if (cell.isWallNorth == false && givenMaze [currentRow - 1, currentCol].visited == false) {
			children.Add (givenMaze [currentRow - 1, currentCol]);
//			givenMaze [currentRow - 1, currentCol].visited = true;
		}

		if (cell.isWallSouth == false && givenMaze [currentRow + 1, currentCol].visited == false) {
			children.Add (givenMaze [currentRow + 1, currentCol]);
//			givenMaze [currentRow + 1, currentCol].visited = true;
		}

		if (cell.isWallWest == false && givenMaze [currentRow, currentCol - 1].visited == false) {
			children.Add (givenMaze [currentRow, currentCol - 1]);
//			givenMaze [currentRow, currentCol - 1].visited = true;
		}

		if (cell.isWallEast == false && givenMaze [currentRow, currentCol + 1].visited == false) {
			children.Add (givenMaze [currentRow, currentCol + 1]);
//			givenMaze [currentRow, currentCol + 1].visited = true;
		}

		return children;
	}
		


	public List<MazeCell> DepthFirstSearch(MazeCell start, MazeCell goal) {
		//List<MazeCell> bestPath = new List<MazeCell>();
		//List<MazeCell> lastPath = new List<MazeCell>();

		List<MazeCell> path = new List<MazeCell>();


		Stack<MazeCell> stack = new Stack<MazeCell> ();
		List<MazeCell> children;

		stack.Push (start);

		start.visited = true;

		while (stack.Count > 0) {
			MazeCell temp = stack.Peek();
			path.Add (temp);

			temp.visited = true;


			stack.Pop();


			if (temp == goal) {
				return path;

			}


			children = getChildrenOfCell (temp);
			children.Reverse ();

			while (children.Count == 0) {
				if (path.Count > 1) {
					path.Remove (temp);
					temp = path [path.Count - 1];
					children = getChildrenOfCell (temp);

				}
			} 

			for (int i = 0; i < children.Count; ++i) {
				stack.Push (children [i]);
			}
		}
		return path;

	}


	public List<int> TranslateAlgPath(List<MazeCell> path) {

		List<int> directions = new List<int>();


		//NORTH = 1
		//EAST = 2
		//SOUTH = 3
		//WEST = 4

		for (int i = 0; i < path.Count - 1; i++) {

			if (path [i].row > path [i + 1].row) {
				//NORTH MOVEMENT
				directions.Add(1);
			}
			else if (path [i].row < path [i + 1].row) {
				//SOUTH MOVEMENT
				directions.Add(3);
			}
			else if (path [i].col > path [i + 1].col) {
				//WEST MOVEMENT
				directions.Add(4);
			}
			else if (path [i].col < path [i + 1].col) {
				//EAST MOVEMENT
				directions.Add(2);
			}
		}

		return directions;

	}



	//Shortest Path Algorithm
	//Algorithm that always moves following right wall, rotates clockwise
	//User Entered Algorithm??





}
