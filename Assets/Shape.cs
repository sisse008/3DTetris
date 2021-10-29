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

    protected List<Vector3> shapePositions = new List<Vector3>();
  

    //change this
    protected Vector3 globalPivot;

    protected Vector3 previousGlobalPivot;

    //does not change
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

    public Shape Clone(Material color)
    {
        ShapeDescriptor discriptor = new ShapeDescriptor(shapePositions, localPivot);

        return new Shape(color, discriptor);

    }

    public virtual void Activate( Vector3 initPositionInGrid, Material c = null)
    {
        color = (c == null) ? color : c;
        active = true;
        globalPivot = initPositionInGrid;
        previousGlobalPivot = globalPivot;
    }


    public List<Vector3> GetGlobalPositions()
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

    public static List<Shape> CloneList(List<Shape> oldList)
    {
        List<Shape> _listClone = new List<Shape>();
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

    public void Move(TetrisMoves move)
    {
        Vector3 newPivotPosition = globalPivot;

        previousGlobalPivot = newPivotPosition;

       // Debug.Log(move.ToString());
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
