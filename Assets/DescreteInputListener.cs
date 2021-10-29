using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DescreteInputListener : MonoBehaviour
{

    public enum DescreteInputDirection { Down, Right, Left, Forward, Back, Up };

    public delegate void KeyAction(DescreteInputDirection direction);
    public event KeyAction KeyPressed;



    protected virtual void Up()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Up);
    }

    protected virtual void Down()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Down);
    }

    protected virtual void Left()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Left);
    }

    protected virtual void Right()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Right);
    }

    protected virtual void Backwards()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Back);
    }

    protected virtual void Forwards()
    {
        KeyPressed?.Invoke(DescreteInputDirection.Forward);
    }


}
