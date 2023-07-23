using UnityEngine;

public class Grid<TGridObject>
{
    //parameters of the grid
    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;

    /// <summary>
    /// 
    /// note that the origin of a cell is still in the left down corner,
    /// this variable offsets the original origin by x,y,z ammount
    /// 
    /// </summary>
    private Vector3 originPosition;

    //the constructor
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        //asign the values to the Grid object
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        //to have the values remembered somewhere
        gridArray = new TGridObject[width, height];

        //drawing the grid on the screen
        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // draw the grid
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                // initialize the original value of the grid's cell with 0;
                Debug.Log(gridArray[x, y]);
            }

        //Close the grid lines
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    /// <summary>
    /// 
    /// functions that geets the coordonates of the points in the grid
    /// and fits them according to the size of the cell
    /// originPosition fits the grid so that the orgin of cell 0,0 is wherever we want
    /// 
    /// </summary>
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    #region Code that handles the set and get values of the grid

    //Code that modifys the value that a grid cell has
    public void SetValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    /// <summary>
    /// 
    ///     GetXY()
    ///         - this function rounds the floats to ints so that 
    ///           a position of x=1,5 and y=2,9 is considered to be cell (1,2)
    ///           
    /// </summary>
    private void GetXY(Vector3 worldposition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldposition.x - originPosition.x) / cellSize);
        y = Mathf.FloorToInt((worldposition.y - originPosition.y) / cellSize);
    }

    //sets the value of cell x,y to be a specific value :))
    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    //gets the value of grid cell
    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return gridArray[x, y];
        }
        return default(TGridObject); 
    }

    // returns the value of the grid cell we had pressed on
    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    #endregion
}
