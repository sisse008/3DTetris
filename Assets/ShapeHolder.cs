using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class ShapeHolder
{

    public static ShapeDescriptor ShapeA()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(new Vector3(0,0,0));
        positions.Add(new Vector3(0, 1, 0));
        positions.Add(new Vector3(0, 2, 0));
        positions.Add(new Vector3(0, 3, 0));

        Vector3 pivot = new Vector3(0, 2, 0);

        return new ShapeDescriptor(positions, pivot);
    }

    public static ShapeDescriptor ShapeB()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(new Vector3(0, 0, 0));
        positions.Add(new Vector3(0, 1, 0));
        positions.Add(new Vector3(0, 2, 0));
        positions.Add(new Vector3(1, 1, 0));

        Vector3 pivot = new Vector3(1, 1, 0);

        return new ShapeDescriptor(positions, pivot);
    }

    public static ShapeDescriptor ShapeC()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(new Vector3(0, 0, 0));
        positions.Add(new Vector3(0, 1, 0));
        positions.Add(new Vector3(0, 2, 0));
        positions.Add(new Vector3(1, 0, 0));

        Vector3 pivot = new Vector3(0, 1, 0);

        return new ShapeDescriptor(positions, pivot);
    }
}
