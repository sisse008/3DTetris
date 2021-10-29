using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{

    public Button b1, b2, b3, b4, b5;


    public delegate void UIMenuAction();
    public event UIMenuAction b1Pressed;
    public event UIMenuAction b2Pressed;
    public event UIMenuAction b3Pressed;
    public event UIMenuAction b4Pressed;
    public event UIMenuAction b5Pressed;


    void OnEnable()
    {
        if(b1)
            b1.onClick.AddListener(ButtonOnePressed);
        if(b2)
            b2.onClick.AddListener(ButtonTwoPressed);
        if(b3)
            b3.onClick.AddListener(ButtonThreePressed);
        if(b4)
            b4.onClick.AddListener(ButtonFourPressed);
        if(b5)
            b5.onClick.AddListener(ButtonFivePressed);
    }

    void OnDisable()
    {
        if(b1)
            b1.onClick.RemoveAllListeners();
        if(b2)
            b2.onClick.RemoveAllListeners();
        if(b3)
            b3.onClick.RemoveAllListeners();
        if(b4)
            b4.onClick.RemoveAllListeners();
        if(b5)
            b5.onClick.RemoveAllListeners();
    }

    void ButtonOnePressed()
    {
        b1Pressed?.Invoke();
    }
    void ButtonTwoPressed()
    {
        b2Pressed?.Invoke();
    }

    void ButtonThreePressed()
    {
        b3Pressed?.Invoke();
    }
    void ButtonFourPressed()
    {
        b4Pressed?.Invoke();
    }
    void ButtonFivePressed()
    {
        b5Pressed?.Invoke();
    }
}
