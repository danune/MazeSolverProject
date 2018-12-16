using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject ground;
	public Camera cam;
	public float size = 2f;

	public List<int> theWay;
	public List<MazeCell> depth;


	public int startRows;
	public int startColumns;
	public int destRows;
	public int destColumns;


	public GameObject mazeSolver;
	public float speed = 1f;

	private SearchAlgs algorithms = new SearchAlgs ();

	private MazeCell[,] mazeCells;

	public static MazeLoader reference;

	void Awake () {
		LoadMaze ();

	}

	// Use this for initialization
	void Start () {

		reference = this;

	//	CheckMazeNeighbors ();

		//ShowMazeCellWallData ("Check");

		//ShowMazeCellWallBoolData ("Initial Check");


	//	ShowMazeCellWallBoolData ("Adjusted Check");

		SolveMaze ();


		//mazeCells [mazeRows - 1, mazeColumns - 1]


			
		cam.transform.Translate (110, -40, -500);
		GameObject.Find ("MazeCreator").transform.Translate (200,  200, 120);
	}


	void Update () {

	}

	private void ShowMazeCellWallData (string title = "<no title>") {
		print ("[WallCell]----------- " + title + " ----------");

		for (int i = 0; i < mazeRows; i++) {
			for (int j = 0; j < mazeColumns; j++) {
				print (
					CellWallData ("N", mazeCells [i, j].northWall) + "--" +
					CellWallData ("E", mazeCells [i, j].eastWall)  + "--" +
					CellWallData ("S", mazeCells [i, j].southWall) + "--" +
					CellWallData ("W", mazeCells [i, j].westWall)
				);
			}
		}
	}

	private string CellWallData(string wallID, GameObject o) {
		string result = wallID;

		if ( (o == null) || o.Equals(null) ) {
			result = result + ": no wall";
		} else {
			result = result + ": " + o.name;
		}

		return result;
	}


	void ShowMazeCellWallBoolData (string title = "<no title>") {
		print ("[WallBool]----------- " + title + " ----------");

		for (int i = 0; i < mazeRows; i++) {
			for (int j = 0; j < mazeColumns; j++) {
				print (
					"[" + i + ", " + j + "]" +
					CellWallBoolData ("N", mazeCells [i, j].isWallNorth) + " " +
					CellWallBoolData ("E", mazeCells [i, j].isWallEast)  + " " +
					CellWallBoolData ("S", mazeCells [i, j].isWallSouth) + " " +
					CellWallBoolData ("W", mazeCells [i, j].isWallWest)
				);
			}
		}
	}

	string CellWallBoolData(string wallID, bool b) {
		string result = wallID;

		if (b) {
			result = result + "+";
		} else {
			result = result + "-";
		}

		return result;
	}

	private void AdjustWallBoolData() {
		// Assumption: maze edges are solid wall (no breaks in them)

		// Non-Top north wall data is invalid
		// Non-Left west wall data is invalid

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {

				if (r == 0) {
					mazeCells [r, c].isWallNorth = true;
				}

				if (c == 0) {
					mazeCells [r, c].isWallWest = true;
				}

				if (r == mazeRows - 1) {
					mazeCells [r, c].isWallSouth = true;
				}

				if (c == mazeColumns - 1) {
					mazeCells [r, c].isWallEast = true;
				}

				if (ifWallExists(mazeCells [r, c].eastWall)) {
					mazeCells [r, c].isWallEast = true;
				}

				if (ifWallExists(mazeCells [r, c].southWall)) {
					mazeCells [r, c].isWallSouth = true;
				}

				if (r > 0) {
					// Adjust for shared North/South wall

					if (ifWallExists (mazeCells [r - 1, c].southWall)) {
						mazeCells [r, c].isWallNorth = true;
					}
				}
					
				if (c > 0) {
					// Adjust for shared West/East wall

					if (ifWallExists(mazeCells [r, c - 1].eastWall)) {
						mazeCells [r, c].isWallWest = true;
					}
				}
			}
		}
	}

	private bool ifWallExists(GameObject wall) {
		if ( (wall != null) && !wall.Equals(null) ) {
			return true;
		} else {
			return false;
		}
	}

	public void CreateVisualPath(List<MazeCell> path) {

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {

				for (int i = 0; i < path.Count; i++) {
					if (mazeCells [r, c] == path [i]) {
						
						mazeCells [r, c] .floor = Instantiate (ground, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
						mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
						mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);
					}

				}

			}
		}
	}

	private IEnumerator delayFunc(float delay) {
		yield return new WaitForSeconds (delay);
	}
		
		
	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
		//		mazeCells [r, c] .floor = Instantiate (wall, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity) as GameObject;
		//		mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
		//		mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;

					//mazeCells [r, c].isWallWest = true;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				//mazeCells [r, c].isWallEast = true;

//				if (c > 0) {
//					if (mazeCells [r, c - 1].isWallEast) {
//						mazeCells [r, c].isWallWest = true;
//					}
//				}

				if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);

					//mazeCells [r, c].isWallNorth = true;
				}

				mazeCells [r, c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);

				//mazeCells [r, c].isWallSouth = true;

//				if (r > 0) {
//					if (mazeCells [r - 1, c].isWallSouth) {
//						mazeCells [r, c].isWallNorth = true;
//					}
//				}
			}
		}

	
		 

		//ShowMazeCellWallBoolData ("initialize");
		//ShowMazeCellWallData ("initialize");
	}

	public void LoadMaze() {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

		ma.ResetVisitedValues ();

	}

	public void SolveMaze() {
		if (destRows > mazeRows) {
			destRows = mazeRows;
		}
		if (destRows <= 0) {
			destRows = 1;
		}
		if (destColumns > mazeColumns) {
			destColumns = mazeColumns;
		}
		if (destColumns <= 0) {
			destColumns = 1;
		}



		if (startRows <= 0) {
			startRows = 0;
		}
		if (startRows >= mazeRows) {
			startRows = mazeRows;
		}
		if (startColumns <= 0) {
			startColumns = 0;
		}
		if (startColumns >= mazeColumns) {
			startColumns = mazeColumns;
		}

		AdjustWallBoolData ();
		algorithms.SetMazeForAlg (mazeCells);
		depth = algorithms.DepthFirstSearch (mazeCells [startRows - 1, startColumns - 1], mazeCells[destRows - 1, destColumns - 1]);
		theWay = algorithms.TranslateAlgPath(depth);

		for (int i = 0; i < theWay.Count; i++) {
			print (theWay [i]);
		}


		for (int i = 0; i < depth.Count; i++) {
			print (depth [i].row + "," + depth[i].col);
		}
	}

	public void DestoryMaze() {
		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				if (c == 0) {
					if ((mazeCells [r, c].westWall != null) && (!mazeCells [r, c].westWall.Equals (null))) {
						mazeCells [r, c].westWall = null;
						GameObject.Destroy (GameObject.Find("West Wall " + r + "," + c));
					}
				}
				if ((mazeCells [r, c].eastWall != null) && (!mazeCells [r, c].eastWall.Equals (null))) {
					mazeCells [r, c].eastWall = null;
					GameObject.Destroy (GameObject.Find("East Wall " + r + "," + c));
				}

				if (r == 0) {
					if ((mazeCells [r, c].northWall != null) && (!mazeCells [r, c].northWall.Equals (null))) {
						mazeCells [r, c].northWall = null;
						GameObject.Destroy (GameObject.Find("North Wall " + r + "," + c));
					}
				}
				if ((mazeCells [r, c].southWall != null) && (!mazeCells [r, c].southWall.Equals (null))) {
					mazeCells [r, c].southWall = null;
					GameObject.Destroy (GameObject.Find("South Wall " + r + "," + c));
				}
			}
		}
	}

	public void DestoryVisualPath(List<MazeCell> path) {
		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {

				for (int i = 0; i < path.Count; i++) {
					if (mazeCells [r, c] == path [i]) {
						if ((mazeCells [r, c].floor != null) && (!mazeCells [r, c].floor.Equals (null))) {
							mazeCells [r, c].floor = null;
							GameObject.Destroy (GameObject.Find("Floor " + r + "," + c));
						}
					}

				}

			}
		}

	}
}
