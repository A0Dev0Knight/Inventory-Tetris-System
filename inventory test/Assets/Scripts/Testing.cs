using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    Transform DebugSphere;

    [SerializeField]
    Transform GridBackground;

    [SerializeField]
    int ZMin = -10;

    [SerializeField]
    int ZMax = 10;

    Vector3 wordlPoseOfMouse;

    private Grid<TestingClass> grid;
    private void Start()
    {
        int width    = 5;
        int height   = 5;
        int cellSize = 1;

        grid = new Grid<TestingClass>(width, height, cellSize, Vector3.zero, (Grid<TestingClass> g, int x, int y) => new TestingClass(g,x,y));

        #region Background visual

        GridBackground.Rotate(new Vector3(-90,0,0));
        GridBackground.localScale= new Vector3(width*.1f,1,height*.1f)/cellSize;
        GridBackground.position = new Vector3(width * .5f, height * .5f);

        #endregion
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //if the initil value os null we add a value
            TestingClass test = grid.GetGridObject(GetMousePos3D());

            if (test != null)
            {
                test.AddValue(45);
            }
            //grid.SetGridObject(GetMousePos3D(), null);
        }
        else if (Input.GetMouseButton(1))
        {
             grid.GetGridObject(GetMousePos3D());
        }
    }

    private Vector3 GetMousePos3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit other))
        {
             wordlPoseOfMouse = other.point;
            DebugSphere.position = wordlPoseOfMouse;
        }

        return wordlPoseOfMouse;
    }

    #region 2D get the cursor postin on screen
    /* GetworldPosition() 2D method
    //Boilerplated code or whatever the name is that gets the position of the mouse in relation
    //to the 3d space we have in our game

    //somehow these lines of code only return (0,1) for the pointer position
    //i cannot find for now the issue
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMouseWorldPositionWithNullZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }
    public Vector3 GetMouseWorldPositionWithNullZ(Vector2 screenPosition, Camera worldCamera)
    {
        Vector3 worldposition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldposition;
    }
    */
    #endregion
}

//this is a class used for testing the generic class Grid
public class TestingClass
{
    private Grid<TestingClass> grid;
    private int value;
    private int x, y;
    public TestingClass(Grid<TestingClass> grid, int x, int y)
    {
        this.grid = grid;
        this.x    = x;
        this.y    = y;
    }
    public void AddValue(int addvalue)
    {
        value += addvalue;
        grid.TriggerGridObjectChanged(x,y);
    }
}