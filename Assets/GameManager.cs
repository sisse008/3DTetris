﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;





public enum TetrisMoves { Down, Right, Left, Forward, Back, Up };
public class GameManager : MonoBehaviour
{
    /*
    //TODO: use dependancy injection instead of singleton
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get 
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }   
    }
    */
    //TODO: make this constant
    List<ShapeDescriptor> shapes;

    List<Shape> activeShapes;

    public static List<Shape> staticShapes { get; private set; }

    float timer = 0;

    public float everyXseconds = 2;

    public Material[] colorMaterials;
    public TetrisGrid tetrisGrid;

    Vector3 FrontView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(0, tetrisGrid.Height * 0.8f, -1 * tetrisGrid.Depth);
    }

    Vector3 RearView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(0, tetrisGrid.Height * 0.8f, tetrisGrid.Depth);
    }

    Vector3 RightView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(tetrisGrid.Width, tetrisGrid.Height * 0.8f, 0);
    }

    Vector3 LeftView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(-1 * tetrisGrid.Width, tetrisGrid.Height * 0.8f, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        //dont change frame rate with ARFoundation
      //  Application.targetFrameRate = 20;
        shapes = new List<ShapeDescriptor>();
        shapes.Add(ShapeHolder.ShapeA());
        shapes.Add(ShapeHolder.ShapeB());
        shapes.Add(ShapeHolder.ShapeC());

        staticShapes = new List<Shape>();

        activeShapes = new List<Shape>();

        StartNewGame();
    }

    Material GetRandomMaterial()
    {
        int size = colorMaterials.Length;
        int rand = Random.Range(0, size);
        Material m = colorMaterials[rand];
        return m;

    }

    void StartNewGame()
    {
        tetrisGrid.InitGrid();
    }

    void InitiateNewShape()
    {
        //get random shape
        int typesSize = shapes.Count;
        int typeRand = Random.Range(0, typesSize);

        ShapeDescriptor type = shapes[typeRand];
        Material material = GetRandomMaterial();

        Shape s = new Shape(material, type);

        s.Activate(tetrisGrid.GetUpperCenterCellPosition());
        activeShapes.Add(s);
    }

    public void RotateShapeClockwise()
    {
        MoveActiveShapes((int)TetrisMoves.Left, true);
    }

    public void RotateShapeCounterClockwise()
    {
        MoveActiveShapes((int)TetrisMoves.Right, true);
    }

    public void TransformShapeRight()
    {
        MoveActiveShapes((int)TetrisMoves.Right);
    }
    public void TransformShapeLeft()
    {
        MoveActiveShapes((int)TetrisMoves.Left);
    }

    public void TransformShapeDown()
    {
        MoveActiveShapes((int)TetrisMoves.Down);
    }

    public void TransformShapeFar()
    {
        MoveActiveShapes((int)TetrisMoves.Forward);
    }

    public void TransformShapeNear()
    {
        MoveActiveShapes((int)TetrisMoves.Back);
    }

    //move all active shapes in grid that did not reach the ground yet.
    //public because called from input listener
    private void MoveActiveShapes(int direction, bool rotate = false)
    {
        TetrisMoves move = (TetrisMoves)direction;

        //do not allow this moves for now
        if (move == TetrisMoves.Up)
            return;

        List<Shape> _shapes = Shape.CloneList(activeShapes);

        bool modified = false;

        foreach (Shape s in activeShapes)
        {            
            bool isSafeMove = tetrisGrid.MoveShape(s, move, staticShapes, rotate);

            if (!isSafeMove && move == TetrisMoves.Down && rotate == false)
            {
                _shapes.Remove(s);
                staticShapes.Add(s);
                modified = true;
            }
        }

        if (modified)
        {
            activeShapes = Shape.CloneList(_shapes);
        }
       
    }

    void UpdateTetrisGrid()
    {
        timer += Time.deltaTime;

        if (timer > everyXseconds)
        {
            Debug.Log("frame rate : " + 1.0f / Time.smoothDeltaTime);
            if (activeShapes.Count == 0)
            {
                InitiateNewShape();

                return;
            }
            MoveActiveShapes((int)TetrisMoves.Down);
            timer = 0;

        }
    }

  
    // Update is called once per frame
    void Update()
    {
        UpdateTetrisGrid();
    }
}
