using UnityEngine;

public class MazeCell {
	public bool visited = false;
	public GameObject northWall, southWall, eastWall, westWall, floor;
	public int row = 0;
	public int col = 0;
	public bool isWallNorth = false, isWallSouth = false, isWallEast = false, isWallWest = false;
}
