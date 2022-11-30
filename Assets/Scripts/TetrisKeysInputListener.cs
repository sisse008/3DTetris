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

public class TetrisKeysInputListener : MonoBehaviour
{
    public Keys MoveLeftKey;
    public Keys MoveRightKey;
    public Keys MoveFarKey;
    public Keys MoveNearKey;
    public Keys MoveDownKey;
    public Keys RotateClockKey;
    public Keys RotateCounterClockKey;

    public GameManager gameManager;


    void Update()
    {
        if (Input.anyKey == false)
            return;

        if (MoveLeftKey.isPressed())
        {
            gameManager.TransformShapeLeft();
            return;
        }

        if (MoveRightKey.isPressed())
        {
            gameManager.TransformShapeRight();
            return;
        }

        if (MoveFarKey.isPressed())
        {
            gameManager.TransformShapeFar();
            return;
        }

        if (MoveNearKey.isPressed())
        {
            gameManager.TransformShapeNear();
            return;
        }

        if (MoveDownKey.isPressed())
        {
            gameManager.TransformShapeDown();
            return;
        }

        if (RotateClockKey.isPressed())
        {
            gameManager.RotateShapeClockwiseZAxis();
            return;
        }

        if (RotateCounterClockKey.isPressed())
        {
            gameManager.RotateShapeCounterClockwiseZAxis();
            return;
        }

    }


}
