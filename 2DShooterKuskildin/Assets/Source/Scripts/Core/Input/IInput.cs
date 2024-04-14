using UnityEngine;

public interface IInput
{
    float HorizontalMove();
    float VerticalMove();
    Vector3 MousePosition();    
    bool Shoot();
}
