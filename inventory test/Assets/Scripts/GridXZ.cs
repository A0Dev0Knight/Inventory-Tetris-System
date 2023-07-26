using System;
using UnityEngine;

public class GridXZ<TGridObject>
{
    // this is an event that is fired when a value in the grid is changed
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;

    }

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
    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject)
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
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // draw the grid
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);

                // initialize the original value of the grid's cell with a default value;
                gridArray[x, z] = createGridObject(this, x, z);
            }

        //Close the grid lines
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }
    public float GetCellSize()
    {
        return cellSize;
    }
    /// <summary>
    /// 
    /// functions that geets the coordonates of the points in the grid
    /// and fits them according to the size of the cell
    /// originPosition fits the grid so that the orgin of cell 0,0 is wherever we want
    /// 
    /// </summary>
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x,0, z) * cellSize + originPosition;
    }

    #region Code that handles the set and get values of the grid

    //Code that modifys the value that a grid cell has
    public void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && x < width && z >= 0 && z < height)
        {
            gridArray[x, z] = value;

        }
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    /// <summary>
    /// 
    ///     GetXY()
    ///         - this function rounds the floats to ints so that 
    ///           a position of x=1,5 and y=2,9 is considered to be cell (1,2)
    ///           
    /// </summary>
    public void GetXZ(Vector3 worldposition, out int x, out int Z)
    {
        x = Mathf.FloorToInt((worldposition.x - originPosition.x) / cellSize);
        Z = Mathf.FloorToInt((worldposition.z - originPosition.z) / cellSize);
    }

    //sets the value of cell x,y to be a specific value :))
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetGridObject(x, z, value);
    }

    //gets the value of grid cell
    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && x < width && z >= 0 && z < height)
        {
            return gridArray[x, z];
        }
        return default(TGridObject);
    }

    // returns the value of the grid cell we had pressed on
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }

    #endregion
}
