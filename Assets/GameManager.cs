using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;





public enum TetrisMoves { Down, Right, Left, Forward, Back, Up };
public class GameManager : MonoBehaviour
{
    //make this constant
    List<ShapeDescriptor> shapes;

    List<Shape> activeShapes;

    public static List<Shape> staticShapes { get; private set; }

    float timer = 0;

    public float everyXseconds = 2;

   

    public GameViewManager gameViewManager;



    public Material[] colorMaterials;
    public TetrisGrid tetrisGrid;



    public Vector3 FrontView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(0, tetrisGrid.Height * 0.8f, -1*tetrisGrid.Depth);
    }

    public Vector3 RearView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(0, tetrisGrid.Height * 0.8f,  tetrisGrid.Depth);
    }

    public Vector3 RightView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(tetrisGrid.Width, tetrisGrid.Height * 0.8f, 0);
    }

    public Vector3 LeftView()
    {
        return tetrisGrid.CenterPosition() + new Vector3(-1*tetrisGrid.Width, tetrisGrid.Height * 0.8f, 0);
    }


 


    // Start is called before the first frame update
    void Start()
    {

       
        shapes= new List<ShapeDescriptor>();
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

        gameViewManager.Initiate(FrontView(), RightView(), RearView(), LeftView(), tetrisGrid.CenterPosition());

        gameViewManager.SetView(tetrisGrid.CenterPosition(), FrontView());

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
        Debug.Log("");
    }



    public void MoveActiveShapes(int direction)
    {
        TetrisMoves move = (TetrisMoves)direction;

        //do not allow this moves
        if (move == TetrisMoves.Up)
            return;

        List<Shape> clone = Shape.CloneList(activeShapes);

        bool modified = false;


        foreach (Shape s in activeShapes)
        {
            
            bool isSafeMove = true;
            
            tetrisGrid.MoveShape(s, move, ref isSafeMove, staticShapes);

            if (!isSafeMove && move == TetrisMoves.Down)
            {

                clone.Remove(s);
                staticShapes.Add(s);
                modified = true;
            }
        }

        if (modified)
        {
            activeShapes = Shape.CloneList(clone);
        }
       
    }



    void UpdateTetrisGrid()
    {
        timer += Time.deltaTime;

        if (timer > everyXseconds)
        {
          //  Debug.Log("activeShapes.Count = " + activeShapes.Count);
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
