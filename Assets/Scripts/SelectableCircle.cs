using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableCircle : MonoBehaviour, IShape
{
    private SpriteRenderer spriteRenderer;

    private bool _selected = false;
    private int _color = 0;

    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            Select(_selected);
        }
    }

    public int GetColor()
    {
        return _color;
    }

    public Vector2 GetPosition()
    {
        return gameObject.transform.position;
    }

    public float GetSize()
    {
        return gameObject.transform.localScale.x;
    }

    public void SetColor(int value)
    {
        _color = value;
        if (spriteRenderer == null) return;

        Color color;
        switch(_color)
        {
            case 0: color = Color.red; break;
            case 1: color = Color.green; break;
            default: color = Color.blue; break;
        }
        spriteRenderer.material.color = color;
        Select(_selected);
    }

    public void SetPosition(Vector2 value)
    {
        gameObject.transform.localPosition = value;
    }

    public void SetSize(float value)
    {
        gameObject.transform.localScale = new Vector3(value, value, value);
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(_color);
    }

    // Update is called once per frame
    void Update()
    {
        if (_selected && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            gameObject.transform.position = worldPosition;
        }
    }

    void Select(bool select)
    {
        if (spriteRenderer != null)
        {
            if (select)
            {
                Color color = spriteRenderer.material.color;
                color.a = 1;
                spriteRenderer.material.color = color;
            } else
            {
                Color color = spriteRenderer.material.color;
                color.a = 0.5f;
                spriteRenderer.material.color = color;
            }
        }
    }

    public bool PosInside(Vector2 value)
    {
        float radius = gameObject.transform.localScale.x;
        float distance = Vector2.Distance(gameObject.transform.position, value);

        return (distance < radius);
    }


    public void Destroy()
    {
        Destroy(gameObject);
    }
}
