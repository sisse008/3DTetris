﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TetrisGrid : MonoBehaviour
{
    [SerializeField]
    int height, width, depth;
    [SerializeField]
    Cell cellPrefab;


    //not inhereting from ObjectGrid because I want Grid to be gameobject in scene (no constructor)
    ObjectGrid<Cell> grid;



    public int Height { get { return height; } }
    public int Width { get { return width; } }
    public int Depth { get { return depth; } }

    public void InitGrid()
    {
        grid = new ObjectGrid<Cell>(height, width, depth, cellPrefab);
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                for (int z = 0; z < depth; z++)
                {
                    // position = (x,y,z) = (width, height, depth) = (j,i,z)
                    Cell cell = Instantiate(cellPrefab, new Vector3(j, i, z), Quaternion.identity, transform);
                    cell.name = "( " + i.ToString() + " , " + j.ToString() + " , " + z.ToString() + " )";
                    //IS IT PASSES BY REFERENCE? yes
                    grid.UpdateGridMatrix(i, j, z, cell);
                }

    }

    public bool isWithinGridLimits(Vector3 p)
    {
        return grid.isWithinGrid(p);
    }

    bool isWithinGridLimits(Shape s)
    {
        List<Vector3> globalPositions = s.GetGlobalPositions();
        foreach (Vector3 p in globalPositions)
            if (!grid.isWithinGrid(p))
            {
                //Debug.Log("move = " + p + "  is not safe move");
                return false;
            }

        return true;
    }

    bool isTetrisCollision(Shape s, List<Shape> staticShapes)
    {
        foreach (Shape ss in staticShapes)
        {
            if (CollisionSystem.Collides(s.GetGlobalPositions(), ss.GetGlobalPositions()))
            {
                return true;
            }
        }
        return false;
    }

    public void MoveShape(Shape s, TetrisMoves move, ref bool isSafeMove, List<Shape> staticShapes)
    {
        List<Vector3> oldPositions = s.GetGlobalPositions();
        
        s.Move(move);

        isSafeMove = isWithinGridLimits(s) && !isTetrisCollision(s, staticShapes);

        if (!isSafeMove)
        {
                
            s.MoveToPreviousPosition();
            return;
        }

        List<Vector3> newPositions = s.GetGlobalPositions();
      

        FreeupGridCells(oldPositions);
        OccupyGridCells(newPositions, s.color);
    }

    public Vector3 CenterPosition()
    {
        return new Vector3(Mathf.Round(width / 2), Mathf.Round(height) / 2, Mathf.Round(depth / 2));
    }
    public Vector3 GetUpperCenterCellPosition()
    {
        return new Vector3( Mathf.Round(width / 2), height - 1, Mathf.Round(depth / 2));
    }


    public void FreeupGridCells(List<Vector3> cellPositions)
    {
        foreach (Vector3 pos in cellPositions)
        {

            Cell cell = grid.getCell(pos);
            if(cell != null)
                cell.SetOccupied(false);
        }
    }


   

   public void OccupyGridCells(List<Vector3> cellPositions, Material color)
   {
        foreach (Vector3 pos in cellPositions)
        {
            Cell cell = grid.getCell(pos);
            if(cell != null)
                cell.SetOccupied(true, color);
        }
   }

   
}