using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : IInput
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    
    public float HorizontalMove()
    {
        return Input.GetAxis(HorizontalAxis);
    }
    public float VerticalMove()
    {
        return Input.GetAxis(VerticalAxis);
    }
    
    public bool Shoot()
    {
        return Input.GetMouseButtonDown(0);
    }

    public Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
