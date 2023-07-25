using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    // reference to building prefab
    [SerializeField] PlaceObjectTypesSO BuildingToBePlaced;

    // initialize the grid object that stores GridObject things
    private GridXZ<GridObject> grid;

    //give the grid that was mentioned up a width a height and a cell size
    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float gridCellsize = 10;

        /// <summary>
        /// 
        /// The grrid gets constructed with:
        ///         - a width
        ///         - a height
        ///         - a cell size
        ///         - an original (position which is 0 for now)
        ///         - defaults for the grid (i have not understood the part with Func entirely)
        /// </summary>
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, gridCellsize, Vector3.zero,
               (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x,z));
    }

    // this is the class GridObject that handles the visual part of the system AKA the buildings
    public class GridObject
    {
        // we get a grid
        private GridXZ<GridObject> grid;
        
        // we get the coordonates
        private int x, z;
        
        // we get the transform AKA the building that is set on the (X,Z) coordinates
        private Transform transform;

        // we construct the GridObject object
        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x    = x;
            this.z    = z;
        }

        // we set the value of the transform of this object to the instance of the building prefab
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        // this function clears the GridObject i think, this part i have not yet understood
        public void ClearTransform()
        {
            transform = null;
        }
        
        // check if we can build on the respective cell
        public bool CanBuild()
        {
            // if condition below is true we can build else we cannot build
            return transform == null;
        }
    }

    private void Update()
    {
        // check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            // translate the mouse position to in-game-3D-world position
            int x, z;
            grid.GetXZ(GetMousePos3D(), out x, out z);

            List<Vector2Int> gridPositionList = BuildingToBePlaced.GetGridPositionList(new Vector2Int(x, z));
            // an empty GridObject that stores the GridObject that is present in the grid at (X,Z)
            GridObject gridObject = grid.GetGridObject(x, z);

            if (gridObject.CanBuild())
            {
                /// <summary>
                /// if we can build (AKA nothing is there already) we spawn the building where it needs to be
                /// and we notify the Grid class that a change was made
                /// 
                /// NOTE: we instantiate only once the prefab!
                /// </summary>
                Transform buildTransform = Instantiate(BuildingToBePlaced.prefab, grid.GetWorldPosition(x, z), Quaternion.identity);
                
                // for every single X and Z in the gridPosition list we set the GridObject value in our grid as being in use
                foreach (Vector2Int gridPosition in gridPositionList)
                { 
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(buildTransform);
                }
            }
            else Debug.Log("You can not build here!"); //else we display a message
        }
    }

    #region This part of code is responsible for getting the mouse position in 3D space
    Vector3 wordlPoseOfMouse;

    private Vector3 GetMousePos3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit other))
        {
            wordlPoseOfMouse = other.point;
        }
        return wordlPoseOfMouse;
    }
    #endregion

}

