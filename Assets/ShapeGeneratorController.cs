using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGeneratorController : MonoBehaviour
{
    public TetrisGrid grid;
    public Material material;

    protected List<Shape> generatedShapes = new List<Shape>();

    Shape currentActiveCellShape;
    List<Shape> staticCellShapes;


    // Start is called before the first frame update
    void Start()
    {
        staticCellShapes = new List<Shape>();
        grid.InitGrid();

        InitiateNewCellShape();
    }

    public void SelectCellPosition()
    {
       
        staticCellShapes.Add(currentActiveCellShape.Copy());
        currentActiveCellShape = null;

        InitiateNewCellShape();
    }

    void InitiateNewCellShape()
    {    
        List<Vector3> cellPosition = new List<Vector3>{ Vector3.zero };

        ShapeDescriptor type = new ShapeDescriptor(cellPosition, Vector3.zero); 

        Shape s = new Shape(material, type);

        s.Activate(grid.GetUpperCenterCellPosition());
        currentActiveCellShape = s;
    }
 
}
