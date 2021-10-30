using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class ShapeHolder
{

    public static ShapeDescriptor ShapeA()
    {
        List<Vector3> localCellPositions = new List<Vector3>();
        localCellPositions.Add(new Vector3(0,0,0));
        localCellPositions.Add(new Vector3(0, 1, 0));
        localCellPositions.Add(new Vector3(0, 2, 0));
        localCellPositions.Add(new Vector3(0, 3, 0));

        Vector3 pivot = new Vector3(0, 2, 0);

        return new ShapeDescriptor(localCellPositions, pivot);
    }

    public static ShapeDescriptor ShapeB()
    {
        List<Vector3> localCellPositions = new List<Vector3>();
        localCellPositions.Add(new Vector3(0, 0, 0));
        localCellPositions.Add(new Vector3(0, 1, 0));
        localCellPositions.Add(new Vector3(0, 2, 0));
        localCellPositions.Add(new Vector3(1, 1, 0));

        Vector3 pivot = new Vector3(1, 1, 0);

        return new ShapeDescriptor(localCellPositions, pivot);
    }

    public static ShapeDescriptor ShapeC()
    {
        List<Vector3> localShapePositions = new List<Vector3>();
        localShapePositions.Add(new Vector3(0, 0, 0));
        localShapePositions.Add(new Vector3(0, 1, 0));
        localShapePositions.Add(new Vector3(0, 2, 0));
        localShapePositions.Add(new Vector3(1, 0, 0));

        Vector3 pivot = new Vector3(0, 1, 0);

        return new ShapeDescriptor(localShapePositions, pivot);
    }
}
