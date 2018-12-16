using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {

	public Text textRow;
	public Text textCol;
	public Text start;
	public Text end;

	private int rows;
	private int cols;

	private int solverButtonCount = 0;

	void Start() {
		textRow.text = MazeLoader.reference.mazeRows.ToString();
		textCol.text = MazeLoader.reference.mazeColumns.ToString();
		start.text = "Start: (" + MazeLoader.reference.startRows.ToString() + "," + MazeLoader.reference.startColumns.ToString() + ")";
		end.text = "End: (" + MazeLoader.reference.destRows.ToString() + "," + MazeLoader.reference.destColumns.ToString() + ")";
		rows = MazeLoader.reference.mazeRows;
		cols = MazeLoader.reference.mazeColumns;
	}

	public void DecreaseRowsButton() {
		if (rows > 1) {
			rows--;
			textRow.text = rows.ToString();
		}
		if (rows == 0) {
			rows = 0;
			textRow.text = "1";
		}
	}

	public void IncreaseRowsButton() {
		if (rows < 80) {
			rows++;
			textRow.text = rows.ToString();
		}
	}

	public void DecreaseColsButton() {
		if (cols > 1) {
			cols--;
			textCol.text = cols.ToString();
		}
		if (cols == 0) {
			cols = 0;
			textCol.text = "1";
		}

	}

	public void IncreaseColsButton() {
		if (cols < 80) {
			cols++;
			textCol.text = cols.ToString();
		}
	}

	public void DecreaseRowsStartButton() {
		if (MazeLoader.reference.startRows > 1) {
			MazeLoader.reference.startRows--;
			start.text = "Start: (" + MazeLoader.reference.startRows.ToString() + "," + MazeLoader.reference.startColumns.ToString() + ")";
		}
		if (MazeLoader.reference.startRows == 0) {
			MazeLoader.reference.startRows = 0;
			start.text = "Start: (1," + MazeLoader.reference.startColumns.ToString() + ")";
		}

	}

	public void DecreaseColsStartButton() {
		if (MazeLoader.reference.startColumns > 1) {
			MazeLoader.reference.startColumns--;
			start.text = "Start: (" + MazeLoader.reference.startRows.ToString () + "," + MazeLoader.reference.startColumns.ToString () + ")";
		}
		if (MazeLoader.reference.startColumns == 0) {
			MazeLoader.reference.startColumns = 0;
			start.text = "Start: (" + MazeLoader.reference.startRows.ToString() + ",1)";
		}
	}

	public void IncreaseRowsStartButton() {
		if (MazeLoader.reference.startColumns < 80) {
			MazeLoader.reference.startColumns++;
			start.text = "Start: (" + MazeLoader.reference.startRows.ToString () + "," + MazeLoader.reference.startColumns.ToString () + ")";
		}
	}

	public void IncreaseColsStartButton() {
		if (MazeLoader.reference.startRows < 80) {
			MazeLoader.reference.startRows++;
			start.text = "Start: (" + MazeLoader.reference.startRows.ToString () + "," + MazeLoader.reference.startColumns.ToString () + ")";
		}

	}

	public void DecreaseRowsEndButton() {
		if (MazeLoader.reference.destRows > 1) {
			MazeLoader.reference.destRows--;
			end.text = "End: (" + MazeLoader.reference.destRows.ToString() + "," + MazeLoader.reference.destColumns.ToString() + ")";
		}
		if (MazeLoader.reference.destRows == 0) {
			MazeLoader.reference.destRows = 0;
			end.text = "End: (1," + MazeLoader.reference.startColumns.ToString() + ")";
		}

	}

	public void DecreaseColsEndButton() {
		if (MazeLoader.reference.destColumns > 1) {
			MazeLoader.reference.destColumns--;
			end.text = "End: (" + MazeLoader.reference.destRows.ToString() + "," + MazeLoader.reference.destColumns.ToString() + ")";
		}
		if (MazeLoader.reference.destColumns == 0) {
			MazeLoader.reference.destColumns = 0;
			end.text = "End: (" + MazeLoader.reference.startRows.ToString() + ",1)";
		}

	}

	public void IncreaseRowsEndButton() {
		if (MazeLoader.reference.destRows < 80) {
			MazeLoader.reference.destRows++;
			end.text = "End: (" + MazeLoader.reference.destRows.ToString () + "," + MazeLoader.reference.destColumns.ToString () + ")";
		}
	}

	public void IncreaseColsEndButton() {
		if (MazeLoader.reference.destColumns < 80) {
			MazeLoader.reference.destColumns++;
			end.text = "End: (" + MazeLoader.reference.destRows.ToString () + "," + MazeLoader.reference.destColumns.ToString () + ")";
		}

	}

	public void ResetButton() {
		if (solverButtonCount == 1) {
			solverButtonCount--;
		}
		MazeLoader.reference.DestoryMaze ();
		MazeLoader.reference.DestoryVisualPath (MazeLoader.reference.depth);

		MazeLoader.reference.mazeRows = rows;
		MazeLoader.reference.mazeColumns = cols;

		MazeLoader.reference.LoadMaze ();
		MazeLoader.reference.SolveMaze ();
	}

	public void SolveButton() {
		if (solverButtonCount == 0) {
			solverButtonCount++;
			MazeLoader.reference.CreateVisualPath (MazeLoader.reference.depth);

		}
	}
}

