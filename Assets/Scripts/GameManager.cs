using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;





public enum TetrisMoves { Down, Right, Left, Forward, Back, Up, Xclock, XcounterClock, Yclock, YcounterClock, Zclock, ZcounterClock};
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
    [SerializeField] Cell cellPrefab;
    [SerializeField] TetrisGrid tetrisGridPrefab;
    [SerializeField] TetrisGrid tetrisGrid;
    public AssetBundleLoader assetBundleLoader;
    /*
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
    */

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

        StartCoroutine(LoadGameAssetsAndInitGameAsync());
       // LoadGameAssetsAndInitGame();
    }

    IEnumerator LoadGameAssetsAndInitGameAsync()
    {
        // yield return assetBundleLoader.LoadAssetsFromDiskAsync("tetris", OnFinsihedLoadingAssets);  
        yield return assetBundleLoader.LoadWebRequest(OnFinsihedLoadingAssets);
    }

    private void OnFinsihedLoadingAssets(GameObject[] objs)
    {
        foreach (GameObject obj in objs)
        {
            if (cellPrefab == null) cellPrefab = obj.GetComponent<Cell>();
            if (tetrisGridPrefab == null) tetrisGridPrefab = obj.GetComponent<TetrisGrid>();
            if (cellPrefab && tetrisGridPrefab)
            {
                StartNewGame();
                break;
            }
        }
    }
    void LoadGameAssetsAndInitGame()
    {
        try
        {
            cellPrefab = assetBundleLoader.LoadAssetFromDisk("tetris", "cell").GetComponent<Cell>();
            tetrisGridPrefab = assetBundleLoader.LoadAssetFromDisk("tetris", "grid").GetComponent<TetrisGrid>();
            StartNewGame();
        }
        catch
        {
            Debug.LogError(">>> GameManager.LoadGameAssetsAndInitGame: was not abble to load assests from asset bundle. Quitting game");
        }
       
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
        tetrisGrid = Instantiate(tetrisGridPrefab);
        tetrisGrid.InitGrid(cellPrefab);
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

    public void RotateShapeClockwiseZAxis()
    {
        MoveActiveShapes((int)TetrisMoves.Left, true);
    }

    public void RotateShapeCounterClockwiseZAxis()
    {
        MoveActiveShapes((int)TetrisMoves.Right, true);
    }

    public void RotateShapeClockwiseXAxis()
    {
        MoveActiveShapes((int)TetrisMoves.Back, true);
    }
    public void RotateShapeCounterClockwiseXAxis()
    {
        MoveActiveShapes((int)TetrisMoves.Forward, true);
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
           // Debug.Log("frame rate : " + 1.0f / Time.smoothDeltaTime);
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
        if(tetrisGrid)
            UpdateTetrisGrid();
    }
}
