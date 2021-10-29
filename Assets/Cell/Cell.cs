using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //is ocupide
    public bool isOccupied { get; private set; }
   

    //set colure
    Material _default;

    Material myMaterial;

    void OnEnable()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
        _default = myMaterial;
    }

    public void SetOccupied(bool occupied, Material c = null)
    {
        if (occupied == isOccupied)
            return;
        isOccupied = occupied;
        Material _c = (c == null) ? _default : c;
        SetColor(_c);
    }

    void SetColor(Material c)
    {
        GetComponent<MeshRenderer>().material = c;
    }
}
