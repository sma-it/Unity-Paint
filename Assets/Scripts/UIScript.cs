using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    private static UIScript _instance;

    public static UIScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIScript>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(UIScript).Name;
                    _instance = obj.AddComponent<UIScript>();
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

    public Button SquareButton;
    public Button CircleButton;
    public TMP_InputField SizeInput;
    public TMP_Dropdown ColorDropDown;

    public Shapes SelectedShape;
    public float SelectedSize = 1;
    public int SelectedColor = 0;
    private IShape _selected;

    // Start is called before the first frame update
    void Start()
    {
        OnSquareButtonClicked();
        SizeInput.onValueChanged.AddListener(OnSizeChange);
        ColorDropDown.onValueChanged.AddListener(OnColorChange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSquareButtonClicked()
    {
        SquareButton.GetComponent<Image>().color = Color.white;
        CircleButton.GetComponent<Image>().color = Color.grey;
        SelectedShape = Shapes.Square;
    }

    public void OnCircleButtonClicked()
    {
        SquareButton.GetComponent<Image>().color = Color.grey;
        CircleButton.GetComponent<Image>().color = Color.white;
        SelectedShape = Shapes.Circle;
    }

    public void OnSizeChange(string text)
    {
        float value;
        if (float.TryParse(text, out value))
        {
            SelectedSize = value;
            
            if (_selected != null)
            {
                _selected.SetSize(SelectedSize);
            }
        }
    }

    public void OnColorChange(int value)
    {
        SelectedColor = value;
        if (_selected != null)
        {
            _selected.SetColor(value);
        }
    }

    public void SetSelected(IShape shape)
    {
        _selected = shape;
        
        if (_selected == null)
        {
            SquareButton.gameObject.SetActive(true);
            CircleButton.gameObject.SetActive(true);
        } else
        {
            SquareButton.gameObject.SetActive(false);
            CircleButton.gameObject.SetActive(false);

            SizeInput.text = "" + _selected.GetSize();
            ColorDropDown.value = _selected.GetColor();
        }


    }
}
