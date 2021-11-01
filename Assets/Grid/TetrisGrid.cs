using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TetrisGrid : MonoBehaviour
{
    [SerializeField]
    int gridHeight, gridWidth, gridDepth;
    [SerializeField]
    Cell cellPrefab;
    [SerializeField]
    float cellHeight, cellWidth, cellDepth;


    //not inhereting from ObjectGrid because I want Grid to be gameobject in scene (no constructor)
    ObjectGrid<Cell> grid;

    void Awake()
    {
        if (cellPrefab)
        {
            cellHeight = cellPrefab.transform.localScale.y;
            cellWidth = cellPrefab.transform.localScale.x;
            cellDepth = cellPrefab.transform.localScale.z;
        }
    }

    public void InitGrid()
    {
        grid = new ObjectGrid<Cell>(gridHeight, gridWidth, gridDepth, cellPrefab);
        for (int i = 0; i < gridHeight; i++)
            for (int j = 0; j < gridWidth; j++)
                for (int z = 0; z < gridDepth; z++)
                {
                    Cell cell = Instantiate(cellPrefab, new Vector3(j*cellWidth, i*cellHeight, z*cellDepth) + transform.position, Quaternion.identity, transform);
                    cell.name = "( " + i.ToString() + " , " + j.ToString() + " , " + z.ToString() + " )";
                    grid.UpdateGridMatrix(i, j, z, cell);
                }
    }

    public bool IsWithinGridLimits(Vector3 p)
    {
        return grid.IsWithinGrid(p);
    }

    bool IsWithinGridLimits(Shape s)
    {
        List<Vector3> globalPositions = s.GetGlobalCellPositions();
        foreach (Vector3 p in globalPositions)
            if (!grid.IsWithinGrid(p))
            {
                //Debug.Log("move = " + p + "  is not safe move");
                return false;
            }
        return true;
    }

    bool IsTetrisCollision(Shape s, List<Shape> staticShapes)
    {
        foreach (Shape ss in staticShapes)
        {
            if (CollisionSystem.Collides(s.GetGlobalCellPositions(), ss.GetGlobalCellPositions()))
            {
                return true;
            }
        }
        return false;
    }

    public bool MoveShape(Shape s, TetrisMoves move, List<Shape> staticShapes, bool rotate = false)
    {
        List<Vector3> oldPositions = s.GetGlobalCellPositions();

        if (!rotate)
            s.UpdatePosition(move);
        else
            s.UpdateRotation(move);

        bool isSafeMove = IsWithinGridLimits(s) && !IsTetrisCollision(s, staticShapes);

        if (!isSafeMove)
        {
            if (!rotate)
                s.MoveToPreviousPosition();
            else
                s.MoveToPreviousRotation();
            return false;
        }
        List<Vector3> newPositions = s.GetGlobalCellPositions();
        FreeupGridCells(oldPositions);
        OccupyGridCells(newPositions, s.color);
        return true;
    }

    public Vector3 CenterPosition()
    {
        return new Vector3(Mathf.Round(gridWidth / 2), Mathf.Round(gridHeight) / 2, Mathf.Round(gridDepth / 2));
    }

    public Vector3 GetUpperCenterCellPosition()
    {
        return new Vector3( Mathf.Round(gridWidth / 2), gridHeight - 1, Mathf.Round(gridDepth / 2));
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
