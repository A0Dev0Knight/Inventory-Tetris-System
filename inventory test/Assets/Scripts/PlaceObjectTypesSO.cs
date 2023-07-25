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
}
