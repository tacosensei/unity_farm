using UnityEngine;

public class PlotGroup {
    public const int DIMENSION = 5; // must be odd!
    public GameObject plotPrefab;
    public GameObject[,] grid = new GameObject[DIMENSION,DIMENSION];
    private int groupID;
    private Vector3 position;
    public PlotGroup(Vector3 pos, int id) {
        groupID = id;
        position = pos;
    }

    public GameObject[,] Initialize(GameObject plot) {
        // add plot to center of our grid
        plot.GetComponent<Plot>().groupID = groupID;
        grid[DIMENSION/2,DIMENSION/2] = plot;
        Debug.Log("Created Group: "+ groupID);
        return grid;
    }

    public void AddPlot(GameObject plot, GameObject parent, string adjacency) {
        Coordinates location = FindPlot(parent);
		switch(adjacency) {
			case "up":
				location.y++;
                break;
			case "down":
				location.y--;
                break;
			case "right":
				location.x++;
                break;
			case "left":
				location.x--;
                break;
			default:
                // fail silently
                return;
		}
        plot.GetComponent<Plot>().groupID = groupID;
        grid[location.x,location.y] = plot;
        Debug.Log("Group: " + groupID + " Location: (" + location.x + ", " + location.y + ")");
    }

    private Coordinates FindPlot(GameObject obj) {
        for(int i = 0; i < DIMENSION; ++i) {
            for(int j = 0; j < DIMENSION; ++j) {
                if(GameObject.ReferenceEquals(grid[i,j], obj)) {
                    return new Coordinates(i,j);
                }
            }
        }
        return new Coordinates(-1, -1);
    }

}

struct Coordinates{
    public int x;
    public int y;
    public Coordinates(int i, int j){
        this.x = i;
        this.y = j;
    }
}