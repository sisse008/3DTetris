using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

static class Extentions
{
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }

    public static List<Vector3> Clone(List<Vector3> listToClone)
    {
        List<Vector3> _clone = new List<Vector3>();
        listToClone.ForEach((v) =>
        {
            _clone.Add(new Vector3(v.x,v.y,v.z));
        });
        return _clone;
    }



}
