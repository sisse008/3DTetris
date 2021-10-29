using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCShapeGenerator : MVCAbstract
{

    public ShapeGeneratorController controller;

    public UIMenuController UIMenu;

    protected override void OnEnable()
    {
        base.OnEnable();
        UIMenu.b1Pressed += controller.SelectCellPosition;
        
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UIMenu.b1Pressed -= controller.SelectCellPosition;


    }


    protected override void UpdateGame(DescreteInputListener.DescreteInputDirection direction)
    {
        controller.MoveCell((int)direction);

    }
}
