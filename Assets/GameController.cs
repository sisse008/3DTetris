using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MVCAbstract
{
   
    public UIMenuController UIMenu;
    public GameManager gameManager;
    public GameViewManager gameViewManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        UIMenu.b1Pressed += gameViewManager.UpdateViewClockwise;
        UIMenu.b2Pressed += gameViewManager.UpdateViewCounterClockwise;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UIMenu.b1Pressed -= gameViewManager.UpdateViewClockwise;
        UIMenu.b2Pressed -= gameViewManager.UpdateViewCounterClockwise;

    }


  
    protected override void UpdateGame(DescreteInputListener.DescreteInputDirection direction)
    {
        if (direction == DescreteInputListener.DescreteInputDirection.Down)
            return;
        gameManager.MoveActiveShapes((int)direction);
    }
   

  

  
}
