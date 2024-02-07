using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShape
{
    public void SetColor(int value);
    public int GetColor();

    public void SetSize(float value);
    public float GetSize();

    public void SetPosition(Vector2 value);
    public Vector2 GetPosition();

    public bool Selected { get; set; }

    public bool PosInside(Vector2 value);

    public void Destroy();
}
