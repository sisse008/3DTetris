using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class MVCAbstract : MonoBehaviour
{
   
    // input listener was here


    protected virtual void OnEnable()
    {
       
       //inputlistener on key press 
    }

    protected virtual void OnDisable()
    {
        
       

    }

    protected virtual void UpdateGame(DescreteInputListener.DescreteInputDirection direction)
    {
       
    }
}
