using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]
    Transform DebugSphere;

    Vector3 wordlPoseOfMouse;

    private Grid grid;
    private void Start()
    {
        grid = new Grid(3, 2, 1, new Vector3(-1,-1,-1)/2);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            grid.SetValue(GetMousePos3D(), 100);
            //magic code that thets the position of the cursor in the world
            //use a 2d method for a 2d game or a 3d method for a 3d game
            Debug.Log(GetMousePos3D());
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
}
