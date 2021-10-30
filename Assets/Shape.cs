using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShapeDescriptor
{
    public List<Vector3> positions { get; private set; }
    public Vector3 pivot { get; private set; }

    public ShapeDescriptor(List<Vector3> positions, Vector3 pivot)
    {
        this.positions = positions;
        this.pivot = pivot;
    }

    public ShapeDescriptor Clone()
    {
        List<Vector3> _positions = Extentions.Clone(positions);
        Vector3 _pivot = new Vector3(pivot.x, pivot.y, pivot.z);
        return new ShapeDescriptor(_positions,_pivot);
    }
}
public class Shape
{
    //change this for rotation
    protected List<Vector3> shapePositions = new List<Vector3>();

    //change this to affect position. pivot cell position with respect to Grid cells
    protected Vector3 globalPivot;

    //save old positions to undo moves (positions and rotations of shape)
    protected Vector3 previousGlobalPivot;
    protected List<Vector3> previousShapePositions = new List<Vector3>();

    //does not change. local cell position with respect to other cells in same shape
    protected Vector3 localPivot;
   
    public Material color { get; protected set; }
    public bool active { get; protected set; }

    public Shape(Material color, ShapeDescriptor discriptor)
    {

        shapePositions = discriptor.positions;
        this.localPivot = discriptor.pivot;
        this.color = color;
       
        active = false;
      
    }
    public Shape Copy()
    {
        ShapeDescriptor discriptor = new ShapeDescriptor(shapePositions, localPivot);

        return new Shape(this.color, discriptor);

    }

    public virtual void Activate( Vector3 initPositionInGrid, Material c = null)
    {
        color = (c == null) ? color : c;
        active = true;
        globalPivot = initPositionInGrid;
        previousGlobalPivot = globalPivot;
    }

    public List<Vector3> GetGlobalCellPositions()
    {
        List<Vector3> globalPositions = new List<Vector3>();

        Vector3 dis = globalPivot - localPivot;
        foreach (Vector3 _localPos in shapePositions)
        {
            Vector3 _globalPos = _localPos + dis;
            globalPositions.Add(_globalPos);
        }

        return globalPositions;
    }
    //TODO: why not just clone and type of list?
    public static List<Shape> CloneList(List<Shape> oldList)
    {
        List<Shape> _listClone = new List<Shape>();
        oldList.ForEach((item) =>
        {
            _listClone.Add(item);
        });
        return _listClone;
    }

    public static List<T> CloneListGeneric<T>(List<T> oldList)
    {
        List<T> _listClone = new List<T>();
        oldList.ForEach((item) =>
        {
            _listClone.Add(item);
        });
        return _listClone;
    }


    public void MoveToPreviousPosition()
    {
        Vector3 _prevPos = previousGlobalPivot;
        previousGlobalPivot = globalPivot;
        globalPivot = _prevPos;
    }

    public void MoveToPreviousRotation()
    {
        shapePositions = CloneListGeneric(previousShapePositions);
    }

    private void Rotate(bool counterClokwise = false)
    {
        //each vector is a column (left to right) in the matrix
        Vector3[] affineTransformation;
        
        if(counterClokwise)
            affineTransformation = new Vector3[] { new Vector3(0,0,1), new Vector3(0,1,0), new Vector3(-1,0,0) };
        else
            affineTransformation = new Vector3[] { new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector3(1, 0, 0) };
       

        previousShapePositions = CloneListGeneric(shapePositions);

        List<Vector3> newCellPositions = new List<Vector3>();
        foreach (Vector3 _position in shapePositions)
        {
            float x = _position.x * affineTransformation[0].x + _position.y* affineTransformation[0].y + _position.z* affineTransformation[0].z;
            float y = _position.x * affineTransformation[1].x + _position.y * affineTransformation[1].y + _position.z * affineTransformation[1].z;
            float z = _position.x * affineTransformation[2].x + _position.y * affineTransformation[2].y + _position.z * affineTransformation[2].z;

            Vector3 newCellPosition = new Vector3(x,y,z);
            newCellPositions.Add(newCellPosition);
        }

        shapePositions = newCellPositions;
    }

    //rotate shape clockwise or counter clockwise
    public void UpdateRotation(TetrisMoves direction)
    {
        if (direction == TetrisMoves.Right)
             Rotate(true);
        else if (direction == TetrisMoves.Left)
             Rotate(false);
    }

    public void UpdatePosition(TetrisMoves move)
    {
        Vector3 newPivotPosition = globalPivot;

        previousGlobalPivot = newPivotPosition;

        if (move == TetrisMoves.Down)
            newPivotPosition.y -= 1;
        else if (move == TetrisMoves.Left)
            newPivotPosition.x -= 1;
        else if(move == TetrisMoves.Right)
            newPivotPosition.x += 1;
        else if (move == TetrisMoves.Forward)
            newPivotPosition.z += 1;
        else if (move == TetrisMoves.Back)
            newPivotPosition.z -= 1;
        else if (move == TetrisMoves.Up)
            newPivotPosition.y += 1;

        globalPivot = newPivotPosition;
    }


}
