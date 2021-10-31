using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuController : MonoBehaviour
{

    public Button r1, r2;


    public delegate void UIRotationControllAction();
    public event UIRotationControllAction b1Pressed;
    public event UIRotationControllAction b2Pressed;



    void OnEnable()
    {
        if(r1)
            r1.onClick.AddListener(ButtonOnePressed);
        if(r2)
            r2.onClick.AddListener(ButtonTwoPressed);
    }

    void OnDisable()
    {
        if(r1)
            r1.onClick.RemoveAllListeners();
        if(r2)
            r2.onClick.RemoveAllListeners();
    }

    void ButtonOnePressed()
    {
        b1Pressed?.Invoke();
    }
    void ButtonTwoPressed()
    {
        b2Pressed?.Invoke();
    }
}
