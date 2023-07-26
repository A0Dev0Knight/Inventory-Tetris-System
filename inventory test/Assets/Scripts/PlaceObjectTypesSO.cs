using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlaceObjectTypesSO : ScriptableObject
{
    // this function is used for debug purpouses, we get the next rotation position
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down:      return Dir.Left;
            case Dir.Left:      return Dir.Up;
            case Dir.Up:        return Dir.Right;
            case Dir.Right:     return Dir.Down;
        }
    }

    // an enum that stores the up down left right position names :)
    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }

    #region Variables
    public string Name;
    public Transform prefab;
    public int height;
    public int width;
    #endregion

    // this function rotates the visual for the building
    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down:      return 0;
            case Dir.Left:      return 90;
            case Dir.Up:        return 180;
            case Dir.Right:     return 270;
        }
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return new Vector2Int(0, 0);
            case Dir.Left: return new Vector2Int(0, width);
            case Dir.Up: return new Vector2Int(width, height );
            case Dir.Right: return new Vector2Int(height, 0);

        }
    }
    // this function returns the coordonates that are occupied by the building in the grid
    // the offset is where in the grid we had pressed with the mouse
    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        // this list remembers the (X,Z) coordinates that are occupied by the building
        List<Vector2Int> gridPositionList = new List<Vector2Int>();

        switch (dir){
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < width; x++)
                    for (int z = 0; z < height; z++)
                    {
                        // we add the coordinates that is supposed to be used up by the building in the list
                        gridPositionList.Add(offset + new Vector2Int(x, z));
                    }
                break;

            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < height; x++)
                    for (int z = 0; z < width; z++)
                    {
                        // we add the coordinates that is supposed to be used up by the building in the list
                        gridPositionList.Add(offset + new Vector2Int(x, z));
                    }
                break;
        }
        // we return the list with used cells of the grid
        return gridPositionList;
    }
}
