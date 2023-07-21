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

        gridArray = new int[width, height];
        debugArray = new TextMesh[width, height];

        //drawing the grid on the screen
        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Debug.Log(x + " " + y);
               
                // draw the grid
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }

        //Close the grid lines
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    //functions that hets the coordonates of the points in the grid and
    //fits them according to the size of the cell
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    //Code that should modify the text that appears in a grid cell
    private void SetValue(int x, int y, int value)
    {
        if (x >= 0 && x <= width && y >= 0 && y <= height)
        {
            gridArray[x, y] = value;
            debugArray[x, y].text = gridArray[x, y].ToString();
        }


    }
}
