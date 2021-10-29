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

public class PCInputListener : DescreteInputListener
{
    public Keys UpKeys;
    public Keys DownKeys;
    public Keys LeftKeys;
    public Keys RightKeys;
    public Keys ForwardsKeys;
    public Keys BackwardsKeys;


    protected override void Up() 
    {
        if(UpKeys.isPressed())
            base.Up();   
    }

    protected override void Down()
    {
        if (DownKeys.isPressed())
            base.Down();
    }

    protected override void Left()
    {
        if (LeftKeys.isPressed())
            base.Left();
    }

    protected override void Right()
    {
        if (RightKeys.isPressed())
            base.Right();
    }

    protected override void Forwards()
    {
        if (ForwardsKeys.isPressed())
            base.Forwards();
    }

    protected override void Backwards()
    {
        if(BackwardsKeys.isPressed())
            base.Backwards();
    }


    void Update()
    {
        Up();
        Down();
        Left();
        Right();
        Forwards();
        Backwards();


    }


}
