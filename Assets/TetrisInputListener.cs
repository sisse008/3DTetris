using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Keys
{
    public List<KeyCode> keyCodes;

    public bool isPressed()
    {
        foreach (KeyCode k in keyCodes)
            if (Input.GetKeyDown(k))
                return true;

        return false;
    }


}

public class TetrisInputListener : MonoBehaviour
{
    public Keys MoveLeft;
    public Keys MoveRight;
    public Keys MoveFar;
    public Keys MoveNear;
    public Keys MoveDown;
    public Keys RotateClock;
    public Keys RotateCounterClock;


    public UIMenuController UIMenu;
    public GameManager gameManager;
    public GameViewManager gameViewManager;

    void OnEnable()
    {
        UIMenu.b1Pressed += gameViewManager.UpdateViewClockwise;
        UIMenu.b2Pressed += gameViewManager.UpdateViewCounterClockwise;
    }

    void OnDisable()
    {
        UIMenu.b1Pressed -= gameViewManager.UpdateViewClockwise;
        UIMenu.b2Pressed -= gameViewManager.UpdateViewCounterClockwise;

    }


    void Update()
    {
        if (Input.anyKey == false)
            return;

        if (MoveLeft.isPressed())
        {
            gameManager.MoveActiveShapes((int)TetrisMoves.Left);
            return;
        }

        if (MoveRight.isPressed())
        {
            gameManager.MoveActiveShapes((int)TetrisMoves.Right);
            return;

        }

        if (MoveFar.isPressed())
        {
            gameManager.MoveActiveShapes((int)TetrisMoves.Forward);
            return;
        }

        if (MoveNear.isPressed())
        {
            gameManager.MoveActiveShapes((int)TetrisMoves.Back);
            return;

        }

        if (MoveDown.isPressed())
        {
            gameManager.MoveActiveShapes((int)TetrisMoves.Down);
            return;

        }

        if (RotateClock.isPressed())
        {
            //rotate = true
            gameManager.MoveActiveShapes((int)TetrisMoves.Left, true);
            return;
        }

        if (RotateCounterClock.isPressed())
        {
            //rotate = true
            gameManager.MoveActiveShapes((int)TetrisMoves.Right, true);
            return;
        }

    }


}
