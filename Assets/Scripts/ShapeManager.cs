using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShapeManager : MonoBehaviour
{
    private static ShapeManager _instance;

    public static ShapeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShapeManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ShapeManager).Name;
                    _instance = obj.AddComponent<ShapeManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject Circle;
    public GameObject Square;

    public List<IShape> List;
    private IShape _selectedShape = null;

    // Start is called before the first frame update
    void Start()
    {
        List = new List<IShape>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject newObject;

            if (UIScript.Instance.SelectedShape == Shapes.Square)
            {
                newObject = Instantiate(Square, worldPosition, Quaternion.identity);
            } else
            {
                newObject = Instantiate(Circle, worldPosition, Quaternion.identity);
            }

            if (newObject != null)
            {
                IShape shape = newObject.GetComponent<IShape>();
                shape.SetColor(UIScript.Instance.SelectedColor);
                shape.SetSize(UIScript.Instance.SelectedSize);

                List.Add(shape);
                Select(shape);
            }

        } else if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (_selectedShape != null)
            {
                List.Remove(_selectedShape);
                _selectedShape.Destroy();
                _selectedShape = null;
                UIScript.Instance.SetSelected(null);
            }

        } else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            var shape = shapeAtPos(worldPosition);

            Select(shape);
        }
    }

    private void Select(IShape shape)
    {
        if (_selectedShape == shape) return;

        if (_selectedShape != null)
        {
            _selectedShape.Selected = false;
        }
        _selectedShape = shape;

        if (_selectedShape != null)
        {
            _selectedShape.Selected = true;
        }
        UIScript.Instance.SetSelected(_selectedShape);
    }

    private IShape shapeAtPos(Vector2 pos)
    {
        foreach(IShape shape in List)
        {
            if (shape.PosInside(pos)) return shape;
        }
        return null;
    }
}
