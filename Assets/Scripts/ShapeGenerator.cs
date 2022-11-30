using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{

    public ShapeGeneratorController controller;

    public UIMenuController UIMenu;

    void OnEnable()
    {
        UIMenu.b1Pressed += controller.SelectCellPosition;
    }

    void OnDisable()
    {
        UIMenu.b1Pressed -= controller.SelectCellPosition;
    }
}
