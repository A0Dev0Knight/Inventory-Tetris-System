﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] Transform TestTransform;

    private GridXZ<GridObject> grid;
    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float gridCellsize = 10;

        grid = new GridXZ<GridObject>(gridWidth, gridHeight, gridCellsize, Vector3.zero,
               (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x,z));
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x, z;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x    = x;
            this.z    = z;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x, z;
            grid.GetXZ(GetMousePos3D(), out x, out z);
            Instantiate(TestTransform, grid.GetWorldPosition(x,z), Quaternion.identity);
        }
    }

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

}

