using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem 
{
    public static bool Collides(List<Vector3> obj1, List<Vector3> obj2)
    {
        foreach (Vector3 p1 in obj1)
            foreach (Vector3 p2 in obj2)
                if (p2 == p1) 
                    return true;

        return false;
    }

 
}
