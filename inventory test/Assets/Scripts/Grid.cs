using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugArray;

    //the constructor
    public Grid(int width, int height, float cellSize)
    {
        //asign the values to the Grid object
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        //to have the values remembered somewhere
        gridArray = new int[width, height];

        //to have a visual text to be dispalyed on the screen for that value
        //this 2d array only handles the visual part, which is not yet implemeted
        debugArray = new TextMesh[width, height];

        //drawing the grid on the screen
        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
                gridArray[x, y] = 0;
                //debugArray[x, y].text = gridArray[x, y].ToString();
                // draw the grid
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                //Debug.Log(x + " " + y + " " + gridArray[x, y]);
            }

        //Close the grid lines
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        SetValue(1, 1, 50);

        //Debug.Log("AAAAAAAAAAAAAAAA " +gridArray[1,1]);
    }

    //functions that hets the coordonates of the points in the grid and
    //fits them according to the size of the cell
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }


    //Code that should modify the text that appears in a grid cell
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            gridArray[x, y] = value;
            //debugArray[x, y].text = gridArray[x, y].ToString();
        }


    }

    //these two functions let ws press with the mouse on the grid and update the value of the grid
    //press in a square, poz of cursor gets rounded to an int and so the positon of the mouse is detected
    //to be inside a square of a grid
    private void GetXY(Vector3 worldposition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldposition.x / cellSize);
        y = Mathf.FloorToInt(worldposition.y / cellSize);
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
        //Debug.Log("New value at " +x +" , "+y +" is "+ gridArray[x, y]);

    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return gridArray[x, y];
        }
        return -1;
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
