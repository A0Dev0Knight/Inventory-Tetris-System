using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlaceObjectTypesSO : ScriptableObject
{
    public string Name;
    public Transform prefab;
    public int sizeX;
    public int sizeZ;

    // this function returns the coordonates that are occupied by the building in the grid
    // the offset is where in the grid we had pressed with the mouse
    public List<Vector2Int> GetGridPositionList(Vector2Int offset)
    {
        // this list remembers the (X,Z) coordinates that are occupied by the building
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        for(int x = 0; x < sizeX; x++)
            for(int z = 0; z < sizeZ; z++)
            {
                // we add the coordinates that is supposed to be used up by the building in the list
                gridPositionList.Add(offset + new Vector2Int(x, z));
            }

        // we return the list with used cells of the grid
        return gridPositionList;
    }
}
