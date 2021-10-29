using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewManager : MonoBehaviour, ICameraViewController
{

    static List<Vector3> gameViewPositions;

    Vector3 front, right, rear, left, center;

    public Camera camera;


    int index;


    public void UpdateViewClockwise()
    {
        index++;
        if (index == 4)
            index = 0;

        SetView(center, gameViewPositions[index]);
    }

    public void UpdateViewCounterClockwise()
    {
        index--;
        if (index == -1)
            index = 3;

        SetView(center, gameViewPositions[index]);
    }

    public void SetView(Vector3 targetCenter, Vector3 newPosition)
    {
        camera.transform.position = newPosition;
        Vector3 direction = targetCenter - newPosition;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        camera.transform.rotation = rotation;

    }



    public void Initiate(Vector3 front, Vector3 right, Vector3 rear, Vector3 left, Vector3 center)
    {
        index = 0;

       

        this.center = center;

        this.right = right;
        this.left = left;
        this.rear = rear;
        this.front = front;


        gameViewPositions = new List<Vector3>(4);
        //front view
        gameViewPositions.Add(front);
        //right view
        gameViewPositions.Add(right);
        //rear view
        gameViewPositions.Add(rear);
        //left view
        gameViewPositions.Add(left);
    }
}
