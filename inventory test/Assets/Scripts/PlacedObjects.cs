using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour
{
    public static PlacedObjects Create(Vector3 worldPosition, Vector2Int origin, PlaceObjectTypesSO.Dir dir, PlaceObjectTypesSO placeObjectTypesSO)
    {
        Transform placeObjectTransform = Instantiate(placeObjectTypesSO.prefab, worldPosition, Quaternion.Euler(0, placeObjectTypesSO.GetRotationAngle(dir), 0));

        PlacedObjects placedObject = placeObjectTransform.GetComponent<PlacedObjects>();
        placedObject.placedObjectTypeSO = placeObjectTypesSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }
    private PlaceObjectTypesSO placedObjectTypeSO;
    private Vector2Int origin;
    private PlaceObjectTypesSO.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
       return placedObjectTypeSO.GetGridPositionList(origin, dir);

    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
