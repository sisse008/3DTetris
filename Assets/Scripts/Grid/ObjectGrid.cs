using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;



//abstract?
public class ObjectGrid<T> where T : MonoBehaviour 
{
    public int width { get; private set; }
    protected int height { get; private set; }
    protected int depth { get; private set; }

    T[,,] gridMatrix;



    public ObjectGrid(int height, int width, int depth, T cell)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;

        gridMatrix = new T[height, width, depth];     
    }

    public void UpdateGridMatrix(int h, int w, int d, T cell)
    {
        gridMatrix[h, w, d] = cell;
    }

    public T getCell(int h, int w, int d)
    {
        return gridMatrix[h,w,d];
    }

    public bool IsWithinGrid(Vector3 p)
    {
        if ((p.y < 0) || (p.y >= height))
            return false;
        if ((p.x < 0) || (p.x >= width))
            return false;
        if ((p.z < 0) || (p.z >= depth))
            return false;

        return true;
    }

    public T getCell(Vector3 p)
    {
        if (!IsWithinGrid(p))
            return null;
        
        return gridMatrix[(int)p.y, (int)p.x, (int)p.z];
    }

}
